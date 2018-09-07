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
    public class FriendController : Controller
    {
        private readonly SplitwiseDbContext context;
        IFriendRepository _frdRepo;

        public FriendController(SplitwiseDbContext context, IFriendRepository frdRepo)
        {
            this.context = context;
            _frdRepo = frdRepo;
        }

        [Route("api/friends")]
        [HttpPost]
        public IActionResult AddFriend([FromBody]FriendList friend)
        {
            int res = _frdRepo.AddFriend(friend);
            if (res != 0)
            {
                return Ok();
            }
            return Forbid();
        }

        [Route("api/friends/invite")]
        [HttpPost]
        public IActionResult InviteFriend([FromBody]Invitation inv)
        {
            int res = _frdRepo.InviteFriend(inv);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/friends")]
        [HttpGet]
        public IEnumerable<FriendList> GetAllFriends()
        {
            var frds = _frdRepo.GetAllFrds();
            return frds;
        }

        [Route("api/friends/{id}")]
        [HttpGet]
        public IActionResult GetFriends(int id)
        {
            var friends = _frdRepo.GetFriends(id);
            if (friends == null)
            {
                return NotFound();
            }
            return Ok(friends);
        }

        [Route("api/friends/{uid}/{fid}")]
        [HttpGet]
        public IActionResult CheckFriend(int uid,int fid)
        {
            var friends = _frdRepo.CheckFriend(uid,fid);
            if (friends > 0)
            {
                return Ok(friends);
            }
            return Ok(0);
        }

        [Route("api/friend/{fid}")]
        [HttpGet]
        public IActionResult GetFriend(int fid)
        {
            var friend = _frdRepo.GetFriend(fid);
            if (friend == null)
            {
                return NotFound();
            }
            return Ok(friend);
        }

        [Route("api/friends/{uid}/{fid}")]
        [HttpDelete]
        public IActionResult RemoveFriend(int uid,int fid)
        {
            int res = _frdRepo.RemoveFriend(uid, fid);
            if (res != 0)
            {
                return Ok(res);
            }
            return NotFound();
        }
    }
}
