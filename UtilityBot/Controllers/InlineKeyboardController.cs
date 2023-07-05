using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private IStorage _memoryStorage;
        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)  
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;
            
            _memoryStorage.GetSession(callbackQuery.From.Id).BotMode = callbackQuery.Data;

            string languageText = callbackQuery.Data switch
            {
                "stringLength" => " строку из слов",
                "sumOfNumbers" => " цифры разделенные пробелом",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"Введите {languageText}.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}