namespace TodoTasks.Services.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using System.Web.Http;

    using Antlr.Runtime.Tree;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using TodoTasks.Data;
    using TodoTasks.Models;
    using TodoTasks.Services.Models;

    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ITodoTasksData data;

        public TasksController()
            : this(new TodoTasksData())
        {

        }

        public TasksController(ITodoTasksData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var userId = User.Identity.GetUserId();
            var tasks = this.data
                .Tasks
                .All()
                .Where(t => t.Category.UserId == userId)
                .Project()
                    .To<TaskModel>()
                    .FirstOrDefault();

            return Ok(tasks);
        }

        [HttpGet]
        public IHttpActionResult ById(Guid id)
        {
            var userId = User.Identity.GetUserId();
            var task = this.data
                .Tasks
                .All()
                .Where(t => t.Category.UserId == userId)
                .Where(t => t.Id == id)
                .Project()
                    .To<TaskModel>()
                    .FirstOrDefault();

            if (task == null)
            {
                return BadRequest("Task does not exist - invalid id");
            }

            return Ok(task);
        }

        [HttpPost]
        public IHttpActionResult Create(TaskModel task)
        {
            var userId = User.Identity.GetUserId();
            if(this.data.Categories.All().All(c => c.UserId != userId))
            {
                return BadRequest("Category for the task does not exist - invalid id");
            }

            var newTask = new TodoTask 
            {
                Content = task.Content,
                CategoryId = task.CategoryId,
                CreationDate = DateTime.Now,
                Deadline = task.Deadline,
                Status = task.Status
            };
            this.data.Tasks.Add(newTask);
            this.data.SaveChanges();

            var taskDataModel =
                this.data.Tasks.All()
                    .Where(c => c.Id == newTask.Id)
                    .Project()
                    .To<TaskModel>()
                    .FirstOrDefault();

            return this.Ok(taskDataModel);
        }

        [HttpPut]
        public IHttpActionResult Update(Guid id, TaskModel task)
        {
            var userId = User.Identity.GetUserId();
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTask = this.data.Tasks.All().FirstOrDefault(c => c.Id == id);
            if (existingTask == null)
            {
                return BadRequest("Such task does not exist!");
            }

            var existingCategory = this.data.Categories.All().FirstOrDefault(c => c.UserId == userId);
            if(existingTask == null)
            {
                return BadRequest("Such category does not exist!");
            }

            existingTask.Status = task.Status;
            existingTask.CategoryId = task.CategoryId;
            existingTask.Content = task.Content;
            existingTask.Deadline = task.Deadline;
            this.data.SaveChanges();

            task.Id = id;
            return Ok(task);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var userId = User.Identity.GetUserId();
            var existingTask = this.data.Tasks.All().Where(t => t.Category.UserId == userId).FirstOrDefault(c => c.Id == id);
            if (existingTask == null)
            {
                return BadRequest("Such task does not exist!");
            }

            this.data.Tasks.Delete(existingTask);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
