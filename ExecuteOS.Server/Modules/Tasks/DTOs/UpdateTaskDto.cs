namespace ExecuteOS.Server.Modules.Tasks.DTOs
{
    public record UpdateTaskDto
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? Status { get; init; }
        public string? Priority { get; init; }
        public DateTime? DueTime { get; init; }
        public decimal? EstimatedHours { get; init; }
    }
}
