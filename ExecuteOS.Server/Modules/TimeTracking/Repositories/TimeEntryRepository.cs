using ExecuteOS.Server.Data;
using ExecuteOS.Server.Modules.TimeTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace ExecuteOS.Server.Modules.TimeTracking.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly AppDbContext _context;

        public TimeEntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TimeEntry> CreateAsync(TimeEntry timeEntry, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            timeEntry.CreatedAt = now;
            timeEntry.UpdatedAt = now;

            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync(cancellationToken);
            return timeEntry;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var timeEntry = await _context.TimeEntries.FindAsync(id);
            if(timeEntry is not null)
            {
                _context.TimeEntries.Remove(timeEntry);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<TimeEntry>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.TimeEntries
                .OrderByDescending(te => te.StartTime)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<TimeEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.TimeEntries
                .AsNoTracking()
                .FirstOrDefaultAsync(te => te.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TimeEntry>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            return await _context.TimeEntries
                .AsNoTracking()
                .Where(te => te.TaskId == taskId)
                .OrderByDescending(te => te.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TimeEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.TimeEntries
                .AsNoTracking()
                .Where(te => te.UserId == userId)
                .OrderByDescending(te => te.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<TimeEntry?> GetRunningEntryAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.TimeEntries
                .AsNoTracking()
                .FirstOrDefaultAsync(te => te.UserId == userId && te.EndTime == null, cancellationToken);
        }

        public async Task<TimeEntry> UpdateAsync(TimeEntry timeEntry, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            timeEntry.UpdatedAt = now;

            if(timeEntry.EndTime.HasValue)
            {
                timeEntry.Duration = timeEntry.EndTime.Value - timeEntry.StartTime;
            }

            _context.TimeEntries.Update(timeEntry);
            await _context.SaveChangesAsync(cancellationToken);
            return timeEntry;
        }
    }
}
