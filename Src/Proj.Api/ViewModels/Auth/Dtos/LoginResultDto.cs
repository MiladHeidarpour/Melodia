namespace Proj.Api.ViewModels.Auth.Dtos;

public class LoginResultDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}