using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reports.DAL.Entities;

namespace Reports.Server.Services
{
    public interface ITaskChangeService
    {
        Task<List<TaskChange>> GetAll();
        Task<TaskChange> Create(Guid taskId, string comment);
        Task<List<TaskChange>> FindByTaskId(Guid taskId);
        Task<List<EmployeeTask>> FindTasks(DateTime date);
    }
}