using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;
using UtilityBot.Models;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private IStorage _memoryStorage;
        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            string botMode = _memoryStorage.GetSession(message.Chat.Id).BotMode;
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Вычислить длину строки", $"stringLength"),
                        InlineKeyboardButton.WithCallbackData($" Вычислить сумму чисел", $"sumOfNumbers")
                    });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Бот может вычислить длину строки или посчитать сумму чисел{Environment.NewLine}",
                        cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, Worker.Work(message.Text, botMode), cancellationToken: ct);
                    break;
            }
        }
    }
}