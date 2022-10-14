using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Model;
using GeneralDynamics.AI.Transversal.Factorias;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IRepository<User> _repository;
        private IUserService _userService = null;

        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        public UserController(IRepository<User> repository)
        {
            _repository = repository;
            _userService = FactoryManager.GetInstance<IUserService>();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var data = await _userService.GetAllUsers();

        //    return Ok(data);
        //}
        
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var currentUser = GetCurrentUser();

            return Ok(currentUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {

            var data = await _userService.GetUserById(id);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (user.Name == string.Empty || user.LastName == string.Empty)
            {
                ModelState.AddModelError("Name", "Name or LastName shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.AddUser(user);

            return Ok(data);
        }


        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> AddUsers([FromBody] IEnumerable<User> users)
        {
            if (users == null)
                return BadRequest();

            if (users.ToList().Count <= 0)
            {
                ModelState.AddModelError("Users", "Users shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.AddUsers(users);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        // [Route("roles")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("User", "User shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.RemoveUser(id);

            return Ok(data);
        }

        [HttpDelete]
        [Route("users")]
        public async Task<IActionResult> RemoveUsers([FromBody] IEnumerable<int> ids)
        {
            if (ids == null)
                return BadRequest();

            if (ids.ToList().Count <= 0)
            {
                ModelState.AddModelError("Users", "Users shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _userService.RemoveUsers(ids);

            return Ok(data);
        }

        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDTO
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }

            return null;
        }


    }
}
