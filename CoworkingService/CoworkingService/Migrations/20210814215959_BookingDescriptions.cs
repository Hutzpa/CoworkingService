using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkingService.Migrations
{
    public partial class BookingDescriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RoomOccupieds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "RoomOccupieds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RoomOccupieds");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "RoomOccupieds");
        }
    }
}
