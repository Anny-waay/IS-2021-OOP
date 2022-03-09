using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Repositories
{
    public class EmployeeRepository
    {
        private readonly ReportsDatabaseContext _context;
        
        public EmployeeRepository(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> FindAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task DeleteAsync(Employee employee)
        {
            await Task.Run(() =>  _context.Employees.Remove(employee));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}