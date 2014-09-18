using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTasks.Data;

namespace TodoTask.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new TodoTasksData();
            data.Tasks.All().ToList();
        }
    }
}
