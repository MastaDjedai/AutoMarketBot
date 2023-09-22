using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AutoMarketBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("6587747565:AAHD7bZ_Mgi-HVAPRu6akH80C3HKPMaeiNo");

            using CancellationTokenSource cts = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cancellationToken: cts.Token);

            Console.ReadLine();


            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
            {
                MainConfig config = new MainConfig();
                if(update.Type == UpdateType.Message && update?.Message?.Text != null)
                {
                    try
                    {
                        await config.HandleMessage(botClient, update.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }



        private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        
    }
}