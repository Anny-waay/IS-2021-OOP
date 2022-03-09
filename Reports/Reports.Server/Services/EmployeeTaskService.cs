using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.Server.Database;
using Reports.Server.Repositories;

namespace Reports.Server.Services
{
    public class EmployeeTaskService : IEmployeeTaskService
    {
        private readonly EmployeeTaskRepository _repository;

        public EmployeeTaskService(ReportsDatabaseContext context) {
            _repository = new EmployeeTaskRepository(context);
        }

        public async Task<List<EmployeeTask>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
        public async  Task<EmployeeTask> Create(string name, Guid employeeId, string description)
        {
            var employeeTask = new EmployeeTask(Guid.NewGuid(), name, employeeId, description);
            await _repository.AddAsync(employeeTask);
            return employeeTask;
        }

        public async Task<EmployeeTask> FindById(Guid id)
        {
            return await _repository.FindAsync(id);
        }

        public async Task<EmployeeTask> Delete(Guid id)
        {
            var employeeTask = await _repository.FindAsync(id);
            await _repository.DeleteAsync(employeeTask);
            return employeeTask;
        }

        public async Task Update(EmployeeTask entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<List<EmployeeTask>> FindByEmployeeId(Guid employeeId)
        {
            var allEmployeeTasks = await _repository.GetAllAsync();
            return allEmployeeTasks.FindAll(t => t.EmployeeId == employeeId); 
        }
        
        public async Task<List<EmployeeTask>> FindSubordinatesTasks(Guid leaderId)
        {
            List<Employee> allEmployees = await _repository.GetAllEmployeeAsync();
            List<Employee> subordinates = allEmployees.FindAll(e => e.LeaderId == leaderId);
            var result = new List<EmployeeTask>();
            foreach (var employee in subordinates)
            {
                var employeeTasks = await FindByEmployeeId(employee.Id);
                foreach (var task in employeeTasks)
                {
                    result.Add(task);
                }
            }

            return result;
        }
    }
}