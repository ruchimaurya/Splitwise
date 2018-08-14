using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("Activity")]
    public class Activity
    {

        [Key]
        public int A_Id { get; set; }

        public int A_DoneBy { get; set; }

        public string A_Description { get; set; }

        public DateTime A_Date { get; set; }

    }
}
