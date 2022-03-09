using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;
using Reports.Server.Repositories;

namespace Reports.Server.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportRepository _repository;

        public ReportService(ReportsDatabaseContext context) {
            _repository = new ReportRepository(context);
        }
        
        public async Task<List<EmployeeTask>> GetAllWeekTasks(Guid employeeId)
        {
            var allTasks = await _repository.GetAllWeekTasksAsync();
            var result = allTasks.FindAll(t => t.EmployeeId == employeeId);
            result = result.FindAll(t => DateTime.Compare(DateTime.Now, t.FinishDate.AddDays(7))<=0);
            return result;
        }

        public async Task<Report> Create(Guid employeeId, Guid taskId, string description)
        {
            var report = new Report(Guid.NewGuid(), employeeId, taskId, description);
            await _repository.AddAsync(report);
            return report;
        }

        public async Task<Report> FindByEmployeeId(Guid employeeId)
        {
            var allReports = await _repository.GetAllAsync();
            return allReports.Find(r => r.EmployeeId == employeeId); 
        }
        
        public async Task<List<Report>> FindSubordinatesReports(Guid leaderId)
        {
            List<Employee> allEmployees = await _repository.GetAllEmployeeAsync();
            List<Employee> subordinates = allEmployees.FindAll(e => e.LeaderId == leaderId);
            var result = new List<Report>();
            foreach (var employee in subordinates)
            {
                var report = await FindByEmployeeId(employee.Id);
                result.Add(report);
            }

            return result;
        }

        public async Task Update(Report entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}