namespace Users.Application.Auth.Models;

public class SignUpResultDto
{
    public bool Succeeded { get; set; }
    public int? ErrorType { get; set; }
}
