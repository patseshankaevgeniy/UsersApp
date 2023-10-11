using System.Threading.Tasks;
using Users.Application.Auth.Models;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Auth;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
            IGenericRepository<User> userRepository,
            ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<SignInResultDto> SignInAsync(SignInDto signInDto)
    {
        if (string.IsNullOrEmpty(signInDto.Email))
        {
            throw new ValidationException("Login can't be null or empty");
        }

        if (string.IsNullOrEmpty(signInDto.Password))
        {
            throw new ValidationException("Password can't be null or empty");
        }

        var user = await _userRepository.FirstAsync(x => x.Email == signInDto.Email);
        if (user == null)
        {
            return new SignInResultDto
            {
                Succeeded = false,
                ErrorType = (int)AuthErrorType.UserNotFound
            };
        }

        if (user.Email == signInDto.Email && user.Password != signInDto.Password)
        {
            return new SignInResultDto
            {
                Succeeded = false,
                ErrorType = (int)AuthErrorType.WrongPassword
            };
        }

        return new SignInResultDto
        {
            Succeeded = true,
            Token = _tokenService.BuildToken(user)
        };
    }

    public async Task<SignUpResultDto> SignUpAsync(SignUpDto signUpDto)
    {

        if (string.IsNullOrEmpty(signUpDto.Email))
        {
            throw new ValidationException("Email can't be null or empty");
        }
        if (string.IsNullOrEmpty(signUpDto.Password))
        {
            throw new ValidationException("Password can't be null or empty");
        }
        if (string.IsNullOrEmpty(signUpDto.Name))
        {
            throw new ValidationException("Name can't be null or empty");
        }
        if (signUpDto.Age <= 0)
        {
            throw new ValidationException("Age can't be zero");
        }

        var user = await _userRepository.FirstAsync(x => x.Email == signUpDto.Email);
        if (user != null)
        {
            return new SignUpResultDto
            {
                Succeeded = false,
                ErrorType = (int)AuthErrorType.LoginAlreadyExists
            };
        }

        user = new User
        {
            Name = signUpDto.Name,
            Email = signUpDto.Email,
            Password = signUpDto.Password,
            Age = signUpDto.Age,
        };

        await _userRepository.CreateAsync(user);
        return new SignUpResultDto { Succeeded = true };
    }
}

