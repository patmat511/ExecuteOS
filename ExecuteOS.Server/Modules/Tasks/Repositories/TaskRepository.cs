using ExecuteOS.Server.Common.Enums;
using ExecuteOS.Server.Data;
using ExecuteOS.Server.Modules.Tasks.Models;
using Microsoft.EntityFrameworkCore;

namespace ExecuteOS.Server.Modules.Tasks.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<TaskItem> AddAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            await _context.Tasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return task;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var task = await _context.Tasks
                 .FindAsync(id, cancellationToken);
            if(task is not null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                 .AnyAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<List<TaskItem>> GetAllAsnync(CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<List<TaskItem>> GetByStatusAsync(Status status, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
