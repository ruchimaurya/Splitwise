using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Models
{
    public class GroupInformation
    {
        public int Gi_GroupId { get; set; }

        public string Gi_Name { get; set; }

        public string Gi_Admin { get; set; }

        public DateTime Gi_Date { get; set; }

        public List<string> Gi_Members { get; set; }

        public List<BillInformation> Gi_Bills { get; set; }
    }
}
