namespace TodoTasks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<TodoTask> tasks;

        public Category()
        {
            this.tasks = new HashSet<TodoTask>();
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<TodoTask> Tasks 
        {
            get
            {
                return this.tasks;
            }

            set
            {
                this.tasks = value;
            }
        }

        [Required]
        public virtual TodoTasksUser User { get; set; }

        public string UserId { get; set; }
    }
}
