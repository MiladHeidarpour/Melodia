﻿using Common.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Auth;

public class RegisterVM
{
    [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
    [MaxLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    [MinLength(11, ErrorMessage = ValidationMessages.InvalidPhoneNumber)]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [MaxLength(50, ErrorMessage = "کلمه عبور نباید بیشتر از 50 کارکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [MaxLength(50, ErrorMessage = "تکرار کلمه عبور نباید بیشتر از 50 کارکتر باشد")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; }
}