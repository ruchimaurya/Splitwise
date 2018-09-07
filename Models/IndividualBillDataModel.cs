using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Splitwise.Models
{   
    public class IndividualBillDataModel
    {      
        public int Ib_Id { get; set; }

        public string Ib_Name { get; set; }

        public int Ib_PaidBy { get; set; }

        public double Ib_Amount { get; set; }

        public List<int> billMembers { get; set; }

    }
}
