using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Gera_bot
{
     class TextMessage
    {
        private ITelegramBotClient _tgBot;

        public TextMessage(ITelegramBotClient tgBot)
        {
            _tgBot = tgBot;
        }

        public async Task Choose(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Посчитать кол-во символов в сообщении",$"length"),
                        InlineKeyboardButton.WithCallbackData($"Посчитать числовые значения в сообщении",$"count")
                    }
                    );

                    await _tgBot.SendTextMessageAsync(message.Chat.Id, $"<b> 😎 Выберите действие {Environment.NewLine}</b>", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                default:
                    break;

                    
            }

        }
    }
}
