using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TesterAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddDbContext<QAContext>(options => options
                                        .UseSqlite(builder.Configuration.GetConnectionString("QADbString")));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
});


/* CASES */
app.MapGet("/api/cases", async (QAContext db) => await db.Cases.ToListAsync());
app.MapGet("/api/cases/{id}", async (QAContext db, int id) => await db.Cases.Include(e => e.Tasks).FirstOrDefaultAsync(e => e.Id == id));

/* TASKS */
app.MapGet("/api/tasks", async (QAContext db) => await db.Tasks.ToListAsync());
app.MapGet("/api/tasks/{id}", async (QAContext db, int id) => await db.Tasks.Include(e => e.Case).FirstOrDefaultAsync(e => e.Id == id));

app.UseHttpsRedirection();

app.Run();
