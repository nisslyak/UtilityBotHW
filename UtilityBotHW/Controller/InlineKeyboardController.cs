using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBotHW.Services;

namespace UtilityBotHW.Controller
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {  
            _telegramClient = telegramBotClient; 
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).ChosenButton = callbackQuery.Data;

            string actionType = callbackQuery.Data switch
            {
                "sum" => $"Выбран режим сложения чисел! Пришлите числа, которые нужно сложить через пробел.",
                "count" => $"Выбран режим подсчёта количества символов! Пришлите текст.",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(
                callbackQuery.From.Id,
            actionType,
                cancellationToken: ct,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}
