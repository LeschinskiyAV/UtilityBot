using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using UtilityBot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;
using UtilityBot.Сontrollers;

namespace VoiceTexterBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            var host = new HostBuilder().ConfigureServices((hostContext, services) => ConfigureServices(services)).UseConsoleLifetime().Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(appSettings);
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
        }
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6119369015:AAFssmpML2kyXcqRIhioalmQhalCtu1tn44",
            };
        }
    }
}