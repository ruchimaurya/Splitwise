using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IGroupRepository
    {
        IEnumerable<Groups> GetGroups();
        IEnumerable<Groups> GetUsersGroups(int id);
        int CreateGroup(Groups group);
        Groups GetGroup(int id);
        int UpdatGroup(int id, Groups b);
        int DeleteGroup(int id);
        IEnumerable<GroupMembersModel> GetGroupMembers(int id);
        int AddMemberToGroup(GroupMembers gm);
        int RemoveMemberFromGroup(int gid,int mid);
        GroupInformation GetGroupInformation(int id);
    }
}
