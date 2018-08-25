using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class ActivityFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ib_settled",
                table: "IndividualBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Gb_Settled",
                table: "GroupBills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "A_ForFriend",
                table: "Activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "A_ForGroup",
                table: "Activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activity_A_ForFriend",
                table: "Activity",
                column: "A_ForFriend");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_A_ForGroup",
                table: "Activity",
                column: "A_ForGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Users_A_ForFriend",
                table: "Activity",
                column: "A_ForFriend",
                principalTable: "Users",
                principalColumn: "U_Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_A_ForGroup",
                table: "Activity",
                column: "A_ForGroup",
                principalTable: "Groups",
                principalColumn: "G_Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Users_A_ForFriend",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_A_ForGroup",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_A_ForFriend",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_A_ForGroup",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "Ib_settled",
                table: "IndividualBills");

            migrationBuilder.DropColumn(
                name: "Gb_Settled",
                table: "GroupBills");

            migrationBuilder.DropColumn(
                name: "A_ForFriend",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "A_ForGroup",
                table: "Activity");
        }
    }
}
