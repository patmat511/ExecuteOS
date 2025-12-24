namespace ExecuteOS.Server.Modules.Tasks.DTOs
{
    public record TaskDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public string Priority { get; init; } = string.Empty;
        public DateTime? DueTime { get; init; }
        public decimal EstimatedHours { get; init; }
        public decimal ActualHours { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? CompletedAt { get; init; }
    }
}
