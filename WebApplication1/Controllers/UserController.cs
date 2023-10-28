using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Requests;
using WebApplication1.Responses;
using WebApplication1.Utility;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MSSQLDbContext _context;

        private DbSet<User> Users;

        public UserController(MSSQLDbContext context)
        {
            _context = context;
            Users = context.Users;
        }

        [HttpPost("register")]
        public ActionResult<List<RegisterStatus>> Register(UserRegister register)
        {
            var statuses = new List<RegisterStatus>();

            var response = new RegisterResponse { Status = statuses }; 

            if (Users.Where(u => u.Email == register.Email).Any())
                statuses.Add(RegisterStatus.EMAILEXISTS);

            if (Users.Where(u => u.Username == register.Username).Any())
                statuses.Add(RegisterStatus.USERNAMEEXISTS);

            if (statuses.Any())
                return BadRequest(statuses);

            User user = new User
            {
                Email = register.Email,
                Username = register.Username,
                Password = PasswordHasher.Hash(register.Password)
            };

            Users.Add(user);
            _context.SaveChanges();

            statuses.Add(RegisterStatus.OK);

            return Ok(statuses);
         
        }

        [HttpPost("login")]
        public ActionResult<LoginStatus> Login(UserLogin login)
        {
            var user = _context.Users.Where(u => u.Username == login.Login || u.Username == login.Login).FirstOrDefault();

            if (user == null)
                return BadRequest(new LoginResponse { Status = LoginStatus.INVALIDUSER });

            if (!PasswordHasher.CheckValidity(user.Password, login.Password))
                return BadRequest(new LoginResponse { Status = LoginStatus.INVALIDPASSWORD });

            return Ok(new LoginResponse { Status = LoginStatus.OK });
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(Users.ToList());
        }
    }
}
