using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Tools;

namespace Reports.Server.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();
        Task<Employee> Create(string name, EmployeeStatus status, Guid leaderId);

        Task<Employee> FindById(Guid id);

        Task Delete(Guid id);

        Task Update(Employee employee);
    }
}