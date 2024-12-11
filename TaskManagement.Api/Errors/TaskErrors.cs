namespace TaskManagement.Api.Errors;

public static class TaskErrors
{
    public static readonly Error TaskNotFound =
        new("Task.NotFound", "Task is not found", StatusCodes.Status404NotFound);

}
