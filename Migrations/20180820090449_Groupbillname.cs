using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class Groupbillname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gb_Name",
                table: "GroupBills",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gb_Name",
                table: "GroupBills",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
