using ExecuteOS.Server.Common.Enums;

namespace ExecuteOS.Server.Modules.TimeTracking.Models
{
    public class TimeEntry
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
