using Microsoft.AspNetCore.Mvc;
using Splitwise.Models;
using Splitwise.Repository;
using Splitwise.SplitwiseDB;
using System.Collections.Generic;

namespace Splitwise.Controllers
{
    public class TransactionController:Controller
    {
        private readonly SplitwiseDbContext context;
        ITransactionRepository _tranRepo;

        public TransactionController(SplitwiseDbContext context, ITransactionRepository tranRepo)
        {
            this.context = context;
            _tranRepo = tranRepo;
        }

        [Route("api/transactions/")]
        [HttpGet]
        public IEnumerable<Transactions> GetAllTransactions()
        {
            var alltrans = _tranRepo.GetAllTransactions();
            return alltrans;
        }


        [Route("api/transactions/users/{id}")]
        [HttpGet]
        public IEnumerable<Transactions> GetUSersAllTransactions(int id)
        {
            var alltrans = _tranRepo.GetUsersAllTransactions(id);
            return alltrans;
        }

        [Route("api/transactions/groups/{gid}")]
        [HttpGet]
        public IEnumerable<FriendTransactionModel> GetGroupsAllTransactions(int gid)
        {
            var grptrans = _tranRepo.GetGroupsAllTransactions(gid);
            return grptrans;
        }


        [Route("api/transactions/friends/{uid}/{fid}")]
        [HttpGet]
        public IEnumerable<FriendTransactionModel> GetIndividualTransactions(int uid, int fid)
        {
            var frdtrans = _tranRepo.GetIndividualTransactions(uid,fid);
            return frdtrans;
        }

        [Route("api/transactions/")]
        [HttpPost]
        public IActionResult AddTransaction([FromBody]Transactions trans)
        {
            int res = _tranRepo.AddTransaction(trans);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/transactions/{id}")]
        [HttpDelete]
        public IActionResult DeleteTransaction(int id)
        {
            int res = _tranRepo.DeleteTransaction(id);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }


        [Route("api/transactions/{id}")]
        [HttpPut]
        public IActionResult UpdateTransaction(int id, [FromBody]Transactions trans)
        {
            if (id == trans.T_Id)
            {
                int res = _tranRepo.UpdateTransaction(id, trans);
                if (res != 0)
                {
                    return Ok(res);
                }
                return NotFound(res);
            }
            return NotFound();
        }

        [Route("api/transactions/friends/settlement/{uid}/{fid}")]
        [HttpGet]
        public IActionResult GetSettlementIndividual(int uid,int fid)
        {
                var res = _tranRepo.GetSettlementIndividual(uid, fid);
                
                    return Ok(res);
        }

        [Route("api/transactions/groups/settlement/{id}")]
        [HttpGet]
        public IActionResult GetSettlementGroup(int id)
        {
             var res = _tranRepo.GetSettlementGroup(id);

            return Ok(res);
        }


    }
}
