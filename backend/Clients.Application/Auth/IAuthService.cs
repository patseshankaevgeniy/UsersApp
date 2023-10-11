using Users.Application.Auth.Models;
using System.Threading.Tasks;

namespace Users.Application.Auth
{
    public interface IAuthService
    {
        Task<SignInResultDto> SignInAsync(SignInDto signInDto);
        Task<SignUpResultDto> SignUpAsync(SignUpDto signUpDto);
    }
}
