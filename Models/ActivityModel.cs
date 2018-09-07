using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Models
{
    public class ActivityModel
    {
        public int AM_Id { get; set; }

        public string AM_DoneBy { get; set; }

        public string AM_Description { get; set; }

        public string AM_ForFriend { get; set; }

        public string AM_ForGroup { get; set; }

        public DateTime AM_Date { get; set; }

        public bool AM_Deleted { get; set; }
    }
}
