using Common.Domain;
using Common.Domain.Base;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Proj.Domain.UserAgg.Services;
using Proj.Domain.VerificationAgg;

namespace Proj.Domain.UserAgg;

public class User : AggregateRoot
{
    public long RoleId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public string Avatar { get; private set; }
    public bool IsActive { get; private set; }
    public List<UserToken> Tokens { get; }

    private User()
    {
    }
    public User(long roleId, string password, IUserDomainService domainService, string? phoneNumber = "", string? email = "", string? fullName = "")
    {
        Guard(email, phoneNumber, domainService);
        RoleId = roleId;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Avatar = "Avatar.png";
        IsActive = true;
        Tokens = new();
    }

    public void Edit(string fullName, string email, string phoneNumber, IUserDomainService domainService)
    {
        Guard(email, phoneNumber, domainService);
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void ChangePassword(string newPassword)
    {
        NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));
        Password = newPassword;
    }

    public void SetAvatar(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
        {
            imageName = "Avatar.png";
        }
        Avatar = imageName;
    }

    public static User RegisterUserPhoneNumber(long roleId, string phoneNumber, string password, IUserDomainService domainService)
    {
        return new User(roleId, password, domainService, phoneNumber, "", "");
    }

    public static User RegisterUserEmail(long roleId, string email, string password, IUserDomainService domainService)
    {
        return new User(roleId, password, domainService, "", email, "");
    }

    //UserToken
    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activetokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
        if (activetokenCount == 3)
        {
            throw new InvalidDomainDataException("امکان استفاده همزمان از 4 دستگاه وجود ندارد");
        }
        var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        token.UserId = Id;
        Tokens.Add(token);
    }
    public string RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(c => c.Id == tokenId);
        if (token == null)
        {
            throw new InvalidDomainDataException("توکن نامعتبر است");
        }
        Tokens.Remove(token);
        return token.HashJwtToken;
    }


    //Guard
    private void Guard(string? email, string? phoneNumber, IUserDomainService domainService)
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber) == false)
        {
            if (phoneNumber.Length != 11)//length<11 || length>11
            {
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");
            }

            if (phoneNumber != PhoneNumber)
            {
                if (domainService.IsPhoneNumberExist(phoneNumber) == true)
                {
                    throw new InvalidDomainDataException("شماره موبایل تکراری است");
                }
            }
        }

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (email.IsValidEmail() == false)
                {
                    throw new InvalidDomainDataException("ایمیل نامعتبر است");
                }
            }
            if (email != Email)
            {
                if (domainService.IsEmailExist(email) == true)
                {
                    throw new InvalidDomainDataException("ایمیل تکراری است");
                }
            }
        }
    }
}