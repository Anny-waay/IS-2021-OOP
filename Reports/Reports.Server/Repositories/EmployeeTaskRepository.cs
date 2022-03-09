using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Repositories
{
    public class EmployeeTaskRepository
    {
        private readonly ReportsDatabaseContext _context;
        
        public EmployeeTaskRepository(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<List<EmployeeTask>> GetAllAsync()
        {
            return await _context.EmployeeTasks.ToListAsync();
        }
        public async Task AddAsync(EmployeeTask employeeTask)
        {
            await _context.EmployeeTasks.AddAsync(employeeTask);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeTask> FindAsync(Guid id)
        {
            return await _context.EmployeeTasks.FindAsync(id);
        }

        public async Task DeleteAsync(EmployeeTask employeeTask)
        {
            await Task.Run(() =>  _context.EmployeeTasks.Remove(employeeTask));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeTask employeeTask)
        {
            _context.EmployeeTasks.Update(employeeTask);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees.ToListAsync();
        }
    }
}