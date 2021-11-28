using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System.Globalization;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using NAudio.Lame;
using NAudio.Wave;
using System.Collections.Generic;
using QRCoder;
using System.Drawing;

namespace TelegeramBot
{
    internal class Program
    {
        static string userState = "";
        static int YearAge, MonthAge, DayAge;
        static int randNumber = 0;

        static async Task Main(string[] args)
        {
            TelegramBotClient botClient = new TelegramBotClient("2134919715:AAElbeHWy94oAnwDGVGgcuqRwsLLVXUIRsc");
            var update = new Update();
            using var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

            var me = await botClient.GetMeAsync();



            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Send cancellation request to stop bot
            cts.Cancel();
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            var handler = update.Type switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
                UpdateType.EditedMessage => BotOnMessageReceived(botClient, update.EditedMessage!),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(botClient, update.InlineQuery!),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(botClient, update.ChosenInlineResult!),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {

            Console.WriteLine($"Receive message type: {message.Type}");

            if (message.Type != MessageType.Text)
                return;

            var action = message.Text!.Split(' ')[0] switch
            {
                "/start" => Start(botClient, message),
                "/game" => Game(botClient, message),
                "/age" => Age(botClient, message),
                "/voice" => Voice(botClient, message),
                "/max" => Max(botClient, message),
                "/argmax" => ArgMax(botClient, message),
                "/qrcode" => QrCode(botClient, message),
                "/request" => RequestContactAndLocation(botClient, message),
                "/help" => Usage(botClient, message),
                _ => Usage(botClient, message)
            };
            Message sentMessage = await action;
            Console.WriteLine($"The message was sent with id: {sentMessage.MessageId}");

            // Send inline keyboard
            // You can process responses in BotOnCallbackQueryReceived handler

            static async Task<Message> Start(ITelegramBotClient botClient, Message message)
            {
                var photo = await botClient.GetUserProfilePhotosAsync(message.Chat.Id);

                var userName = message.Chat.Username;
                var name = message.Chat.FirstName;
                var family = message.Chat.LastName;
                var bio = message.Chat.Bio;

                var text = $"Hello {userName}\n" +
                    $"name: {name} \n" +
                    $"family: {family} \n" +
                    $"bio: {bio}";
                return await botClient.SendPhotoAsync(chatId: message.Chat.Id,
                                                              photo: photo.Photos[0][0].FileId,
                                                              caption: text,
                                                              replyMarkup: new ReplyKeyboardRemove());

            }

            static async Task<Message> Game(ITelegramBotClient botClient, Message message)
            {
                userState = "game";
                Random random = new Random();
                randNumber = random.Next(0, 100);

                ReplyKeyboardMarkup replyKeyboardMarkup = new(
                    new[]
                    {
                         new KeyboardButton[] { "New Game"}
                    })
                {
                    ResizeKeyboard = true
                };
                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                           text: "بازی حدس عدد\nمن عددی بین 0 تا 100 توی ذهنم دارم که باید حدس بزنی",
                                                           replyMarkup: replyKeyboardMarkup);
            }

            static async Task<Message> Age(ITelegramBotClient botClient, Message message)
            {
                userState = "age_Y";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                           text: "محاسبه سن از تاریخ تولد\n\nسال تولد خود را وارد کنید:",
                                                                replyMarkup: new ReplyKeyboardRemove());
            }

            static async Task<Message> Voice(ITelegramBotClient botClient, Message message)
            {
                userState = "voice";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                           text: "تبدیل متن به صوت:\nلطفا متن انگلیسی را تایپ کنید...",
                                                                replyMarkup: new ReplyKeyboardRemove());
            }

