using System;
using System.Collections.Generic;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public EmployeeStatus Status { get; set; }
        public Guid LeaderId { get; set; }

        public Employee()
        {
        }

        public Employee(Guid id, string name, EmployeeStatus status, Guid leaderId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            if (leaderId == Guid.Empty && status != 0)
            {
                throw new ArgumentNullException(nameof(leaderId),"LeaderId is invalid");
            }
            
            Id = id;
            Name = name;
            Status = status;
            LeaderId = leaderId;
        }
    }
}