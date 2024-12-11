namespace TaskManagement.Api.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound =
        new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCredentials =
        new Error("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);


}
