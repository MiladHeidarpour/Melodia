using System.ComponentModel.DataAnnotations;
using Proj.Domain.RoleAgg.Enums;

namespace Proj.Api.ViewModels.Admins.Roles;

public class CreateRoleVM
{
    [Required(ErrorMessage = "عنوان نقش را وارد کنید")]
    [Display(Name = "عنوان نقش")]
    [MaxLength(60,ErrorMessage = "عنوان نمیتواند بیشتر از 60 کارکتر باشد")]
    public string Title { get; set; }


    [Required(ErrorMessage = "دسترسی هارا واردکنید")]
    [Display(Name = "دسترسی ها")]
    public List<Permission> Permissions { get; set; }
}