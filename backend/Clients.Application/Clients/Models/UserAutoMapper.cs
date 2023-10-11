using AutoMapper;
using System.Linq;
using Users.Domain.Entities;

namespace Users.Application.Users.Models;

public class UserAutoMapper : Profile
{
    public UserAutoMapper()
    {
        CreateMap<User, UserModel>()
            .ForMember(x => x.Roles, o => o.MapFrom(p => p.Roles.Select(r => r.Role)))
            .ReverseMap();
    }
}
