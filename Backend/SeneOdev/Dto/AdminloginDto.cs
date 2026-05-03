namespace SeneOdev.Dto
{
    public record AdminLoginRequest(
        string Username,
        string Password,
        string Role
    );

}
