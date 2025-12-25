using ExecuteOS.Server.Modules.TimeTracking.DTOs;

namespace ExecuteOS.Server.Modules.TimeTracking.Services
{
    public interface ITimeEntryService
    {
        Task<IEnumerable<TimeEntryDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TimeEntryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TimeEntryDto>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TimeEntryDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<TimeEntryDto?> GetRunningEntryAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<TimeEntryDto> StartTimeAsync(CreateTimeEntryDto dto, CancellationToken cancellationToken = default);
        Task<TimeEntryDto> StopTimeAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TimeEntryDto> CreateAsync(CreateTimeEntryDto dto, CancellationToken cancellationToken = default);
        Task<TimeEntryDto> UpdateAsync(UpdateTimeEntryDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
