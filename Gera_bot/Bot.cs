using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Newtonsoft.Json.Linq;


namespace Gera_bot
{
     class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;

       

        private TextMessage _textMessage;

        public Bot(ITelegramBotClient telegramClient, TextMessage textMessage)
        {
            _telegramClient = telegramClient;
            
            _textMessage = textMessage;
        }

        async Task HandleUpdateAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken cancellationToken)
        {
            if(update.Type == UpdateType.CallbackQuery)
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Что-то пошло не по плану", cancellationToken: cancellationToken);
            }
            if(update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessage.Choose(update.Message, cancellationToken);
                        return;
                    default:
                        //await _intSum.SumInt(update.Message, cancellationToken); 
                        return;
                }
            }
        }
        Task HandleErrorAsync(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegram API Error: {apiRequestException.Message}\n {apiRequestException.ErrorCode}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            Console.WriteLine("Ожидание 10 секунд");

            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             _telegramClient.StartReceiving(
                HandleUpdateAsync, HandleErrorAsync,
                new ReceiverOptions { AllowedUpdates = { } },
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }
        
        }
    }

