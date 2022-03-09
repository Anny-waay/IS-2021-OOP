using System;
using System.Collections.Generic;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime FinishDate { get; set; }

        public Report()
        {
        }
        
        public Report(Guid id, Guid employeeId, Guid taskId, string description)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId),"EmployeeId is invalid");
            }
            
            Id = id;
            EmployeeId = employeeId;
            TaskId = taskId;
            Description = description;
            StartDate = DateTime.Now;
            FinishDate = DateTime.MaxValue;
        }
    }
}