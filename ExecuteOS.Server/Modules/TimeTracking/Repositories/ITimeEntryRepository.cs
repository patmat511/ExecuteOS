using ExecuteOS.Server.Modules.TimeTracking.Models;

namespace ExecuteOS.Server.Modules.TimeTracking.Repositories
{
    public interface ITimeEntryRepository
    {
        Task<IEnumerable<TimeEntry>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TimeEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TimeEntry>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TimeEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<TimeEntry?> GetRunningEntryAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<TimeEntry> CreateAsync(TimeEntry timeEntry, CancellationToken cancellationToken = default);
        Task<TimeEntry> UpdateAsync(TimeEntry timeEntry, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
