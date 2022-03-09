using System;

namespace Reports.DAL.Entities
{
    public class TaskChange
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        
        public TaskChange(Guid id, Guid taskId, string comment)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (taskId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }
            
            Id = id;
            TaskId = taskId;
            Comment = comment;
            Date = DateTime.Now;
        }
    }
}