using ExecuteOS.Server.Modules.Tasks.DTOs;

namespace ExecuteOS.Server.Modules.Tasks.Services
{
    public interface ITaskService
    {
        Task<TaskDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<TaskDto>> GetAllAsync (CancellationToken cancellationToken = default);
        Task<List<TaskDto>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto dto, CancellationToken cancellationToken = default);
        Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto dto, CancellationToken cancellationToken = default);
        Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
