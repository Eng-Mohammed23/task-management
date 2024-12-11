using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Contracts.Duration;

public record DurationOfDayRequest(
    int Id,
    decimal Value,
    DateTime Time,
    string? Rating,
    int TaskId
);
