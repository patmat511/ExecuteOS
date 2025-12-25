namespace ExecuteOS.Server.Modules.TimeTracking.DTOs
{
    public record CreateTimeEntryDto
    {
        public Guid? TaskId { get; init; }
        public DateTime? StartTime { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
    }
}
