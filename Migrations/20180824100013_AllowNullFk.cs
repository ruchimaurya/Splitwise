using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class AllowNullFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Users_A_ForFriend",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_A_ForGroup",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "A_ForGroup",
                table: "Activity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "A_ForFriend",
                table: "Activity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Users_A_ForFriend",
                table: "Activity",
                column: "A_ForFriend",
                principalTable: "Users",
                principalColumn: "U_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_A_ForGroup",
                table: "Activity",
                column: "A_ForGroup",
                principalTable: "Groups",
                principalColumn: "G_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Users_A_ForFriend",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_A_ForGroup",
                table: "Activity");

            migrationBuilder.AlterColumn<int>(
                name: "A_ForGroup",
                table: "Activity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "A_ForFriend",
                table: "Activity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Users_A_ForFriend",
                table: "Activity",
                column: "A_ForFriend",
                principalTable: "Users",
                principalColumn: "U_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_A_ForGroup",
                table: "Activity",
                column: "A_ForGroup",
                principalTable: "Groups",
                principalColumn: "G_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
