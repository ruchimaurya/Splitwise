using Splitwise.Models;
using Microsoft.EntityFrameworkCore;

namespace Splitwise.SplitwiseDB
{
    public class SplitwiseDbContext :DbContext
    {

        public SplitwiseDbContext(DbContextOptions<SplitwiseDbContext> options)
             : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Invitation> Invitation { get; set; }
        public DbSet<BillMember> BillMembers { get; set; }
        public DbSet<FriendList> FriendList { get; set; }
        public DbSet<GroupBills> GroupBills { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<GroupTransaction> GroupTransactions { get; set; }
        public DbSet<IndividualBills> IndividualBills { get; set; }
        public DbSet<IndividualTransaction> IndividualTransactions { get; set; }

    }
}
