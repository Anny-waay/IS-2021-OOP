using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/tasks-change")]
    public class TaskChangeController : ControllerBase
    {
        private readonly ITaskChangeService _service;

        public TaskChangeController(ITaskChangeService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var allTaskChanges = _service.GetAll();
            return Ok(allTaskChanges);
        }
        
        [HttpPost("create")]
        public async Task<TaskChange> Create([FromQuery] Guid taskId, [FromQuery] string comment)
        {
            return await _service.Create(taskId, comment);
        }

        [HttpGet("find-by-taskId")]
        public async Task<IActionResult> FindByTaskId([FromQuery] Guid taskId)
        {
            var result = _service.FindByTaskId(taskId).Result;
            if (result != new List<TaskChange>())
            {
                return Ok(result);
            }
        
            return NotFound();
        }
        
        [HttpGet("find-tasks-by-date-of-change")]
        public async Task<IActionResult> FindTasksByDate([FromQuery] DateTime date)
        {
            var result = await _service.FindTasks(date);
            if (result != new List<EmployeeTask>())
            {
                return Ok(result);
            }
        
            return NotFound();
        }
    }
}