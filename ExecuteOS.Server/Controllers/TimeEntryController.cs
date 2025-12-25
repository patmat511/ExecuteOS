using ExecuteOS.Server.Modules.TimeTracking.DTOs;
using ExecuteOS.Server.Modules.TimeTracking.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExecuteOS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryService _timeEntryService;

        public TimeEntryController(ITimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetAll(CancellationToken cancellationToken)
        {
            var entries = await _timeEntryService.GetAllAsync(cancellationToken);
            return Ok(entries);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TimeEntryDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
             var entry = await _timeEntryService.GetByIdAsync(id, cancellationToken);
             return Ok(entry);
        }

        [HttpGet("task/{taskId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetByTaskId(Guid taskId, CancellationToken cancellationToken)
        {
           var entry = await _timeEntryService.GetByTaskIdAsync(taskId, cancellationToken);
           return Ok(entry); 
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TimeEntryDto>>> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var entries = await _timeEntryService.GetByUserIdAsync(userId, cancellationToken);
                return Ok(entries);
            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("running/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TimeEntryDto>> GetRunning(Guid userId, CancellationToken cancellationToken)
        {
            var entry = await _timeEntryService.GetRunningEntryAsync(userId, cancellationToken);
            return Ok(entry);
      
        }

    }
}
