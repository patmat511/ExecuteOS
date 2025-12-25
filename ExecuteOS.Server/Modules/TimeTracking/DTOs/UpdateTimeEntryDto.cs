namespace ExecuteOS.Server.Modules.TimeTracking.DTOs
{
    public record UpdateTimeEntryDto
    {
        public DateTime? EndTime { get; init; }
        public string? Descritpion { get; init; }
        public string? Category { get; init; }
    }
}
