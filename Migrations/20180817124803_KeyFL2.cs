using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class KeyFL2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendList",
                table: "FriendList");

            migrationBuilder.AddColumn<int>(
                name: "Fl_Id",
                table: "FriendList",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendList",
                table: "FriendList",
                column: "Fl_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FriendList_User_Id",
                table: "FriendList",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendList",
                table: "FriendList");

            migrationBuilder.DropIndex(
                name: "IX_FriendList_User_Id",
                table: "FriendList");

            migrationBuilder.DropColumn(
                name: "Fl_Id",
                table: "FriendList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendList",
                table: "FriendList",
                column: "User_Id");
        }
    }
}
