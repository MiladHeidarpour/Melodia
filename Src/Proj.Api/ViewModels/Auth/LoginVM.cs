using Common.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Auth;

public class LoginVM
{
    //[Required(ErrorMessage = "شماره تلفن را وارد کنید")]
    //[MaxLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    //[MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    //public string PhoneNumber { get; set; }

    public string UserIdentifier { get; set; }

    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    public string Password { get; set; }
}