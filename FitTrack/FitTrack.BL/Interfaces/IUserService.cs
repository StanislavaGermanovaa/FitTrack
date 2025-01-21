using FitTrack.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.BL.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(string id);
        void CreateUser(User user);
        void DeleteUser(string id);
    }
}
