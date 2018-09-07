using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Models
{
    public class BillInformation
    {
        public int BI_Id { get; set; }

        public string BI_Name { get; set; }

        public string BI_PaidBy { get; set; }

        public DateTime BI_Date { get; set; }

        public double BI_Amount { get; set; }

        public List<string> BI_PaidFor { get; set; }
    }
}
