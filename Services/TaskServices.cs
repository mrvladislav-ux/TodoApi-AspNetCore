using System.Text.Json;
using System.Linq;
using System.Collections.Generic;


public class TaskService
{

    public readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskItem> Get()
        {
            return _context.Tasks.ToList();
        }

    public TaskItem? GetByID(int id)
        {
           return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

    public TaskItem Add(string title)
        {
            var task = new TaskItem
            {
                Title = title,
                IsCompleted = false
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

    public bool DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if(task == null)
        {
            return false;
        }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return true;
        }

    public TaskItem? EditTask(int id , UpdatedTaskDto dto)
        {
            var ExistingTask = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if(ExistingTask == null)
        {
            return null;
        }

           ExistingTask.Title = dto.Title;
           ExistingTask.IsCompleted = dto.IsCompleted;

           _context.SaveChanges();

            return ExistingTask;
        }

    public List<TaskItem> FilterCompleted()
    {
       return _context.Tasks.Where(t => t.IsCompleted).ToList();
    }

    public List<TaskItem> FilterPending()
    {
        return _context.Tasks.Where(t => !t.IsCompleted).ToList();
    }

    public List<TaskItem> Search(string title)
    {
        return _context.Tasks.Where(t => t.Title.Contains(title , StringComparison.OrdinalIgnoreCase)).ToList();
    }

}