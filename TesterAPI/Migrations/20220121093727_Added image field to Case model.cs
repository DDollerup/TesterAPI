using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesterAPI.Migrations
{
    public partial class AddedimagefieldtoCasemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Cases",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cases");
        }
    }
}
