﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Splitwise.SplitwiseDB;

namespace Splitwise.Migrations
{
    [DbContext(typeof(SplitwiseDbContext))]
    [Migration("20180911064447_DM_Delflag")]
    partial class DM_Delflag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Splitwise.Models.Activity", b =>
                {
                    b.Property<int>("A_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("A_Date");

                    b.Property<bool>("A_Deleted");

                    b.Property<string>("A_Description");

                    b.Property<int>("A_DoneBy");

                    b.Property<int?>("A_ForFriend");

                    b.Property<int?>("A_ForGroup");

                    b.HasKey("A_Id");

                    b.HasIndex("A_DoneBy");

                    b.HasIndex("A_ForFriend");

                    b.HasIndex("A_ForGroup");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Splitwise.Models.BillMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Bm_BillId");

                    b.Property<int>("Bm_Paidfor");

                    b.HasKey("Id");

                    b.HasIndex("Bm_BillId");

                    b.HasIndex("Bm_Paidfor");

                    b.ToTable("BillMember");
                });

            modelBuilder.Entity("Splitwise.Models.FriendList", b =>
                {
                    b.Property<int>("Fl_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Friend_Id");

                    b.Property<int>("User_Id");

                    b.HasKey("Fl_Id");

                    b.HasIndex("Friend_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("FriendList");
                });

            modelBuilder.Entity("Splitwise.Models.GroupBills", b =>
                {
                    b.Property<int>("Gb_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Gb_Amount");

                    b.Property<DateTime>("Gb_DateTime");

                    b.Property<bool>("Gb_Deleted");

                    b.Property<int>("Gb_ForGroup");

                    b.Property<string>("Gb_Name")
                        .IsRequired();

                    b.Property<int>("Gb_PaidBy");

                    b.Property<bool>("Gb_Settled");

                    b.HasKey("Gb_Id");

                    b.HasIndex("Gb_ForGroup");

                    b.HasIndex("Gb_PaidBy");

                    b.ToTable("GroupBills");
                });

            modelBuilder.Entity("Splitwise.Models.GroupMembers", b =>
                {
                    b.Property<int>("GM_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GM_GroupId");

                    b.Property<bool>("Gm_Deleted");

                    b.Property<int>("Gm_Member");

                    b.HasKey("GM_Id");

                    b.HasIndex("GM_GroupId");

                    b.HasIndex("Gm_Member");

                    b.ToTable("GroupMember");
                });

            modelBuilder.Entity("Splitwise.Models.Groups", b =>
                {
                    b.Property<int>("G_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("G_Admin");

                    b.Property<DateTime>("G_Date");

                    b.Property<bool>("G_Deleted");

                    b.Property<string>("G_Name")
                        .IsRequired();

                    b.HasKey("G_Id");

                    b.HasIndex("G_Admin");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Splitwise.Models.GroupTransaction", b =>
                {
                    b.Property<int>("Gt_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Gt_Amount");

                    b.Property<int>("Gt_PaidBy");

                    b.Property<int>("Gt_ReceivedBy");

                    b.HasKey("Gt_Id");

                    b.HasIndex("Gt_PaidBy");

                    b.HasIndex("Gt_ReceivedBy");

                    b.ToTable("GroupTransaction");
                });

            modelBuilder.Entity("Splitwise.Models.IndividualBills", b =>
                {
                    b.Property<int>("Ib_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Ib_Amount");

                    b.Property<DateTime>("Ib_DateTime");

                    b.Property<bool>("Ib_Deleted");

                    b.Property<string>("Ib_Name")
                        .IsRequired();

                    b.Property<int>("Ib_PaidBy");

                    b.Property<bool>("Ib_settled");

                    b.HasKey("Ib_Id");

                    b.HasIndex("Ib_PaidBy");

                    b.ToTable("IndividualBills");
                });

            modelBuilder.Entity("Splitwise.Models.IndividualTransaction", b =>
                {
                    b.Property<int>("It_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("It_Amount");

                    b.Property<int>("It_BillId");

                    b.Property<bool>("It_Delete");

                    b.Property<int>("It_PaidBy");

                    b.HasKey("It_Id");

                    b.HasIndex("It_PaidBy");

                    b.ToTable("IndividualTransaction");
                });

            modelBuilder.Entity("Splitwise.Models.Invitation", b =>
                {
                    b.Property<int>("I_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("I_Email")
                        .IsRequired();

                    b.Property<int>("I_Sender");

                    b.HasKey("I_Id");

                    b.HasIndex("I_Sender");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("Splitwise.Models.Transactions", b =>
                {
                    b.Property<int>("T_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("T_Amount");

                    b.Property<DateTime>("T_DateTime");

                    b.Property<bool>("T_Deleted");

                    b.Property<int>("T_PaidBy");

                    b.Property<int?>("T_ReceivedByFriend");

                    b.Property<int?>("T_ReceivedByGroup");

                    b.HasKey("T_Id");

                    b.HasIndex("T_ReceivedByFriend");

                    b.HasIndex("T_ReceivedByGroup");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Splitwise.Models.Users", b =>
                {
                    b.Property<int>("U_Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("U_Contact");

                    b.Property<bool>("U_Deleted");

                    b.Property<string>("U_Email")
                        .IsRequired();

                    b.Property<string>("U_Name")
                        .IsRequired();

                    b.Property<string>("U_Password")
                        .IsRequired();

                    b.HasKey("U_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Splitwise.Models.Activity", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("Activities")
                        .HasForeignKey("A_DoneBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("Activity")
                        .HasForeignKey("A_ForFriend");

                    b.HasOne("Splitwise.Models.Groups")
                        .WithMany("Activity")
                        .HasForeignKey("A_ForGroup");
                });

            modelBuilder.Entity("Splitwise.Models.BillMember", b =>
                {
                    b.HasOne("Splitwise.Models.IndividualBills")
                        .WithMany("BillMembers")
                        .HasForeignKey("Bm_BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("BillMembers")
                        .HasForeignKey("Bm_Paidfor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.FriendList", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("FriendListFriend")
                        .HasForeignKey("Friend_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("FriendListUser")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.GroupBills", b =>
                {
                    b.HasOne("Splitwise.Models.Groups")
                        .WithMany("GroupBills")
                        .HasForeignKey("Gb_ForGroup")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("GroupBills")
                        .HasForeignKey("Gb_PaidBy")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.GroupMembers", b =>
                {
                    b.HasOne("Splitwise.Models.Groups")
                        .WithMany("GroupMembers")
                        .HasForeignKey("GM_GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("GroupMembers")
                        .HasForeignKey("Gm_Member")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.Groups", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("Group")
                        .HasForeignKey("G_Admin")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.GroupTransaction", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("GetGroupTransactions")
                        .HasForeignKey("Gt_PaidBy")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Splitwise.Models.Groups")
                        .WithMany("GroupTransactions")
                        .HasForeignKey("Gt_ReceivedBy")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.IndividualBills", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("IndividualBills")
                        .HasForeignKey("Ib_PaidBy")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.IndividualTransaction", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("IndividualTransactions")
                        .HasForeignKey("It_PaidBy")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.Invitation", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("Invitations")
                        .HasForeignKey("I_Sender")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Splitwise.Models.Transactions", b =>
                {
                    b.HasOne("Splitwise.Models.Users")
                        .WithMany("Transactions")
                        .HasForeignKey("T_ReceivedByFriend");

                    b.HasOne("Splitwise.Models.Groups")
                        .WithMany("Transactions")
                        .HasForeignKey("T_ReceivedByGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
