using System.Net;
using System.Text;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common.AspNetCore.TelegramUtil;

public class TelegramService : ITelegramService
{
    private readonly string _botToken = "7619510132:AAEz7KJtBfsy1Eayo6Gottb4X0xSjEHHZ4o";
    private readonly string _chatId = "-1002366986036";
    private readonly TelegramBotClient _botClient;
    //private readonly HttpClient _httpClient;
    public TelegramService()
    {
        //var handler = new HttpClientHandler()
        //{
        //    Proxy = new WebProxy("http://142.132.171.131:9090"), // پروکسی خود را وارد کنید
        //    UseProxy = true,
        //};

        // ایجاد HttpClient با handler پروکسی
        var httpClient = new HttpClient(/*handler*/)
        {
            Timeout = TimeSpan.FromMinutes(1)
        };

        // ایجاد TelegramBotClient با HttpClient
        _botClient = new TelegramBotClient(_botToken, httpClient);
    }
    public async Task SendMessage(string message)
    {
        await _botClient.SendMessage(_chatId, text: message);

        //try
        //{
        //    var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        //    var data = new
        //    {
        //        chat_id = "@MelodiaUser",
        //        text = message
        //    };

        //    var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync(url, content);
        //}
        //catch (Exception e)
        //{
        //    //
        //}
    }
}