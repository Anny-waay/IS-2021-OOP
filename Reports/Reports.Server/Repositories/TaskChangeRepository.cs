using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Repositories
{
    public class TaskChangeRepository
    {
        private readonly ReportsDatabaseContext _context;
        
        public TaskChangeRepository(ReportsDatabaseContext context) {
            _context = context;
        }

        public async Task<List<TaskChange>> GetAllAsync()
        {
            return await _context.TaskChanges.ToListAsync();
        }
        public async Task AddAsync(TaskChange taskChange)
        {
            await _context.TaskChanges.AddAsync(taskChange);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeTask> FindEmployeeTasksAsync(Guid id)
        {
            return await _context.EmployeeTasks.FindAsync(id);
        }
    }
}