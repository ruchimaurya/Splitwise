using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class TransactionTableFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_T_ReceivedByFriend",
                table: "Transaction",
                column: "T_ReceivedByFriend");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_T_ReceivedByGroup",
                table: "Transaction",
                column: "T_ReceivedByGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_T_ReceivedByFriend",
                table: "Transaction",
                column: "T_ReceivedByFriend",
                principalTable: "Users",
                principalColumn: "U_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Groups_T_ReceivedByGroup",
                table: "Transaction",
                column: "T_ReceivedByGroup",
                principalTable: "Groups",
                principalColumn: "G_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_T_ReceivedByFriend",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Groups_T_ReceivedByGroup",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_T_ReceivedByFriend",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_T_ReceivedByGroup",
                table: "Transaction");
        }
    }
}
