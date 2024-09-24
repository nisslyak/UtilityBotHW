using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBotHW.Services;

namespace UtilityBotHW.Controller
{
    internal class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _sessionStorage;
        private readonly CallbackQueryFunctions _callbackQueryFunctions;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage storage, CallbackQueryFunctions callbackQueryFunctions)
        {
            _telegramClient = telegramBotClient;
            _sessionStorage = storage;
            _callbackQueryFunctions = callbackQueryFunctions;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine("Got text message");

            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Сумма чисел", "sum"),
                        InlineKeyboardButton.WithCallbackData("Количесво символов", "count")
                    });

                    await _telegramClient.SendTextMessageAsync(
                        message.Chat.Id,
$"<b>Наш бот имеет несколько функций: подсчёт количества символов в тексте и вычисление суммы чисел, которые вы ему отправляете</b>\n\nКакое действие вы хотите выполнить?",
                        cancellationToken: ct,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:

                    string result = string.Empty;
                    int res = 0;
                    try
                    {
                        switch (_sessionStorage.GetSession(message.Chat.Id).ChosenButton)
                        {
                            case "sum":
                                res = _callbackQueryFunctions.GetSumNumbers(message.Text);
                                result = $"Сумма чисел равна {res}";
                                break;
                            case "count":
                                result = $"В сообщении {message.Text.Length} символов";
                                break;
                            default:
                                result = "Необходимо выбрать режим в главном меню.";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
                    break;
            }
        }
    }
}
