using Proj.Domain.UserAgg;
using Proj.Query.Users.Dtos;
using Proj.Query.Users.DTOs;

namespace Proj.Query.Users.Mapper;

public static class UserMapper
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            RoleId = user.RoleId,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Password = user.Password,
            Avatar = user.Avatar,
            IsActive = user.IsActive,
            CreationDate = user.CreationDate,
        };
    }

    public static UserFilterData MapFilterData(this User user)
    {
        return new UserFilterData()
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Avatar = user.Avatar,
        };
    }

    public static List<UserDto>? Map(this List<User> users)
    {
        var model = new List<UserDto>();
        users.ForEach(user =>
        {
            model.Add(new UserDto()
            {
                Id = user.Id,
                RoleId = user.RoleId,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Avatar = user.Avatar,
                IsActive = user.IsActive,
                CreationDate = user.CreationDate,
            });
        });
        return model;
    }
}