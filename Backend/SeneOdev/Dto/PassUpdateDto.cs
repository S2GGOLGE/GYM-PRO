namespace SeneOdev.Dto
{
    public record PassUpdateRequest(
        string Username,
        string NewPass,
        string NewPassRepeat
    );
}