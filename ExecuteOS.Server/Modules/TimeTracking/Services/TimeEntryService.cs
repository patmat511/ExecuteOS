using ExecuteOS.Server.Common.Enums;
using ExecuteOS.Server.Modules.TimeTracking.DTOs;
using ExecuteOS.Server.Modules.TimeTracking.Models;
using ExecuteOS.Server.Modules.TimeTracking.Repositories;

namespace ExecuteOS.Server.Modules.TimeTracking.Services
{
    public class TimeEntryService : ITimeEntryService
    {

        private readonly ITimeEntryRepository _repository;

        public TimeEntryService(ITimeEntryRepository repository)
        {
            _repository = repository;
        }
        public async Task<TimeEntryDto> CreateAsync(CreateTimeEntryDto dto, CancellationToken cancellationToken = default)
        {
            var userId = Guid.NewGuid(); 

            if (!Enum.TryParse<Category>(dto.Category, true, out var category))
            {
                throw new ArgumentException($"Invalid category: {dto.Category}");
            }

            var startTime = dto.StartTime ?? DateTime.UtcNow;

            var timeEntry = new TimeEntry
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TaskId = dto.TaskId,
                StartTime = startTime,
                EndTime = null,
                Duration = TimeSpan.Zero,
                Description = dto.Description,
                Category = category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var createdEntry = await _repository.CreateAsync(timeEntry, cancellationToken);
            return MapToDto(createdEntry);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var exists = await _repository.GetByIdAsync(id, cancellationToken);
            if (exists is null)
                throw new KeyNotFoundException($"Time entry with ID {id} not found.");

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entries = await _repository.GetAllAsync(cancellationToken);
            return entries.Select(MapToDto);
        }

        public async Task<TimeEntryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entry = await _repository.GetByIdAsync(id, cancellationToken);
            if(entry is null)
                throw new KeyNotFoundException($"Time entry with ID {id} not found.");
            
            return MapToDto(entry);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            var entries =  await _repository.GetByTaskIdAsync(taskId, cancellationToken);
            if(!entries.Any())
                throw new KeyNotFoundException($"No time entries found for Task ID {taskId}.");
           
            return entries.Select(MapToDto);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var entries = await _repository.GetByUserIdAsync(userId, cancellationToken);
            if(!entries.Any())
                throw new KeyNotFoundException($"No time entries found for User ID {userId}.");

            return entries.Select(MapToDto);
        }

        public async Task<TimeEntryDto?> GetRunningEntryAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var entry = await _repository.GetRunningEntryAsync(userId, cancellationToken);
            if(entry is null)
                throw new KeyNotFoundException($"No running time entry found for User ID {userId}.");

            return MapToDto(entry);

        }

        public async Task<TimeEntryDto> StartTimeAsync(CreateTimeEntryDto dto, CancellationToken cancellationToken = default)
        {
            var userId = Guid.NewGuid();
            var runningEntry = await _repository.GetRunningEntryAsync(userId, cancellationToken);

            if(runningEntry is not null)
                throw new InvalidOperationException("A time entry is already running for this user.");

            if(!Enum.TryParse<Category>(dto.Category, true, out var category))
            {
                throw new ArgumentException($"Invalid category {dto.Category}");
            }

            var timeEntry = new TimeEntry
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                StartTime = DateTime.UtcNow,
                EndTime = null,
                Description = dto.Description,
                Category = category,
                Duration = TimeSpan.Zero
            };

            var created = await _repository.CreateAsync(timeEntry, cancellationToken);
            return MapToDto(created);
        }

        public async Task<TimeEntryDto> StopTimeAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entry = await _repository.GetRunningEntryAsync(id, cancellationToken);

            if (entry is null)
                throw new KeyNotFoundException($"Entry with id: {id} not found");

            if (entry.EndTime.HasValue)
                throw new InvalidProgramException("Timer is already stopped");

            entry.EndTime = DateTime.UtcNow;
            entry.Duration = entry.EndTime.Value - entry.StartTime;

            var updated = await _repository.UpdateAsync(entry, cancellationToken);
            return MapToDto(updated);
        }

        public async Task<TimeEntryDto> UpdateAsync(Guid id, UpdateTimeEntryDto dto, CancellationToken cancellationToken = default)
        {
            var entry = await _repository.GetByIdAsync(id, cancellationToken);
            if(entry is null)
                throw new KeyNotFoundException($"Time entry with ID {id} not found.");
            
            if(dto.EndTime.HasValue)
            {
                entry.EndTime = dto.EndTime;
                entry.Duration = dto.EndTime.Value - entry.StartTime;
            }

            if(!string.IsNullOrWhiteSpace(dto.Descritpion))
            {
                entry.Description = dto.Descritpion!;
            }

            if(!string.IsNullOrWhiteSpace(dto.Category))
            {
                if (!Enum.TryParse<Category>(dto.Category, true, out var category))
                {
                    throw new ArgumentException($"Invalid category: {dto.Category}");
                }

                entry.Category = category;
            }

            var updatedEntry = await _repository.UpdateAsync(entry, cancellationToken);
            return MapToDto(updatedEntry);

        }

        private static TimeEntryDto MapToDto(TimeEntry entry)
        {
            return new TimeEntryDto
            {
                Id = entry.Id,
                UserId = entry.UserId,
                TaskId = entry.TaskId,
                TaskTitle = string.Empty,
                StartTime = entry.StartTime,
                EndTime = entry.EndTime,
                Duration = entry.Duration,
                Description = entry.Description,
                Category = entry.Category.ToString(),
                CreatedAt = entry.CreatedAt,
                UpdatedAt = entry.UpdatedAt
            };
        }
    }
}
