using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Application.Clients.Models;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Interfaces;
using Users.Application.Users.Models;
using Users.Domain.Entities;
using ValidationException = Users.Application.Common.Exceptions.ValidationException;

namespace Users.Application.Users.Services;

public class UsersService : IUsersService
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CreatedUserModel> _validator;

    public UsersService(
        IGenericRepository<User> clientRepository,
        IGenericRepository<UserRole> userRoleRepository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        IValidator<CreatedUserModel> validator)
    {
        _userRepository = clientRepository;
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _validator = validator;
    }

    public async Task<List<UserModel>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync(x => x.Include(y => y.Roles));

        return users.Select(_mapper.Map<UserModel>).ToList();
    }

    public async Task<UserModel> GetAsync(int userId)
    {
        if (userId <= 0)
        {
            throw new ValidationException("Id can't be less or equal zero");
        }

        var user = await _userRepository.FirstAsync(
                                            x => x.Id == userId,
                                            x => x.Include(y => y.Roles));

        if (user == null)
        {
            throw new NotFoundException($"Can't find user with id: {userId}");
        }

        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> GetCurrentUser()
    {
         var user = await _userRepository.FirstAsync(
                                            x => x.Id == _currentUserService.UserId,
                                            x => x.Include(y => y.Roles));

        if (user == null)
        {
            throw new NotFoundException($"Can't find user with id: {_currentUserService.UserId}");
        }

        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> AddUserRoleAsync(int userId, string role)
    {
        if (userId <= 0)
        {
            throw new ValidationException("Id can't be less or equal zero");
        }

        var user = await _userRepository
            .FirstAsync(
                x => x.Id == userId,
                x => x.Include(y => y.Roles));

        if (user == null)
        {
            throw new NotFoundException($"Can't find user with id: {userId}");
        }

        var userRole = await _userRoleRepository.FirstAsync(x => x.Role == role);
        if (userRole == null)
        {
            throw new NotFoundException($"Can't find userRole with name: {role}");
        }

        if (user.Roles.Contains(userRole))
        {
            throw new ValidationException("This role has already been added");
        }

        user.Roles.Add(userRole);
        user = await _userRepository.UpdateAsync(user);
        return _mapper.Map<UserModel>(user);
    }

    public async Task<UserModel> CreateAsync(CreatedUserModel userModel)
    {
        var result = _validator.Validate(userModel);
        if (!result.IsValid)
        {
            throw new ValidationException(result.ToString());
        }

        var newUser = new User
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Password = userModel.Password,
            Age = userModel.Age,
            Roles = new List<UserRole>()
        };

        foreach (var role in userModel.Roles)
        {
            var userRole = await _userRoleRepository.FirstAsync(x => x.Role == role);
            if (userRole == null)
            {
                throw new NotFoundException($"Can't find userRole with name: {role}");
            }
            newUser.Roles.Add(userRole);
        }

        newUser = await _userRepository.CreateAsync(newUser);
        return _mapper.Map<UserModel>(newUser);
    }

    public async Task<UserModel> UpdateAsync(int id, UserPatchModel userPatchDto)
    {
        var user = await _userRepository
            .FirstAsync(
                x => x.Id == id,
                x => x.Include(y => y.Roles));

        if (user == null)
        {
            throw new NotFoundException($"Can't find user with id: {id}");
        }

        return null;
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException("Id can't be less or equal zero");
        }

        var user = await _userRepository.GetAsync(id);

        if (user == null)
        {
            throw new NotFoundException($"Can't find user with id: {id}");
        }

        await _userRepository.DeleteAsync(user);
    }

    public async Task<UserModel> CheckUser(string email)
    {
        var user = await _userRepository
            .FirstAsync(
                x => x.Email == email,
                x => x.Include(y => y.Roles));

        return _mapper.Map<UserModel>(user);
    }
}
