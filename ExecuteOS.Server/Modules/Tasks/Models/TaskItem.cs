using ExecuteOS.Server.Common.Enums;

namespace ExecuteOS.Server.Modules.Tasks.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueTime { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal ActualHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TaskItem(string title, Priority priority)
        {
            Id = Guid.NewGuid();
            Title = title;
            Priority = priority;
            Status = Status.New;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

        }

    }
}
