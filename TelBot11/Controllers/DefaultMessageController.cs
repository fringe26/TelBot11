using Telegram.Bot;
using Telegram.Bot.Types;

public class DefaultMessageController
{
    private readonly ITelegramBotClient _telegramClient;

    public DefaultMessageController(ITelegramBotClient telegramClient)
    {
        _telegramClient = telegramClient;
    }

    public async Task Handle(Message message, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Controller {GetType().Name} get message");

        await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено сообщение не поддерживаемого формата", cancellationToken: cancellationToken);
    }
}
