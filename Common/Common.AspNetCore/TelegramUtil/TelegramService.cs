using System.Net;
using System.Text;
using System.Text.Json;

namespace Common.AspNetCore.TelegramUtil;

public class TelegramService : ITelegramService
{
    private readonly string _botToken = "7619510132:AAEz7KJtBfsy1Eayo6Gottb4X0xSjEHHZ4o";
    private readonly HttpClient _httpClient;
    public TelegramService()
    {
        //var proxy = new WebProxy("http://138.91.159.185:80") // آدرس و پورت پروکسی خود را وارد کنید
        //{
        //    Credentials = new NetworkCredential("username", "password") // در صورت نیاز به احراز هویت
        //};

        //var handler = new HttpClientHandler
        //{
        //    Proxy = proxy,
        //    UseProxy = true
        //};

        //// ایجاد HttpClient با استفاده از HttpClientHandler
        //_httpClient = new HttpClient(handler);
        _httpClient = new HttpClient();
    }
    public async Task SendMessage(string message)
    {
        try
        {
            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
            var data = new
            {
                chat_id = "@MelodiaUser",
                text = message
            };

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
        }
        catch (Exception e)
        {
            //
        }
    }
}