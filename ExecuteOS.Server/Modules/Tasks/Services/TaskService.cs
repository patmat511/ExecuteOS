using ExecuteOS.Server.Common.Enums;
using ExecuteOS.Server.Modules.Tasks.DTOs;
using ExecuteOS.Server.Modules.Tasks.Models;
using ExecuteOS.Server.Modules.Tasks.Repositories;
using System.Runtime.InteropServices;

namespace ExecuteOS.Server.Modules.Tasks.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Task title cannot be empty");

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Priority = (Priority)Enum.Parse(typeof(Priority), dto.Priority),
                Status = Status.New,
                EstimatedHours = dto.EstimatedHours,
                DueTime = dto.DueTime,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(task, cancellationToken);
            return MapToDto(task);
        }

        public async Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if(!await _repository.ExistsAsync(id, cancellationToken))
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var tasks = await _repository.GetAllAsync(cancellationToken);

            return tasks.Select(MapToDto).ToList(); 
        }

        public async Task<TaskDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);

            if (task is null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            return MapToDto(task);
        }

        public async Task<List<TaskDto>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            if(!Enum.TryParse<Status>(status, out var parsedStatus))
                throw new ArgumentException($"Invalid status value: {status}");
            
            var tasks = await _repository.GetByStatusAsync(parsedStatus, cancellationToken);

            return tasks.Select(MapToDto).ToList();
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);
            if(task is null)
                throw new KeyNotFoundException($"Task with ID {id} not found.");

            if(!string.IsNullOrWhiteSpace(dto.Title))
                task.Title = dto.Title;

            if(!string.IsNullOrWhiteSpace(dto.Description))
                task.Description = dto.Description;

            if(!string.IsNullOrWhiteSpace(dto.Status) && Enum.TryParse<Status>(dto.Status, out var status))
                task.Status = status;

            if (!string.IsNullOrWhiteSpace(dto.Priority) && Enum.TryParse<Priority>(dto.Priority, out var priority))
                task.Priority = priority;

            if (dto.EstimatedHours.HasValue)
                task.EstimatedHours = dto.EstimatedHours.Value;

            if (dto.DueTime.HasValue)
                task.DueTime = dto.DueTime;

            task.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(task, cancellationToken); 
            return MapToDto(task);
        }
        private TaskDto MapToDto(TaskItem task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                EstimatedHours = task.EstimatedHours,
                ActualHours = task.ActualHours,
                DueTime = task.DueTime,
                CreatedAt = task.CreatedAt,
                CompletedAt = task.CompletedAt
            };
        }

    }
}
