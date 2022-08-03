using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TelBot11.Services;
using Telegram.Bot;

Console.OutputEncoding = System.Text.Encoding.Unicode;

var host = new HostBuilder()
    .ConfigureServices(services =>ConfigureServices(services))
    .UseConsoleLifetime()
    .Build();

Console.WriteLine("Service starting....");
Thread.Sleep(2000);

await host.RunAsync();

Console.WriteLine("Service Stopped...");






static void ConfigureServices(IServiceCollection services)
{
    AppSettings appSettings = BuildAppSettings();
    services.AddSingleton(BuildAppSettings());

    services.AddTransient<DefaultMessageController>();
    services.AddTransient<InlineKeyboardController>();
    services.AddTransient<TextMessageController>();

    services.AddTransient<CalculatorService>();
    services.AddTransient<SentenceSizeService>();

    services.AddSingleton<IStorage,MemoryStorage>();

    // Регистрируем объект TelegramBotClient c токеном подключения
    services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
    // Регистрируем постоянно активный сервис бота
    services.AddHostedService<Bot>();
}


static AppSettings BuildAppSettings()
{
    return new AppSettings()
    {
        BotToken = "5520622944:AAHDAvlOtFq-p2LmiHuirugryON6STHiMLE"
    };
}
