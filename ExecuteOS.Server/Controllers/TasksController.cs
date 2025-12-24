using ExecuteOS.Server.Common.Enums;
using ExecuteOS.Server.Modules.Tasks.DTOs;
using ExecuteOS.Server.Modules.Tasks.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll(CancellationToken cancellationToken)
        {
            var tasks = await _taskService.GetAllAsync(cancellationToken) ?? Enumerable.Empty<TaskDto>();

            if (!tasks.Any())
            {
                return NoContent();
            }

            return Ok(tasks);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id, cancellationToken);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(new {error = ex.Message});
            }
            
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetByStatus(string status, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _taskService.GetByStatusAsync(status, cancellationToken);
                return Ok(tasks);
            }
            catch (ArgumentException ex)
            {

                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.CreateTaskAsync(dto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }
            catch (ArgumentException ex)
            {

                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskDto>> Update(Guid id, [FromBody] UpdateTaskDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.UpdateTaskAsync(id, dto, cancellationToken);
                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(new { error = ex.Message });
            }
        }

    }
}
