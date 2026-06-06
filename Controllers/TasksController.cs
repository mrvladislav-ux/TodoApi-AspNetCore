using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.IO;

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
    


        [HttpGet]
        public ActionResult<List<TaskItem>> Get()
        {

            var tasks = service.Get();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetByID(int id)
        {
            var task = service.GetByID(id);

            if(task == null)
            {
                return NotFound();
            }

            return task;
        }
        

        [HttpPost]
        public ActionResult<TaskItem> Add(CreatedTaskDto dto)
        {
            var CreatedTask = service.Add(dto.Title);

            return CreatedAtAction(
                nameof(GetByID),
                new {id = CreatedTask.Id},
                CreatedTask
            );
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            bool deleted = service.DeleteTask(id);

            if(!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        

        [HttpPut("{id}")]
        public ActionResult<TaskItem> EditTask(int id , UpdatedTaskDto dto)
        {
            var ChangedTask = service.EditTask(id , dto);

            if(ChangedTask == null)
            {
                return NotFound();
            }
            
            return Ok(ChangedTask);
        }

        [HttpGet("Completed")]
        public ActionResult<List<TaskItem>> FilterCompleted()
        {
            var SortedTasks = service.FilterCompleted();

            if(SortedTasks.Count == 0)
            {
                return NotFound();
            }

            return Ok(SortedTasks);
        }

        [HttpGet("Pending")]
        public ActionResult<List<TaskItem>> FilterPendding()
        {
            var PenddingTasks = service.FilterPending();

            return Ok(PenddingTasks);
        }

        [HttpGet("search")]
        public ActionResult<List<TaskItem>> Search(string title)
        {
            var SearchedTasks = service.Search(title);

            return Ok( SearchedTasks);
        }
 
}
}
