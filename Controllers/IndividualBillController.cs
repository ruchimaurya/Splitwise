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
    public class IndividualBillController : Controller
    {
        private readonly SplitwiseDbContext context;
        IIndividualBillRepository _iBillRepo;       

        public IndividualBillController(SplitwiseDbContext context, IIndividualBillRepository iBillRepo)
        {
            this.context = context;
            _iBillRepo = iBillRepo;
        }

        [Route("api/individualbills")]
        [HttpGet]
        public IEnumerable<IndividualBills> GetIndividualBills()
        {
            var bills = _iBillRepo.GetIndividualBills();
            return bills;
        }

        [Route("api/individualbills/")]
        [HttpPost]
        public IActionResult AddIndividualBill([FromBody]IndividualBillDataModel model)
        {
            int res = _iBillRepo.AddIndividualBill(model);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/individualbills/{id}")]
        [HttpGet]
        public BillInformation GetIndividualBillInfo(int id)
        {

            var bill = _iBillRepo.GetIndividualBillInfo(id);
            return bill;
        }

        [Route("api/individualbills/{id}")]
        [HttpDelete]
        public IActionResult DeleteIndividualBill(int id)
        {
            int res = _iBillRepo.DeleteIndividualBill(id);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }
   
        [Route("api/friendsbills/{uid}/{fid}")]
        [HttpGet]
        public IEnumerable<BillInformation> GetFriendsBills(int uid, int fid)
        {
            var res = _iBillRepo.GetFriendsBills(uid,fid);
            return res;
        }

    }
}
