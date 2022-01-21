using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TesterAPI;

var builder = WebApplication.CreateBuilder(args);

// Set Content Root - Fortæller at den bruge wwwroot mappen, som roden til sit content
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

builder.Services.AddCors();
builder.Services.AddDbContext<QAContext>(options => options
                                        .UseSqlite(builder.Configuration.GetConnectionString("QADbString")));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// HTTPS Redirection
app.UseHttpsRedirection();

// Cors
app.UseCors(options =>
{
    options.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
});
// Static Files
app.UseStaticFiles();


/* CASES */
app.MapGet("/api/cases", async (QAContext db) => await db.Cases.ToListAsync());
app.MapGet("/api/cases/{id}", async (QAContext db, int id) => await db.Cases.Include(e => e.Tasks).FirstOrDefaultAsync(e => e.Id == id));
app.MapPost("/api/cases", async (QAContext db, Case @case) =>
{
    await db.Cases.AddAsync(@case);
    await db.SaveChangesAsync();

    return Results.Ok();
});

/* TASKS */
app.MapGet("/api/tasks", async (QAContext db) => await db.Tasks.ToListAsync());
app.MapGet("/api/tasks/{id}", async (QAContext db, int id) => await db.Tasks.Include(e => e.Case).FirstOrDefaultAsync(e => e.Id == id));


app.Run();
