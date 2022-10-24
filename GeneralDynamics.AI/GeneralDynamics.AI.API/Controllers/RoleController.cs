using GeneralDynamics.AI.Application.Services;
using GeneralDynamics.AI.Data;
using GeneralDynamics.AI.Data.Repository;
using GeneralDynamics.AI.Transversal.Factorias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralDynamics.AI.API.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {

        private readonly IRepository<Role> _repository;
        private IRoleService _roleService = null;

        public RoleController(IRepository<Role> repository)
        {
            _repository = repository;
            _roleService = FactoryManager.GetInstance<IRoleService>();
        }

        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var data = await _roleService.GetAllRoles();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {

            var data = await _roleService.GetRoleById(id);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            if (role == null)
                return BadRequest();

            if(role.Code == string.Empty || role.Description == string.Empty)
            {
                ModelState.AddModelError("Code", "Code or Description shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _roleService.AddRole(role);

            return Ok(data);
        }

        
        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> AddRoles([FromBody] IEnumerable<Role> roles)
        {
            if (roles == null)
                return BadRequest();

            if (roles.ToList().Count <= 0)
            {
                ModelState.AddModelError("Roles", "Roles shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _roleService.AddRoles(roles);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRole(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("Roles", "Roles shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _roleService.RemoveRole(id);

            return Ok(data);
        }

        [HttpDelete]
        [Route("roles")]
        public async Task<IActionResult> RemoveRoles([FromBody] IEnumerable<int> ids)
        {
            if (ids == null)
                return BadRequest();

            if (ids.ToList().Count <= 0)
            {
                ModelState.AddModelError("Roles", "Roles shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _roleService.RemoveRoles(ids);

            return Ok(data);
        }

        // TODO Implement find with specification
    }
}
