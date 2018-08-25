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
    public class GroupController : Controller
    {
        private readonly SplitwiseDbContext context;
        IGroupRepository _groupRepo;

        public GroupController(IGroupRepository gr, SplitwiseDbContext context)
        {
            _groupRepo = gr;
            this.context = context;
        }

        [Route("api/groups")]
        [HttpGet]
        public IEnumerable<Groups> GetGroups()
        {
            var groups = _groupRepo.GetGroups();
            return groups;
            //await mapper.Map<IEnumerable<Users>, IEnumerable<Users>>(users);
        }

        [Route("api/groups")]
        [HttpPost]
        public IActionResult CreateGroup([FromBody]Groups group)
        {
            int res = _groupRepo.CreateGroup(group);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/groups/{id}")]
        [HttpGet]
        public IActionResult GetGroup(int id)
        {
            var group = _groupRepo.GetGroup(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }


        [Route("api/groups/info/{id}")]
        [HttpGet]
        public IActionResult GetGroupinfo(int id)
        {
            var group = _groupRepo.GetGroupInformation(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [Route("api/groups/{id}")]
        [HttpPut]
        public IActionResult UpdateGroup(int id, [FromBody]Groups group)
        {
            if (id == group.G_Id)
            {
                int res = _groupRepo.UpdatGroup(id, group);
                if (res != 0)
                {
                    return Ok(res);
                }
                return NotFound();
            }
            return NotFound();
        }

        [Route("api/groups/{id}")]
        [HttpDelete]
        public IActionResult DeleteGroup(int id)
        {
            int res = _groupRepo.DeleteGroup(id);
            if (res != 0)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [Route("api/groupmembers/{id}")]
        [HttpGet]
        public IActionResult GetGroupMembers(int id)
        {
            var members = _groupRepo.GetGroupMembers(id);
            if (members == null)
            {
                return NotFound();
            }
            return Ok(members);
        }

        [Route("api/groupmembers")]
        [HttpPost]
        public IActionResult AddGroupMember([FromBody]GroupMembers gm)
        {
            int res = _groupRepo.AddMemberToGroup(gm);
            if (res != 0)
            {
                return Ok(res);
            }
            return Forbid();
        }

        [Route("api/groupmembers/{gid}/{mid}")]
        [HttpDelete]
        public IActionResult DeleteGroupMember(int gid,int mid)
        {
            int res = _groupRepo.RemoveMemberFromGroup(gid,mid);
            if (res != 0)
            {
                return Ok(res);
            }
            return NotFound();
        }
    }
}
