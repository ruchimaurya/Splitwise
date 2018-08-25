using Microsoft.AspNetCore.Mvc;
using Splitwise.Models;
using Splitwise.Repository;
using Splitwise.SplitwiseDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Controllers
{
    public class GroupBillController : Controller
    {
        private readonly SplitwiseDbContext context;
        IGroupBillRepository _gBillRepo;

        public GroupBillController(SplitwiseDbContext context, IGroupBillRepository gBillRepo)
        {
            this.context = context;
            _gBillRepo = gBillRepo;
        }

        [Route("api/groupbills")]
        [HttpGet]
        public IEnumerable<GroupBills> GetAllGroupBills()
        {
            var bills = _gBillRepo.GetGroupBills();
            return bills;
        }

        [Route("api/groupbills")]
        [HttpPost]
        public IActionResult AddGroupBill([FromBody]GroupBills gBill)
        {
            int res = _gBillRepo.AddGroupBill(gBill);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/groupbills/{id}")]
        [HttpPut]
        public IActionResult UpdateGroupBill(int id, [FromBody]GroupBills gBill)
        {
            if (id == gBill.Gb_Id)
            {
                int res = _gBillRepo.UpdatGroupBill(id, gBill);
                if (res != 0)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            return NotFound();
        }

        [Route("api/groupBills/{id}")]
        [HttpDelete]
        public IActionResult DeleteGroupBill(int id)
        {
            int res = _gBillRepo.DeleteGroupBill(id);
            if (res != 0)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [Route("api/groupbills/{id}")]
        [HttpGet]
        public IEnumerable<GroupBills> GetIndividualGroupBills(int id)
        {
            var bills = _gBillRepo.GetIndividualGroupBills(id);
            return bills;
        }

        [Route("api/groupbills/bill/{id}")]
        [HttpGet]
        public BillInformation GetBillInfo(int id)
        {
            var bill = _gBillRepo.GetBillInfo(id);
            return bill;
        }
    }
}
