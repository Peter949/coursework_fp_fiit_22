using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Migrations.Sql;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AdminForm.Controllers;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using telegram_bot_c_.Controllers;
using telegram_bot_c_.Models;

            

namespace telegram_bot_c_
{
    internal class Program
    {
        static int sign_ph = -1;
        static int sign_h_id = 0;
        static int sign_m_id = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("started");
            try
            {
                StartBot();
            }
            catch (Exception err)
            {
                Console.ReadKey();
            }
            
            Console.ReadKey();
        }
        private static async void CreateStartButtons(TelegramBotClient bot, Message msg)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "Список причесок", "Список мастеров" },
                new KeyboardButton[] { "ЗАПИСАТЬСЯ НА ПРИЕМ", "Мои записи"},
            })
            {
                ResizeKeyboard = true,
                IsPersistent = true
            };
            await bot.SendMessage(
                chatId: msg.From.Id,
                text: "Добро пожаловать в парикмахерскую!\nВы можете посмотреть виды причесок и их стоимость в нашем салоне. После, " +
                "можете записаться у нашего мастера прямо тут.\n**С УВАЖЕНИЕМ КОМАНДА ПАРИКМАХЕРСКОЙ!**",
                replyMarkup: replyKeyboard
            );
        }
        private static async void CreateSignHButtons(TelegramBotClient bot, Message msg)
        {
            List<HairType> hairs = HairController.getHairs();
            var buttonsH = hairs.Select(arg => new KeyboardButton(arg.Name));
            var replySignH = new ReplyKeyboardMarkup(buttonsH)
            {
                ResizeKeyboard = true,
                IsPersistent = true
            };
            await bot.SendMessage(
                chatId: msg.Chat.Id,
                text: "Выберите прическу.",
                replyMarkup: replySignH
            );
        }
        private static async void CreateSignMButtons(TelegramBotClient bot, Message msg)
        {
            List<Master> masters = MasterController.getMasters();
            var buttonsM = masters.Select(arg => new KeyboardButton(arg.Name));
            var replySignM = new ReplyKeyboardMarkup(buttonsM)
            {
                ResizeKeyboard = true,
                IsPersistent = true
            };
            await bot.SendMessage(
                chatId: msg.Chat.Id,
                text: "Выберите мастера.",
                replyMarkup: replySignM
            );
        }
        private static async void StartBot()
        {
            var cts = new CancellationTokenSource();
            string token = File.ReadAllText("../../Resources/Token.txt");
            var bot = new TelegramBotClient(token);
            var me = await bot.GetMe();
            bot.OnMessage += OnMessage;
            bot.OnError += OnError;
            bot.OnUpdate += OnUpdate;
            cts.Cancel();
            Console.WriteLine(me.Id + " " + me.FirstName);



            async Task OnMessage(Message msg, UpdateType type)
            {
                if (msg.Text is null)
                {
                    return;
                }
                Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
                
                if (msg.Text == "/start")
                {
                    sign_ph = -1;
                    List<Client> clients = ClientController.getClients();
                    bool isReg = false;
                    long user_id = msg.From.Id;
                    foreach (var client in clients)
                    {
                        if (client.Telegram_id == user_id)
                        {
                            await bot.SendMessage(msg.From.Id, "Вы уже зарегистрированы!");
                            isReg = true;
                            break;
                        }
                    }
                    if (!isReg)
                    {
                        ClientController.addClient(me.FirstName, user_id);
                        await bot.SendMessage(msg.From.Id, $"Вы успешно зарегистрировались {msg.From.FirstName}");
                    }
                    CreateStartButtons(bot, msg);
                }

                if (msg.Text == "Список причесок")
                {
                    List<HairType> hairs = HairController.getHairs();

                    var inlineKeyboard = new InlineKeyboardMarkup(hairs.Select(arg =>
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData(text: arg.Name, callbackData: arg.Name),
                            }
                    ));
                    await bot.SendMessage(
                        chatId: msg.From.Id,
                        text: "Выберите из списка прическу.",
                        replyMarkup: inlineKeyboard);
                }
                if (msg.Text == "Список мастеров")
                {
                    List<Master> masters = MasterController.getMasters();
                    foreach (var master in masters)
                    {
                        await bot.SendMessage(msg.From.Id, $"ФИО: {master.Name}\nНомер телефона: {master.Phone}");
                    }
                }
                if (msg.Text == "ЗАПИСАТЬСЯ НА ПРИЕМ")
                {
                    CreateSignHButtons(bot, msg);
                    sign_ph = 0;
                }

                if (sign_ph == 0)
                {
                    foreach (var hair in HairController.getHairs())
                    {
                        if (hair.Name == msg.Text)
                        {
                            sign_h_id = hair.Id;
                            CreateSignMButtons(bot, msg);
                            sign_ph = 1;
                            break;
                        }
                    }
                }
                if(msg.Text == "Мои записи")
                {
                    List<Note> notes = NoteController.getNotes();
                    List<Client> clients = ClientController.getClients();
                    List<HairType> hairs = HairController.getHairs();
                    List<Master> masters = MasterController.getMasters();
                    bool isNote = false;
                    foreach (var client in clients)
                    {
                        if(client.Telegram_id == msg.Chat.Id)
                        {
                            foreach (var note in notes)
                            {
                                string text = "";
                                if (note.Client_id == client.Id)
                                {
                                    foreach(var master in masters)
                                    {
                                        if(note.Master_id == master.Id)
                                        {
                                            text += $"Вы записаны к мастеру *{master.Name} | {master.Phone}* ";
                                            break;
                                        }
                                    }
                                    foreach (var hair in hairs)
                                    {
                                        if (note.Hair_id == hair.Id)
                                        {
                                            text += $"на прическу *{hair.Name}*!";
                                            isNote = true;
                                            break;
                                        }
                                    }
                                }
                                if (isNote)
                                {
                                    await bot.SendMessage(msg.Chat.Id, text);
                                }
                                else
                                {
                                    await bot.SendMessage(msg.Chat.Id, "Вы не записаны на прием!");
                                }
                            }
                            break;
                        }
                    }
                    
                }
                if(sign_ph == 1)
                {
                    foreach (var master in MasterController.getMasters())
                    {
                        if (master.Name == msg.Text)
                        {
                            sign_m_id = master.Id;
                            CreateStartButtons(bot, msg);
                            foreach(var client in ClientController.getClients())
                            {
                                if(client.Telegram_id == msg.Chat.Id)
                                {
                                    NoteController.createNote(client.Id, sign_h_id, sign_m_id);
                                    await bot.SendMessage(msg.Chat.Id, "Вы успешно записаны!");
                                    break;
                                }
                            }
                            sign_ph = -1;
                            break;
                        }
                    }
                }
            }


            async Task OnUpdate(Update update)
            {
                if(update.CallbackQuery != null)
                {
                    var callbackQuery = update.CallbackQuery;
                    var chosenHairType = callbackQuery.Data;
                    List<HairType> hairs = HairController.getHairs();
                    foreach (var hair in hairs)
                    {
                        if (hair.Name == chosenHairType)
                        {
                            var ms = new MemoryStream(hair.Image);
                            ms.Position = 0;
                            await bot.SendPhoto(
                                chatId: callbackQuery.Message.Chat.Id,
                                photo: InputFile.FromStream(ms),
                                caption: $"Прическа: {hair.Name}\nЦена: {hair.Price}"
                            );
                        }
                    }
                }

            }

            async Task OnError(Exception exception, HandleErrorSource source)
            {
                Console.WriteLine(exception);
            }

        }
    }
}









/*using (var context = new Context())
{
    byte[] img = File.ReadAllBytes("../../Images/Помпадур.jpg");
    //var stream = new MemoryStream();

    var hair = new HairType
    {
        Id = 1,
        Name = "Помпадур",
        Image = img
    };
    context.hair_types.Add(hair);
    context.SaveChanges();
}*/

/*using (var context = new Context())
            {
                var master = new Master
                {
                    Id = 1,
                    Name = "Адам"
                };
                context.masters.Add(master);
                context.SaveChanges();
            }*/