            static async Task<Message> Max(ITelegramBotClient botClient, Message message)
            {
                userState = "max";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                           text: "\nپیدا کردن بزرگترین عدد\n اعداد را با فرمت زیر تایپ کنید:\n 14,7,78,15,8,19,20",
                                                                replyMarkup: new ReplyKeyboardRemove());
            }

            static async Task<Message> ArgMax(ITelegramBotClient botClient, Message message)
            {
                userState = "argmax";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                           text: "\nپیدا کردن اندیس بزرگترین عدد\n اعداد را با فرمت زیر تایپ کنید:\n 14,7,78,15,8,19,20",
                                                                replyMarkup: new ReplyKeyboardRemove());
            }

            static async Task<Message> QrCode(ITelegramBotClient botClient, Message message)
            {
                userState = "qrcode";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                           text: "ساخت QrCode \n لطفا متن خود را تایپ کنید :",
                                                                replyMarkup: new ReplyKeyboardRemove());
            }

            static async Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message)
            {
                ReplyKeyboardMarkup RequestReplyKeyboard = new(
                    new[]
                    {
                    KeyboardButton.WithRequestLocation("Location"),
                    KeyboardButton.WithRequestContact("Contact"),
                    });

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Who or Where are you?",
                                                            replyMarkup: RequestReplyKeyboard);
            }

            static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
            {
                if (message.Text == "New Game")
                {
                    Random random = new Random();
                    randNumber = random.Next(0, 100);
                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                    text: "شروع دوباره\nمجدد حدس بزن");
                }

                if (userState == "game")
                {
                    if (int.TryParse(message.Text.ToString(), out int num))
                    {
                        int number = Convert.ToInt32(message.Text);
                        if (number == randNumber)
                        {
                            userState = "";
                            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                             text: "آفرین برنده شدی",
                                                             replyMarkup: new ReplyKeyboardRemove());
                        }
                        if (number > randNumber)
                            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                    text: "بیا پایین تر");
                        if (number < randNumber)
                            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                    text: "برو بالا تر");
                        if (number > 100 || number < 0)
                            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                    text: "عدد باید بین بازه 0 تا 100 باشد");
                    }
                    else
                    {
                        return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                    text: "باید عددی صحیح بین 0 تا 100 انتخاب کنید",
                                                                    replyMarkup: new ReplyKeyboardRemove());
                    }
                }

                if (userState == "age_Y")
                {
                    YearAge = Convert.ToInt32(message.Text);
                    userState = "age_M";
                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "ماه تولد خود را وارد کنید:");
                }
                if (userState == "age_M")
                {
                    MonthAge = Convert.ToInt32(message.Text);
                    userState = "age_D";
                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "روز تولد خود را وارد کنید:");
                }
                if (userState == "age_D")
                {
                    DayAge = Convert.ToInt32(message.Text);
                    var date = DateTime.Now;
                    PersianCalendar calendar = new PersianCalendar();
                    var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));

                    var text = $"سن شما:\n" +
                        $"{persianDate.Year - YearAge} سال و {Math.Abs(persianDate.Month - MonthAge)} ماه و {Math.Abs(persianDate.Day - DayAge)} روز";

                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: text);
                }

                if (userState == "voice")
                {
                    // Initialize a new instance of the SpeechSynthesizer.  
                    using (var reader = new SpeechSynthesizer())
                    {
                        //set some settings
                        reader.Volume = 100;
                        reader.Rate = 0; //medium

                        //save to memory stream
                        MemoryStream ms = new MemoryStream();
                        reader.SetOutputToWaveStream(ms);

                        //do speaking
                        reader.Speak(message.Text);

                        string fileName = $"{Guid.NewGuid().ToString()}.mp3";
                        //now convert to mp3 using LameEncoder or shell out to audiograbber
                        ConvertWavStreamToMp3File(ref ms, fileName);

                        using (var stream = System.IO.File.OpenRead(fileName))
                        {
                            message = await botClient.SendVoiceAsync(
                                chatId: message.Chat.Id,
                                voice: stream,
                                duration: 36);
                        }
                        Task.Delay(500);
                        System.IO.File.Delete(fileName);
                        userState = "";
                    }

                }

                if (userState == "max")
                {
                    try
                    {
                        //List<int> numbers = new List<int>();
                        List<int> numbers = message.Text.Split(',')
                            .Select(x => Convert.ToInt32(x))
                            .ToList();

                        var text = $"All numbers is {numbers.Count} \n" +
                                $"Max: {numbers.Max()} \n" +
                                $"Min: {numbers.Min()}";
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: text);
                        userState = "";
                    }
                    catch (Exception e)
                    {
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "خطایی در پردازش صورت گرفته لطفا دوباره تلاش کنید");
                    }
                }

                if (userState == "argmax")
                {
                    try
                    {
                        var numbers = message.Text.Split(',')
                        .Select(x => Convert.ToInt32(x))
                        .ToList();

                        var indexMax = numbers.IndexOf(numbers.Max());
                        var indexMin = numbers.IndexOf(numbers.Min());

                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: $" !!! start index from 0 !!!\n" +
                                                            $"Index max number is {indexMax} and min number is {indexMin}");
                        userState = "";
                    }
                    catch (Exception)
                    {
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "خطایی در پردازش صورت گرفته لطفا دوباره تلاش کنید");
                    }

                }

                if (userState == "qrcode")
                {
                    try
                    {
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(message.Text, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);

                        string filePath = $"{Guid.NewGuid().ToString()}.bmp";
                        qrCodeImage.Save(filePath);

                        using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

                        await botClient.SendPhotoAsync(chatId: message.Chat.Id,
                                                              photo: new InputOnlineFile(fileStream, fileName),
                                                              caption: message.Text);
                        userState = "";
                        System.IO.File.Delete(fileName);
                    }
                    catch (Exception)
                    {
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "خطایی در پردازش صورت گرفته لطفا دوباره تلاش کنید");
                    }

                }

                else
                {
                    string usage = $"\nhelp:\n" +
                                         $"/start   - راه اندازی ربات \n" +
                                         $"/game    - بازی حدس عدد \n" +
                                         $"/age     - محاسبه سن با کمک تاریخ تولد \n" +
                                         $"/voice   - تبدیل متن به صوت \n" +
                                         $"/max     - پیدا کردن بزرگترین عدد \n" +
                                         $"/argmax  - پیدا کردن اندیس بزرگترین عدد \n" +
                                         $"/qrcode  - ساخت Qr Code \n" +
                                         $"/request - ارسال شماره تماس و موقعیت مکانی(GPS) \n" +
                                         $"/help    - راهنما \n";

                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: usage,
                                                                replyMarkup: new ReplyKeyboardRemove());
                }
                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "",
                                                                replyMarkup: new ReplyKeyboardRemove());
            }
            // Handle the SpeakCompleted event.  
            static void ConvertWavStreamToMp3File(ref MemoryStream ms, string savetofilename)
            {
                //rewind to beginning of stream
                ms.Seek(0, SeekOrigin.Begin);

                using (var retMs = new MemoryStream())
                using (var rdr = new WaveFileReader(ms))
                using (var wtr = new LameMP3FileWriter(savetofilename, rdr.WaveFormat, LAMEPreset.VBR_90))
                {
                    rdr.CopyTo(wtr);
                }
            }
        }

        // Process Inline Keyboard callback data
        private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}");

            await botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message.Chat.Id,
                text: $"Received {callbackQuery.Data}");
        }

        private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
        {
            Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

            InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "3",
                title: "TgBots",
                inputMessageContent: new InputTextMessageContent(
                    "hello"
                )
            )
        };

            await botClient.AnswerInlineQueryAsync(inlineQueryId: inlineQuery.Id,
                                                   results: results,
                                                   isPersonal: true,
                                                   cacheTime: 0);
        }

        private static Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
            return Task.CompletedTask;
        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
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
