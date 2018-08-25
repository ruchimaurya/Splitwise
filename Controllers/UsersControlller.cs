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
    public class UsersControlller : Controller
    {      
        IUsersRepository _userRepo;

        public UsersControlller(IUsersRepository ur, SplitwiseDbContext context)
        {
            _userRepo = ur;      
        }

        [Route("api/users")]
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            var users = _userRepo.GetUsers();
            return users;
            //await mapper.Map<IEnumerable<Users>, IEnumerable<Users>>(users);
        }

        [Route("api/users")]
        [HttpPost]
        public IActionResult CreateUser([FromBody]Users user)
        {
            int res = _userRepo.CreateUser(user);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/users/{id}")]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            var user = _userRepo.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Route("api/users/{id}")]
        [HttpPut]
        public IActionResult UpdateUser(int id, [FromBody]Users user)
        {
            if (id == user.U_Id)
            {
                int res = _userRepo.UpdatUser(id, user);
                if (res != 0)
                {
                    return Ok(res);
                }
                return NotFound(res);
            }
            return NotFound();
        }

        [Route("api/users/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            int res = _userRepo.DeleteUser(id);
            if (res != 0)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [Route("api/usersgroups/{id}")]
        [HttpGet]
        public IActionResult GetUsersGroups(int id)
        {
            var groups = _userRepo.GetUsersGroup(id);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        [Route("api/users/transactions/{id}")]
        [HttpGet]
        public IActionResult GetUsersTransactions(int id)
        {
            var trans = _userRepo.GetUsersTransaction(id);
            if (trans == null)
            {
                return NotFound();
            }
            return Ok(trans);
        }

        [Route("api/users/activity/{id}")]
        [HttpGet]
        public IActionResult GetUsersActivity(int id)
        {
            var act = _userRepo.GetUsersActivity(id);
            if (act == null)
            {
                return NotFound();
            }
            return Ok(act);
        }

    }
}
