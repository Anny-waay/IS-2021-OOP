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
    [Route("/reports")]
    public class ReportController: ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("get-all-week-tasks")]
        public async Task<IActionResult> GetAllWeekTasks(Guid employeeId)
        {
            var employeeTasks = _service.GetAllWeekTasks(employeeId);
            return Ok(employeeTasks);
        }
        
        [HttpPost("create")]
        public async Task<Report> Create([FromQuery] Guid employeeId, [FromQuery] Guid taskId, [FromQuery] string description)
        {
            return await _service.Create(employeeId, taskId, description);
        }

        [HttpGet("find-employeeId")]
        public async Task<IActionResult> FindEmployeeId([FromQuery] Guid employeeId)
        {
            var result = await _service.FindByEmployeeId(employeeId);
            if (result != null)
            {
                return Ok(result);
            }
        
            return NotFound();
        }
        
        [HttpGet("find-subordinates-reports")]
        public async Task<IActionResult> FindSubordinatesReports([FromQuery] Guid leaderId)
        {
            var result = await _service.FindSubordinatesReports(leaderId);
            if (result != new List<Report>())
            {
                return Ok(result);
            }
        
            return NotFound();
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromQuery] Report entity)
        {
            await _service.Update(entity);
            return Ok(entity);
        }
    }
}