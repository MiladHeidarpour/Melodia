using Microsoft.VisualBasic;
using System.Text;
using System.Text.Json;

namespace Common.AspNetCore.IGapUtil;

public class IGapService : IIGapService
{
    private readonly HttpClient _httpClient;
    private readonly string _botToken = "a885d81c-bbb3-4df7-b76e-7849deebedc9";

    public IGapService()
    {
        _httpClient = new HttpClient();
    }

    public async Task SendMessage(string text)
    {
        try
        {
            string apiUrl = $"https://api.igap.net/v1/bot{_botToken}/sendMessage";

            var payload = new
            {
                chat_id = "MelodiaApi",
                text = text,
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // ارسال درخواست به API آیگپ
            var response = await _httpClient.PostAsync(apiUrl, content);

            //// بررسی موفقیت‌آمیز بودن درخواست
            //return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            //
        }
    }
}