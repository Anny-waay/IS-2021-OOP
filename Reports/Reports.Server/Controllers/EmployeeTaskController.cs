using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks")]
    public class EmployeeTaskController : ControllerBase
    {
        private readonly IEmployeeTaskService _service;

        public EmployeeTaskController(IEmployeeTaskService service)
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
        public async Task<EmployeeTask> Create([FromQuery] string name, [FromQuery] Guid employeeId, [FromQuery] string description)
        {
            return await _service.Create(name, employeeId, description);
        }

        [HttpGet("find-id")]
        public async Task<IActionResult> FindId([FromQuery] Guid id)
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
        
        [HttpGet("find-date")]
        public async Task<IActionResult> FindDate([FromQuery] DateTime date)
        {
            var allEmployeeTasks = _service.GetAll();
            var result = allEmployeeTasks.Result
                .FindAll(t => t.StartDate == date);
            if (result != new List<EmployeeTask>())
            {
               return Ok(result);
            }
        
            return NotFound();
        }
        
        [HttpGet("find-employeeId")]
        public async Task<IActionResult> FindEmployeeId([FromQuery] Guid employeeId)
        {
            var result = await _service.FindByEmployeeId(employeeId);
            if (result != new List<EmployeeTask>())
            {
                return Ok(result);
            }
        
            return NotFound();
        }
        
        [HttpGet("find-subordinates-tasks")]
        public async Task<IActionResult> FindSubordinatesTasks([FromQuery] Guid leaderId)
        {
            var result = await _service.FindSubordinatesTasks(leaderId);
            if (result != new List<EmployeeTask>())
            {
                return Ok(result);
            }
        
            return NotFound();
        }
        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var employeeTask = await _service.Delete(id);
            return Ok(employeeTask);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromQuery] EmployeeTask entity)
        {
            await _service.Update(entity);
            return Ok(entity);
        }
    }
}