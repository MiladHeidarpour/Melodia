using System.Net.Mail;

namespace Proj.Api.ViewModels.Admins.Settings;

public class EmailMessageVM
{
    public string Subject { get; set; }           // موضوع ایمیل
    public string Body { get; set; }              // متن ایمیل



    //public string FromEmail { get; set; }         // ایمیل فرستنده
    //public DateTime? ScheduledSendTime { get; set; } // زمان ارسال برنامه‌ریزی شده
    //public List<string> ToEmails { get; set; }    // لیست ایمیل گیرندگان اصلی
    //public List<string> CcEmails { get; set; }    // لیست ایمیل‌های CC
    //public List<string> BccEmails { get; set; }   // لیست ایمیل‌های BCC
    //public bool IsHtml { get; set; }              // آیا ایمیل به صورت HTML است؟
    //public List<EmailAttachment> Attachments { get; set; } // پیوست‌ها
    //public EmailPriority Priority { get; set; }   // اولویت ایمیل
    //public string Language { get; set; }          // زبان ایمیل (اختیاری)
    //public Dictionary<string, string> CustomHeaders { get; set; } // هدرهای سفارشی
}