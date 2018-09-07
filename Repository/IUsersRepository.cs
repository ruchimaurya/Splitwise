using Microsoft.AspNetCore.Mvc;
using Splitwise.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IUsersRepository
    {
        IEnumerable<Users> GetUsers();
        int CreateUser(Users user);
        int UserLogin(string email, string password);
        Users GetUser(int id);
        int UpdatUser(int id, Users b);
        int DeleteUser(int id);
        IEnumerable<Groups> GetUsersGroup(int id);
        IEnumerable<FriendTransactionModel> GetUsersTransaction(int id);
        IEnumerable<ActivityModel> GetUsersActivity(int id);
        int GetUidFromEmail(string email);
    }
}
