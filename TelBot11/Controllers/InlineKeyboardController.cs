using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class InlineKeyboardController
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly IStorage _memoryStorage;

    public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
    {
        _telegramClient = telegramClient;
        _memoryStorage = memoryStorage;
    }

    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == null)
            return;

        // Обновление пользовательской сессии новыми данными
        _memoryStorage.GetSession(callbackQuery.From.Id).Choose = callbackQuery.Data;

        // Генерим информационное сообщение
        string choose = callbackQuery.Data switch
        {
            "WordLength" => "You Choose Calculate Sentence length.",
            "Calculate" => "You Choose Calculator, Enter Number",
            _ => String.Empty
        };

        await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Operation - {choose}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);

    }

}
