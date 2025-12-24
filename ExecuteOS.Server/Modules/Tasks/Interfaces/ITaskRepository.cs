using ExecuteOS.Server.Common.Enums;
using ExecuteOS.Server.Modules.Tasks.Models;

namespace ExecuteOS.Server.Modules.Tasks.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> GetAllAsnync(CancellationToken cancellationToken = default);
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TaskItem> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<TaskItem> GetByStatusAsync(Status status, CancellationToken cancellationToken = default);
        Task<TaskItem> AddAsync(TaskItem task, CancellationToken cancellationToken = default);
        Task<TaskItem> UpdateAsync(TaskItem task, CancellationToken cancellationToken = default);
        Task<TaskItem> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
