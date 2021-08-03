using Microsoft.EntityFrameworkCore.Migrations;

namespace CoworkingService.Data.Migrations
{
    public partial class OwnerFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Coworkings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coworkings_OwnerId",
                table: "Coworkings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coworkings_AspNetUsers_OwnerId",
                table: "Coworkings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coworkings_AspNetUsers_OwnerId",
                table: "Coworkings");

            migrationBuilder.DropIndex(
                name: "IX_Coworkings_OwnerId",
                table: "Coworkings");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Coworkings");
        }
    }
}
