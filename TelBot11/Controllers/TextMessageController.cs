using TelBot11.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly CalculatorService _calculatorService;
    private readonly SentenceSizeService _sentenceSizeService;
    private readonly IStorage _memoryStorage;

    public TextMessageController(ITelegramBotClient telegramClient, CalculatorService calculatorService, SentenceSizeService sentenceSizeService, IStorage memoryStorage)
    {
        _telegramClient = telegramClient;
        _calculatorService = calculatorService;
        _sentenceSizeService = sentenceSizeService;
        _memoryStorage = memoryStorage;
    }

    public async Task Handle(Message message, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Controller {GetType().Name} get message");
        
        switch (message.Text)
        {
            case "/start":

                // Объект, представляющий кноки
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($"Calculate Sentence Length" , $"WordLength"),
                        InlineKeyboardButton.WithCallbackData($"Calculate Numbers" , $"Calculate")
                    }); 

                 // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                 await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Choose Option.</b> {Environment.NewLine}", cancellationToken: cancellationToken, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                //switch (_memoryStorage.GetSession(message.Chat.Id).Choose)
                //{
                //    case "WordLength":

                //}
                return;
                break;
        }

        if (_memoryStorage.GetSession(message.Chat.Id).Choose.Equals("WordLength"))
        {
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Sentence Length is - {_sentenceSizeService.GetLenth(message.Text)}", cancellationToken: cancellationToken);
        }
        else if (_memoryStorage.GetSession(message.Chat.Id).Choose.Equals("Calculate"))
        {
            try
            {
                var chars = message.Text.Split(" ").Select(s => Double.Parse(s)).ToList();
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Result is - {_calculatorService.GetSum(chars)}");
            }
            catch (Exception ex)
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"You have to write only digits with space beetwen them!");

            }


            
        }
    }
}
