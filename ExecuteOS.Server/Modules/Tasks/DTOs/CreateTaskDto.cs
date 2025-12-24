namespace ExecuteOS.Server.Modules.Tasks.DTOs
{
    public record CreateTaskDto
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Priority { get; init; } 
        public DateTime? DueTime { get; init; }
        public decimal EstimatedHours { get; init; }
    }
}
