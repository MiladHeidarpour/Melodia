using Common.Application;

namespace Common.AspNetCore.TelegramUtil;

public interface ITelegramService
{
    Task SendMessage(string message);
}