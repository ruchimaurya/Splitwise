using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class DeleteFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "U_Deleted",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "G_Deleted",
                table: "Groups",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "U_Deleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "G_Deleted",
                table: "Groups");
        }
    }
}
