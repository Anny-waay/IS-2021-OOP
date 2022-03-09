using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.Server.Database;
using Reports.Server.Repositories;

namespace Reports.Server.Services
{
    public class TaskChangeService : ITaskChangeService
    {
        private readonly TaskChangeRepository _repository;

        public TaskChangeService(ReportsDatabaseContext context) {
            _repository = new TaskChangeRepository(context);
        } 
        
        public async Task<List<TaskChange>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TaskChange> Create(Guid taskId, string comment)
        {
            var taskChange = new TaskChange(Guid.NewGuid(), taskId, comment);
            await _repository.AddAsync(taskChange);
            return taskChange;
        }

        public async Task<List<TaskChange>> FindByTaskId(Guid taskId)
        {
            var allTaskChanges = await GetAll();
            return allTaskChanges.FindAll(t => t.TaskId == taskId);
        }
        public async Task<List<EmployeeTask>> FindTasks(DateTime date)
        {
            var allTaskChanges = GetAll();
            var taskChanges = allTaskChanges.Result
                .FindAll(t => t.Date == date);
            var result = new List<EmployeeTask>();
            foreach (TaskChange change in taskChanges)
            {
                var employeeTask = await _repository.FindEmployeeTasksAsync(change.TaskId);
                result.Add(employeeTask);
            }

            return result;
        }
    }
}