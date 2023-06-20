using CodeFirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserContext _userContext;

        public UsersController(UserContext userContext)
        {
            _userContext = userContext;
        }


        [HttpGet]
        [Route("GetUsers")]
        public List<Users> GetUsers()
        {
            return _userContext.Users.ToList();
        }


        [HttpGet]
        [Route("getuser")]
        public Users GetUser(int id)
        {
            return _userContext.Users.Where(x => x.ID == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("AddUser")]
        public string AddUser(Users users)
        {
            string response = string.Empty;
            _userContext.Users.Add(users);
            _userContext.SaveChanges();
            return "Users added";
        }


        [HttpPut]
        [Route("UpdateUser")]
        public string UpdateUser(Users users)
        {
            _userContext.Entry(users).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _userContext.SaveChanges();
            return "User updated";
        }


        [HttpDelete]
        [Route("deleteUser")]
        public string DeleteUser(int id)
        {
            Users users = _userContext.Users.Where(u => u.ID == id).FirstOrDefault();
            if (users != null)
            {
                _userContext.Users.Remove(users);
                _userContext.SaveChanges();
                return "User deleted";
            }
            else
            {
                return "No user found";
            }
        }

    }
}
