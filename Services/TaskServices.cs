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

    public List<TaskItem> Get(int userId)
        {
            return _context.Tasks.Where(t => t.UserID == userId).ToList();
        }

    public TaskItem? GetByID(int id , int userId)
        {
           return _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserID == userId);
        }

    public TaskItem Add(string title , int userId)
        {
            var task = new TaskItem
            {
                Title = title,
                IsCompleted = false,
                UserID = userId
                
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

    public bool DeleteTask(int id , int userId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserID == userId);

            if(task == null)
        {
            return false;
        }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return true;
        }

    public TaskItem? EditTask(int id , UpdatedTaskDto dto , int userId)
        {
            var ExistingTask = _context.Tasks.FirstOrDefault(t => t.Id == id && t.UserID == userId);

            if(ExistingTask == null)
        {
            return null;
        }

           ExistingTask.Title = dto.Title;
           ExistingTask.IsCompleted = dto.IsCompleted;

           _context.SaveChanges();

            return ExistingTask;
        }

    public List<TaskItem> FilterCompleted(int userId)
    {
       return _context.Tasks.Where(t => t.IsCompleted && t.UserID == userId).ToList();
    }

    public List<TaskItem> FilterPending(int userId)
    {
        return _context.Tasks.Where(t => !t.IsCompleted && t.UserID == userId).ToList();
    }

    public List<TaskItem> Search(string title , int userId)
    {
        return _context.Tasks.Where(t => t.UserID == userId && t.Title.Contains(title , StringComparison.OrdinalIgnoreCase)).ToList();
    }

}