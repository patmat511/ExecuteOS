using ExecuteOS.Server.Common.Enums;

namespace ExecuteOS.Server.Modules.TimeTracking.DTOs
{
    public record TimeEntryDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public Guid? TaskId { get; init; }
        public string TaskTitle { get; init; } = string.Empty;
        public DateTime StartTime { get; init; }
        public DateTime? EndTime { get; init; }
        public TimeSpan Duration { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Category { get; init; }  = string.Empty; 
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public bool IsRunning => !EndTime.HasValue;
    }
}
