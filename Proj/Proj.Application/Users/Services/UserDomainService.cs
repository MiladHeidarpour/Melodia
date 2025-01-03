﻿using Proj.Domain.UserAgg.Repositories;
using Proj.Domain.UserAgg.Services;

namespace Proj.Application.Users.Services;

public class UserDomainService:IUserDomainService
{
    private readonly IUserRepository _repository;

    public UserDomainService(IUserRepository repository)
    {
        _repository = repository;
    }
    public bool IsEmailExist(string email)
    {
        return _repository.Exists(s => s.Email == email);
    }

    public bool IsPhoneNumberExist(string phoneNumber)
    {
        return _repository.Exists(s => s.PhoneNumber == phoneNumber);
    }
}