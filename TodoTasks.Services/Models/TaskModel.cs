namespace TodoTasks.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using TodoTasks.Models;

    public class TaskModel
    {
        public TaskModel()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? Deadline { get; set; }

        public StatusType Status { get; set; }

        public int CategoryId { get; set; }
    }
}