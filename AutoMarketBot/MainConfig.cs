using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoMarketBot
{
    public class MainConfig
    {
        
        async private Task MainKeyboard(ITelegramBotClient botClient,Message message)
        {
            ReplyKeyboardMarkup keyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Оформити замовлення" },
                new KeyboardButton[] { "Переглянути товар" }
            })
            {
                ResizeKeyboard = true
            };
            Message sendMainKeyboard = await botClient.SendTextMessageAsync(message.Chat.Id, "Чим допомогти?", replyMarkup:keyboardMarkup);
        }
        async private Task CheckoutKeyboard(ITelegramBotClient botClient, Message message)
        {
            ReplyKeyboardMarkup keyboardMarkup = new(new[]
            {
                new KeyboardButton[]{"Кошик"},
                new KeyboardButton[]{"Замовити"},
                new KeyboardButton[]{"Назад"}
            })
            {
                ResizeKeyboard = true
            };
            Message sendChekoutKeyboard = await botClient.SendTextMessageAsync(message.Chat.Id,"(*_*)",replyMarkup:keyboardMarkup);
        }

        async private Task BasketInline(ITelegramBotClient botClient, Message message)
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithSwitchInlineQueryCurrentChat(text:"querry","quer")
                }
            });
            
            Message sendMessage = await botClient.SendTextMessageAsync(message.Chat.Id, "Каталог", replyMarkup: inlineKeyboard);

        }
        async public Task HandleMessage(ITelegramBotClient botClient,Message message)
        {
            if(message.Text == "/start")
            {
                await MainKeyboard(botClient, message);
            }
            if(message.Text == "Назад")
            {
                await MainKeyboard(botClient, message);
            }
            if(message.Text == "Оформити замовлення")
            {
                await CheckoutKeyboard(botClient, message);
            }
            if(message.Text == "Переглянути товар")
            {
                await BasketInline(botClient, message);
            }
        }

    }
}
