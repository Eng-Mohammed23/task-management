using Azure.Core;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManagement.Api.Contracts.Duration;
using TaskManagement.Api.Contracts.DurationOfDay;
using TaskManagement.Api.Entities;
using TaskManagement.Api.Errors;

namespace TaskManagement.Api.Services
{
    public class TaskService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : ITaskService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<Result<IEnumerable<TaskResponse>>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure<IEnumerable<TaskResponse>>(UserErrors.UserNotFound);

            var tasks = await _context.MyTasks.Include(u => u.DurationOfDays)
                .Where(t => t.UserId == userId)
                .AsNoTracking().ProjectToType<TaskResponse>()
                .ToListAsync(cancellationToken);

            //if(tasks.Count() > 0 || tasks is not null) 
            //    tasks = tasks.Adapt<List<MyTask>>();

            //return tasks.Adapt<IEnumerable<TaskResponse>>();
            return Result.Success<IEnumerable<TaskResponse>>(tasks);
            //return tasks;
        }
        public async Task<Result<TaskResponse>> GetAsync(int taskId, string userId, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure<TaskResponse>(UserErrors.UserNotFound);

            if (await _context.MyTasks.Include(u => u.DurationOfDays).SingleOrDefaultAsync(x => x.Id == taskId) is not { } task)
                return Result.Failure<TaskResponse>(TaskErrors.TaskNotFound);

            return Result.Success(task.Adapt<TaskResponse>());
        }
        public async Task<Result<TaskResponse>> CreateAsync(TaskRequest request, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure<TaskResponse>(UserErrors.UserNotFound);

            var task = request.Adapt<MyTask>();
            //task.UserId = userId;

            await _context.AddAsync(task,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(task.Adapt<TaskResponse>());
        }
        public async Task<Result<TaskResponse>> SendMyDayTask(DurationOfDayRequest request, string userId, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure<TaskResponse>(UserErrors.UserNotFound);

            if (await _context.MyTasks.FindAsync(request.TaskId) is not { } task)
                return Result.Failure<TaskResponse>(TaskErrors.TaskNotFound);
            await _context.AddAsync(request.Adapt<DurationOfDay>());
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(task.Adapt<TaskResponse>());
        }
        public async Task<Result> EditAsync(TaskRequest request, string userId, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            if (await _context.MyTasks.FindAsync(request.Id) is not { } task)
                return Result.Failure(TaskErrors.TaskNotFound);

            //var task = request.Adapt<MyTask>();
            //task.UserId = userId;

            _context.Update(request.Adapt<MyTask>());
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int taskId, string userId, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            if (await _context.MyTasks.FindAsync(taskId) is not { } task)
                return Result.Failure(TaskErrors.TaskNotFound);

            _context.MyTasks.Remove(task);

            //var durations= _context.DurationOfDays.Where(x => x.TaskId.Contains())

            var durations = await _context.DurationOfDays.Where(x => x.TaskId == taskId).ToListAsync();
            _context.RemoveRange(durations);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
