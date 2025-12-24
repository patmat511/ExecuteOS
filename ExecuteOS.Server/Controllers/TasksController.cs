using ExecuteOS.Server.Modules.Tasks.DTOs;
using ExecuteOS.Server.Modules.Tasks.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExecuteOS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks(CancellationToken cancellationToken)
        {
            var tasks = await _taskService.GetAllAsync(cancellationToken) ?? Enumerable.Empty<TaskDto>();

            if (!tasks.Any())
            {
                return NoContent();
            }

            return Ok(tasks);

        }
    }
}
