using ExecuteOS.Server.Common.Enums;
using ExecuteOS.Server.Modules.Tasks.Models;

namespace ExecuteOS.Server.Modules.Tasks.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<TaskItem>> GetByStatusAsync(Status status, CancellationToken cancellationToken = default);
        Task<TaskItem> AddAsync(TaskItem task, CancellationToken cancellationToken = default);
        Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
