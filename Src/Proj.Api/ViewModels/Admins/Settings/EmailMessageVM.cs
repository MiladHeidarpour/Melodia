using System.Net.Mail;
using Proj.Query.Users.Dtos;

namespace Proj.Api.ViewModels.Admins.Settings;

public class EmailMessageVM
{
    public List<UserDto>? Users { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool? ForAll { get; set; } = false;


    //public string FromEmail { get; set; }         // ایمیل فرستنده
    //public DateTime? ScheduledSendTime { get; set; } // زمان ارسال برنامه‌ریزی شده

    //public List<string> CcEmails { get; set; }    // لیست ایمیل‌های CC
    //public List<string> BccEmails { get; set; }   // لیست ایمیل‌های BCC
    //public bool IsHtml { get; set; }              // آیا ایمیل به صورت HTML است؟
    //public List<EmailAttachment> Attachments { get; set; } // پیوست‌ها
    //public EmailPriority Priority { get; set; }   // اولویت ایمیل
    //public string Language { get; set; }          // زبان ایمیل (اختیاری)
    //public Dictionary<string, string> CustomHeaders { get; set; } // هدرهای سفارشی
}