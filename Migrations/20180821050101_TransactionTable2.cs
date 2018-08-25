using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class TransactionTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gt_Amount",
                table: "Transaction",
                newName: "T_Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "T_Amount",
                table: "Transaction",
                newName: "Gt_Amount");
        }
    }
}
