using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface IReportService
    {
        Task<List<EmployeeTask>> GetAllWeekTasks(Guid employeeId);
        Task<Report> Create(Guid employeeId, Guid taskId, string description);
        Task<Report> FindByEmployeeId(Guid employeeId);
        Task<List<Report>> FindSubordinatesReports(Guid leaderId);
        Task Update(Report entity);
    }
}