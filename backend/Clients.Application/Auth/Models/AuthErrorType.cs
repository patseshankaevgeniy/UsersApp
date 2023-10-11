namespace Users.Application.Auth.Models;

public enum AuthErrorType
{
    UserNotFound = 0,
    WrongPassword = 1,
    UnknowExeption = 2,
    LoginAlreadyExists = 3,
    BadUserName = 4,
}
