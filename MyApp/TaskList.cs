using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    internal class TaskList
    {
        List<Task> tasks;
        //N - Not Changed;
        //C - Changed;
        //D - Deleted;
        List<Char> changed; 
        public TaskList(List<Task> tasks) 
        {
            this.tasks = tasks;
            changed = new List<char>(tasks.Count);
            for (int i = 0; i < changed.Count; i++)
            {
                changed[i] = 'N';
            }
        }
        public void AddTask(Task t)
        {
            tasks.Add(t);
            changed.Add('C');
        }

        public void RemoveTask(long id) 
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    changed[i] = 'D';
                }
            }
        }

        public void ChangeTask(Task t)
        {
            for (int i = 0; i < tasks.Count; i++) 
            {
                if (tasks[i].Id == t.Id)
                {
                    tasks[i] = t;
                    changed[i] = 'C';
                    return;
                }
            }
        }

        public Task GetTaskById(long id)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == id)
                {
                    return tasks[i];
                }
            }
            return null;
        }
        public List<Task> Tasks { get { return tasks; } }
    }
}
