using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkingService.Migrations
{
    public partial class PeopleCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeopleCurrentlyIn",
                table: "Coworkings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleCurrentlyIn",
                table: "Coworkings");
        }
    }
}
