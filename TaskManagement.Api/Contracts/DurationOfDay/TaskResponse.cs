using TaskManagement.Api.Contracts.Duration;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Contracts.DurationOfDay;

public record TaskResponse(
    int Id, 
    string Title, 
    string UserId,
    IEnumerable<DurationOfDayResponse> DurationsOfDay
);
