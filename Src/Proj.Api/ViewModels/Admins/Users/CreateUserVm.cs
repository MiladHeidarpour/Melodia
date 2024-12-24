using System.ComponentModel.DataAnnotations;

namespace Proj.Api.ViewModels.Admins.Users;

public class CreateUserVm
{
    [Display(Name = "نقش")]
    [Required(ErrorMessage = "نقش را وارد کنید")]
    public long RoleId { get; set; }

    [Display(Name = "نام و نام خانوادگی")]
    [MaxLength(100,ErrorMessage = "نام و نام خانوادگی نمیتواند بیشتر از 100 کارکتر باشد")]
    public string? FullName { get; set; }


    [Display(Name = "ایمیل")]
    [MaxLength(256, ErrorMessage = "ایمیل زیادی طولانی است!")]
    public string? Email { get; set; }

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
    [MaxLength(11,ErrorMessage = "شماره تلفن نامعتبر است")]
    [MinLength(11,ErrorMessage = "شماره تلفن نامعتبر است")]
    public string PhoneNumber { get; set; }


    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [MaxLength(50, ErrorMessage = "کلمه عبور نباید بیشتر از 50 کارکتر باشد")]
    public string Password { get; set; }


    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید")]
    [MinLength(6, ErrorMessage = "تکرار کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    [MaxLength(50, ErrorMessage = "تکرار کلمه عبور نباید بیشتر از 50 کارکتر باشد")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نیستند")]
    public string ConfirmPassword { get; set; }
}