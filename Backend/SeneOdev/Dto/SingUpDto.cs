namespace SeneOdev.Dto
{
    public record SignupRequest(
        string Name,
        string Surname,
        string Username,
        string Email,
        string Phone,
        string Gender,
        bool Sozlesme,
        string Password,
        string PasswordRepeat
    );
}