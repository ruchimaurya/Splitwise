using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    public class GroupSettlementModel
    {
        public int Gs_PayerId { get; set; }

        public string Gs_PayerName { get; set; }

        public double Gs_Amount { get; set; }
               
    }
}
