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
        Users GetUser(int id);
        int UpdatUser(int id, Users b);
        int DeleteUser(int id);
        IEnumerable<Groups> GetUsersGroup(int id);
        IEnumerable<Transactions> GetUsersTransaction(int id);
        IEnumerable<Activity> GetUsersActivity(int id);
    }
}
