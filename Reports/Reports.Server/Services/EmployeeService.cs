using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reports.DAL.Entities;
using Reports.DAL.Tools;
using Reports.Server.Controllers;
using Reports.Server.Database;
using Reports.Server.Repositories;

namespace Reports.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(ReportsDatabaseContext context) {
            _repository = new EmployeeRepository(context);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Employee> Create(string name, EmployeeStatus status, Guid leaderId)
        {
            var employeeLeader = await _repository.FindAsync(leaderId);
            if (employeeLeader == null || employeeLeader.Status > status || status == EmployeeStatus.TeamLeader)
            {
                leaderId = Guid.Empty;
            }
            var employee = new Employee(Guid.NewGuid(), name, status, leaderId);
            await _repository.AddAsync(employee);
            return employee;
        }
        
        public async Task<Employee> FindById(Guid id)
        {
           return await _repository.FindAsync(id);
        }

        public async Task Delete(Guid id)
        {
            var employee = await _repository.FindAsync(id);
            await _repository.DeleteAsync(employee);
        }

        public async Task Update(Employee employee)
        {
            _repository.UpdateAsync(employee);
        }
    }
}