using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class DelFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "T_Deleted",
                table: "Transaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "It_Delete",
                table: "IndividualTransaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ib_Deleted",
                table: "IndividualBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Gb_Deleted",
                table: "GroupBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "A_Deleted",
                table: "Activity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "T_Deleted",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "It_Delete",
                table: "IndividualTransaction");

            migrationBuilder.DropColumn(
                name: "Ib_Deleted",
                table: "IndividualBills");

            migrationBuilder.DropColumn(
                name: "Gb_Deleted",
                table: "GroupBills");

            migrationBuilder.DropColumn(
                name: "A_Deleted",
                table: "Activity");
        }
    }
}
