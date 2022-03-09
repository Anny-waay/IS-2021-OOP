using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Repositories
{
    public class ReportRepository
    {
        private readonly ReportsDatabaseContext _context;
        
        public ReportRepository(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await _context.Reports.ToListAsync();
        }
        public async Task<List<EmployeeTask>> GetAllWeekTasksAsync()
        {
            return await _context.EmployeeTasks.ToListAsync();
        }
        
        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        public async Task AddAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
    }
}