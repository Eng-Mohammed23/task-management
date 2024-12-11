using TaskManagement.Api.Contracts.Duration;
using TaskManagement.Api.Contracts.DurationOfDay;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Services;

public interface ITaskService
{
    Task<Result<IEnumerable<TaskResponse>>> GetAllAsync(string userId, CancellationToken cancellationToken = default);   
    Task<Result<TaskResponse>> GetAsync(int taskId, string userId, CancellationToken cancellationToken = default);
    Task<Result<TaskResponse>> CreateAsync(TaskRequest request, CancellationToken cancellationToken = default);
    Task<Result<TaskResponse>> SendMyDayTask(DurationOfDayRequest request, string userId, CancellationToken cancellationToken = default);
    Task<Result> EditAsync(TaskRequest request, string userId, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int taskId, string userId, CancellationToken cancellationToken = default);

}
