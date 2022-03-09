using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var allEmployeeTasks = _service.GetAll();
            return Ok(allEmployeeTasks);
        }
        
        [HttpPost("create")]
        public async Task<Employee> Create([FromQuery] string name, [FromQuery] EmployeeStatus status, [FromQuery] Guid leaderId)
        {
            return await _service.Create(name, status, leaderId);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _service.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromQuery] Employee entity)
        {
            await _service.Update(entity);
            return Ok(entity);
        }
    }
}