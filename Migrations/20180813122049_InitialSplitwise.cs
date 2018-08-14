using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.Migrations
{
    public partial class InitialSplitwise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    U_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    U_Name = table.Column<string>(nullable: false),
                    U_Password = table.Column<string>(nullable: false),
                    U_Email = table.Column<string>(nullable: false),
                    U_Contact = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.U_Id);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    A_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    A_DoneBy = table.Column<int>(nullable: false),
                    A_Description = table.Column<string>(nullable: true),
                    A_Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.A_Id);
                    table.ForeignKey(
                        name: "FK_Activity_Users_A_DoneBy",
                        column: x => x.A_DoneBy,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendList",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false),
                    Friend_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendList", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_FriendList_Users_Friend_Id",
                        column: x => x.Friend_Id,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendList_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    G_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    G_Name = table.Column<string>(nullable: false),
                    G_Admin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.G_Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_G_Admin",
                        column: x => x.G_Admin,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualBills",
                columns: table => new
                {
                    Ib_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ib_Name = table.Column<string>(nullable: false),
                    Ib_PaidBy = table.Column<int>(nullable: false),
                    Ib_Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualBills", x => x.Ib_Id);
                    table.ForeignKey(
                        name: "FK_IndividualBills_Users_Ib_PaidBy",
                        column: x => x.Ib_PaidBy,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndividualTransaction",
                columns: table => new
                {
                    It_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    It_BillId = table.Column<int>(nullable: false),
                    It_PaidBy = table.Column<int>(nullable: false),
                    It_Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualTransaction", x => x.It_Id);
                    table.ForeignKey(
                        name: "FK_IndividualTransaction_Users_It_PaidBy",
                        column: x => x.It_PaidBy,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    I_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    I_Sender = table.Column<int>(nullable: false),
                    I_Email = table.Column<string>(nullable: false),
                    I_Contact = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.I_Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Users_I_Sender",
                        column: x => x.I_Sender,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupBills",
                columns: table => new
                {
                    Gb_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gb_Name = table.Column<int>(nullable: false),
                    Gb_PaidBy = table.Column<int>(nullable: false),
                    Gb_ForGroup = table.Column<int>(nullable: false),
                    Gb_Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupBills", x => x.Gb_Id);
                    table.ForeignKey(
                        name: "FK_GroupBills_Groups_Gb_ForGroup",
                        column: x => x.Gb_ForGroup,
                        principalTable: "Groups",
                        principalColumn: "G_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupBills_Users_Gb_PaidBy",
                        column: x => x.Gb_PaidBy,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    GM_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GM_GroupId = table.Column<int>(nullable: false),
                    Gm_Member = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => x.GM_Id);
                    table.ForeignKey(
                        name: "FK_GroupMember_Groups_GM_GroupId",
                        column: x => x.GM_GroupId,
                        principalTable: "Groups",
                        principalColumn: "G_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_Users_Gm_Member",
                        column: x => x.Gm_Member,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GroupTransaction",
                columns: table => new
                {
                    Gt_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gt_PaidBy = table.Column<int>(nullable: false),
                    Gt_ReceivedBy = table.Column<int>(nullable: false),
                    Gt_Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTransaction", x => x.Gt_Id);
                    table.ForeignKey(
                        name: "FK_GroupTransaction_Users_Gt_PaidBy",
                        column: x => x.Gt_PaidBy,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTransaction_Groups_Gt_ReceivedBy",
                        column: x => x.Gt_ReceivedBy,
                        principalTable: "Groups",
                        principalColumn: "G_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BillMember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bm_BillId = table.Column<int>(nullable: false),
                    Bm_Paidfor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillMember_IndividualBills_Bm_BillId",
                        column: x => x.Bm_BillId,
                        principalTable: "IndividualBills",
                        principalColumn: "Ib_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillMember_Users_Bm_Paidfor",
                        column: x => x.Bm_Paidfor,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_A_DoneBy",
                table: "Activity",
                column: "A_DoneBy");

            migrationBuilder.CreateIndex(
                name: "IX_BillMember_Bm_BillId",
                table: "BillMember",
                column: "Bm_BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillMember_Bm_Paidfor",
                table: "BillMember",
                column: "Bm_Paidfor");

            migrationBuilder.CreateIndex(
                name: "IX_FriendList_Friend_Id",
                table: "FriendList",
                column: "Friend_Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBills_Gb_ForGroup",
                table: "GroupBills",
                column: "Gb_ForGroup");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBills_Gb_PaidBy",
                table: "GroupBills",
                column: "Gb_PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_GM_GroupId",
                table: "GroupMember",
                column: "GM_GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_Gm_Member",
                table: "GroupMember",
                column: "Gm_Member");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_G_Admin",
                table: "Groups",
                column: "G_Admin");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransaction_Gt_PaidBy",
                table: "GroupTransaction",
                column: "Gt_PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTransaction_Gt_ReceivedBy",
                table: "GroupTransaction",
                column: "Gt_ReceivedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBills_Ib_PaidBy",
                table: "IndividualBills",
                column: "Ib_PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualTransaction_It_PaidBy",
                table: "IndividualTransaction",
                column: "It_PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_I_Sender",
                table: "Invitation",
                column: "I_Sender");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "BillMember");

            migrationBuilder.DropTable(
                name: "FriendList");

            migrationBuilder.DropTable(
                name: "GroupBills");

            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "GroupTransaction");

            migrationBuilder.DropTable(
                name: "IndividualTransaction");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "IndividualBills");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
