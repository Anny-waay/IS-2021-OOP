using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;
using Reports.DAL.Tools;

namespace Reports.Server.Services
{
    public interface IEmployeeTaskService
    {
        Task<List<EmployeeTask>> GetAll();
        Task<EmployeeTask> Create(string name, Guid employeeId, string description);

        Task<EmployeeTask> FindById(Guid id);

        Task<EmployeeTask> Delete(Guid id);

        Task Update(EmployeeTask entity);
        
        Task<List<EmployeeTask>> FindByEmployeeId(Guid employeeId);

        Task<List<EmployeeTask>> FindSubordinatesTasks(Guid leaderId);
    }
}