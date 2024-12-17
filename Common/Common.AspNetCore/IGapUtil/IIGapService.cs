namespace Common.AspNetCore.IGapUtil;

public interface IIGapService
{
    Task SendMessage(string text);
}