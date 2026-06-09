using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace First_API.Controllers
{
    [ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{

    private readonly TaskService service;

    public TasksController(TaskService service)
        {
            this.service = service;
        }
    

        [Authorize]
        [HttpGet]
        public ActionResult<List<TaskItem>> Get()
        {
            var userId = GetUserId();

            var tasks = service.Get(userId);

            return Ok(tasks);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetByID(int id)
        {
            var userId = GetUserId();

            var task = service.GetByID(id , userId);

            if(task == null)
            {
                return NotFound();
            }

            return task;
        }
        

        [Authorize]
        [HttpPost]
        public ActionResult<TaskItem> Add(CreatedTaskDto dto)
        {

            var userId = GetUserId();

            var CreatedTask = service.Add(dto.Title , userId);

            return CreatedAtAction(
                nameof(GetByID),
                new {id = CreatedTask.Id},
                CreatedTask
            );
        }


        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {

            var userId = GetUserId();

            bool deleted = service.DeleteTask(id , userId);

            if(!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<TaskItem> EditTask(int id , UpdatedTaskDto dto)
        {

            var userId = GetUserId();

            var ChangedTask = service.EditTask(id , dto , userId);

            if(ChangedTask == null)
            {
                return NotFound();
            }
            
            return Ok(ChangedTask);
        }


        [Authorize]
        [HttpGet("Completed")]
        public ActionResult<List<TaskItem>> FilterCompleted()
        {
            var userId = GetUserId();

            var SortedTasks = service.FilterCompleted(userId);

            if(SortedTasks.Count == 0)
            {
                return NotFound();
            }

            return Ok(SortedTasks);
        }


        [Authorize]
        [HttpGet("Pending")]
        public ActionResult<List<TaskItem>> FilterPendding()
        {
            var userId = GetUserId();

            var PenddingTasks = service.FilterPending(userId);

            return Ok(PenddingTasks);
        }


        [Authorize]
        [HttpGet("search")]
        public ActionResult<List<TaskItem>> Search(string title)
        {
            var userId = GetUserId();

            var SearchedTasks = service.Search(title , userId);

            return Ok( SearchedTasks);
        }

        private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
 
}
}
