using Microsoft.AspNetCore.Http;

namespace Common.Application.SecurityUtil;

public static class MusicValidator
{
    //public static bool IsMp3(this IFormFile? file)
    //{
    //    if (file == null) return false;

    //    try
    //    {

    //        // با استفاده از TagLib فایل را باز می‌کنیم
    //        using (var stream = file.OpenReadStream())
    //        {
    //            var tfile = TagLib.File.Create(stream);
    //            return tfile != null && tfile.Properties.Duration.TotalSeconds > 0;
    //        }
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    public static bool IsMp3(this IFormFile? file)
    {
        if (file == null) return false;

        try
        {
            // باز کردن جریان فایل برای خواندن
            using (var stream = file.OpenReadStream())
            {
                return stream.IsMp3();
            }
        }
        catch
        {
            return false;
        }
    }
    public static bool IsMp3(this Stream fileStream)
    {
        if (fileStream == null || fileStream.Length < 3)
            return false;

        // خواندن سه بایت اول فایل
        byte[] buffer = new byte[3];
        fileStream.Read(buffer, 0, 3);

        // بررسی هدر فایل MP3 (بایت اول باید 0x49 باشد، بایت دوم باید 0x44، بایت سوم باید 0x33)
        return buffer[0] == 0x49 && buffer[1] == 0x44 && buffer[2] == 0x33;
    }
}