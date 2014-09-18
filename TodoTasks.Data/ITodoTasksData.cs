namespace TodoTasks.Data
{
    using TodoTasks.Data.Repositories;
    using TodoTasks.Models;

    public interface ITodoTasksData
    {
        IRepository<TodoTask> Tasks { get; }

        IRepository<Category> Categories { get; }

        void SaveChanges();
    }
}
