using System;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class EmployeeTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TaskState State { get; set; }
        public Guid EmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime FinishDate { get; set; }

        public EmployeeTask()
        {
        }

        public EmployeeTask(Guid id, string name, Guid employeeId, string description)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId),"EmployeeId is invalid");
            }
            
            Id = id;
            Name = name;
            State = TaskState.Open;
            EmployeeId = employeeId;
            Description = description;
            StartDate = DateTime.Now;
            FinishDate = DateTime.MaxValue;
        }
    }
}