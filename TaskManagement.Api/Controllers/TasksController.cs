using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Api.Abstractions;
using TaskManagement.Api.Contracts.Duration;
using TaskManagement.Api.Contracts.DurationOfDay;
using TaskManagement.Api.Services;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;

        [HttpGet("")]
        public async Task<IActionResult> GetAll(string userId, CancellationToken cancellationToken)
        {
            var result = await _taskService.GetAllAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetAsync(int taskId, string userId, CancellationToken cancellationToken)
        {
            var result = await _taskService.GetAsync(taskId, userId, cancellationToken);
            return result.IsSuccess? Ok(result) : result.ToProblem();
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody]TaskRequest request, CancellationToken cancellationToken)
        {
            var result = await _taskService.CreateAsync(request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> SendMyDayTask(DurationOfDayRequest request, string userId, CancellationToken cancellationToken)
        {
            var result = await _taskService.SendMyDayTask(request, userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("")]
        public async Task<IActionResult> Edit(TaskRequest request, string userId, CancellationToken cancellationToken)
        {
            var result = await _taskService.EditAsync(request, userId, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("")]
        public async Task<IActionResult> DeleteAsync(int taskId, string userId, CancellationToken cancellationToken)
        {
            var result = await _taskService.DeleteAsync(taskId, userId, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
