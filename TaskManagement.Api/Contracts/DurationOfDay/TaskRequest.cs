namespace TaskManagement.Api.Contracts.DurationOfDay;

public record TaskRequest(
    int? Id,
    string Title,
    string UserId
);


