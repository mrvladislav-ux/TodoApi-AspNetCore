using System.Text.Json;
using System.Linq;
using System.Collections.Generic;


public class TaskService
{

    private List<TaskItem> tasks = new List<TaskItem>();

    private int CurrentID = 1;

    public TaskService()
    {
        LoadFile();
    }


    private void SaveFile()
        {
            string json = JsonSerializer.Serialize(tasks);

            System.IO.File.WriteAllText("tasks.json" , json);
        } 


    public void LoadFile()
        {
            if (System.IO.File.Exists("tasks.json"))
            {
                string json = System.IO.File.ReadAllText("tasks.json");

                tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();

            }

              CurrentID = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
                
        }

    public List<TaskItem> Get()
        {
            return tasks;
        }

    public TaskItem? GetByID(int id)
        {
           var task = tasks.FirstOrDefault(t => t.Id == id);

            return task;

        }

    public TaskItem Add(string title)
        {
            var task = new TaskItem
            {
                Id = CurrentID++,
                Title = title,
                IsCompleted = false
            };

            tasks.Add(task);
            SaveFile();

            return task;
        }

    public bool DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if(task == null)
        {
            return false;
        }

             tasks.Remove(task);
             SaveFile();

            return true;
        }

    public TaskItem? EditTask(int id , UpdatedTaskDto dto)
        {
            var ExistingTask = tasks.FirstOrDefault(t => t.Id == id);

            if(ExistingTask == null)
        {
            return null;
        }

           ExistingTask.Title = dto.Title;
           ExistingTask.IsCompleted = dto.IsCompleted;

            SaveFile();

            return ExistingTask;
        }

    public List<TaskItem> FilterCompleted()
    {
       return tasks.Where(t => t.IsCompleted).ToList();
    }

    public List<TaskItem> FilterPending()
    {
        return tasks.Where(t => !t.IsCompleted).ToList();
    }

    public List<TaskItem> Search(string title)
    {
        return tasks.Where(t => t.Title.Contains(title , StringComparison.OrdinalIgnoreCase)).ToList();
    }

}