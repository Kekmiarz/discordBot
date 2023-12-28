using DSharpPlus;
// using DSharpPlus.AsyncEvents;
// using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
// using DSharpPlus.Exceptions;
//using DSharpPlus.Interactivity;
// using DSharpPlus.Net;
//using DSharpPlus.SdplashCommands;
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
//test
namespace discordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "ODk3OTQxOTIyMTA1MTYzNzc2.GcFViX.bCVOpqb6yBl1s7ktmuIzbKqIks5_7N4TUJrV8g",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
            });
            Task dailyReminderTask = DailyReminder(discord);
            Task papiezReminderTask = PapiezReminder(discord);
            Task messageObserverTask = WatchForMessage(discord);

            await discord.ConnectAsync();

            await Task.WhenAll(dailyReminderTask, papiezReminderTask, messageObserverTask);

            async Task DailyReminder(DiscordClient discordLocal)
            {
                var guildsCreated = await discord.GetGuildAsync(883768472427978812, null);
                var channels = guildsCreated.Channels;

                string message;
                string messageReplaced;

                DateTime currentDateTime = DateTime.Now;

                string formattedDateTime = currentDateTime.ToString("dd MMMM yyyy");

                string data = "data.txt";

                string filesPath = "..//discordBot//custom_files//";

                string file_read = File.ReadAllText(filesPath + data);
                if (file_read != formattedDateTime)
                {
                    File.WriteAllText(filesPath + data, formattedDateTime);

                    Console.WriteLine(File.ReadAllText(filesPath + data));

                    foreach (var kanal in channels)
                    {
                        if (kanal.Value.Name.Contains("daily-reminder"))
                        {
                            message = kanal.Value.Name;
                            messageReplaced = message.Replace('-', ' ');
                            await discordLocal.SendMessageAsync(await discordLocal.GetChannelAsync(kanal.Key), "@everyone " + messageReplaced);
                            Console.WriteLine(await discordLocal.GetChannelAsync(kanal.Key) + " " + DateTime.Now.ToShortTimeString());
                            await Task.Delay(2500);
                        }

                    }
                    Console.WriteLine("Daily remindery aktualne");
                }
                else
                    Console.WriteLine("Daily remindery aktualne");
                return;
            }
            async Task PapiezReminder(DiscordClient discordLocal)
            {
                var messageBuilder = new DiscordMessageBuilder();

                DiscordChannel papiezawka = await discordLocal.GetChannelAsync(1112102955181674607);
                Console.WriteLine("kys");
                string relativePath = "custom_files/attachments/papiez/papiez1.gif";
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..", relativePath);
                if (File.Exists(relativePath))
                {
                    using (FileStream fileStream = new FileStream(relativePath, FileMode.Open, FileAccess.Read))
                    {
                        messageBuilder.AddFile(fileStream);
                        var time = DateTime.Now;
                        DateTime papiez = new DateTime(time.Year, time.Month, time.Day, 21, 37, 0);

                        DateTime timeHandler = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

                        int until_papiez = (int)(papiez - timeHandler).TotalMilliseconds;
                        Console.WriteLine(until_papiez);
                        await Task.Delay(until_papiez);
                        await messageBuilder.SendAsync(papiezawka);
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }


            }
            async Task WatchForMessage(DiscordClient discordLocal)
            {
                discordLocal.MessageCreated += async (discordLocal, eventLocal) => await MessageListener(discordLocal, eventLocal);
            }
            async Task MessageListener(DiscordClient discordLocal, MessageCreateEventArgs eventLocal)
            {
                ulong[] blacklist = { 897941922105163776, 703133081254756425, 614140239606317066 }; //id botow 

                string files_path = @"C:\Users\cojat\Documents\repki\discord_bot\ConsoleApp1\pliki_pomocnicze\";
                string cur_file;

                string message = eventLocal.Message.Content;
                string respondMessage;
                DiscordChannel cur_channel;

                if (!blacklist.Contains(eventLocal.Message.Author.Id))
                {
                    cur_channel = eventLocal.Message.Channel;
                    switch (message)
                    {
                        case var temp when (blacklist.Contains(eventLocal.Message.Author.Id)):
                            return;
                        case var temp when (message.ToLower().StartsWith("!daily reminder:")):
                            DiscordGuild guildLocal = await discordLocal.GetGuildAsync(eventLocal.Guild.Id);
                            string newChannelName = message.Remove(0, 16) + " daily reminder";
                            DiscordChannel channelTag = await guildLocal.CreateTextChannelAsync(newChannelName, null, newChannelName);
                            switch (eventLocal.Guild.Id)
                            {
                                case 1013837467771732131:
                                    await guildLocal.CreateTextChannelAsync(newChannelName, null, newChannelName);
                                    await eventLocal.Message.RespondAsync("<#" + channelTag.Id + ">");
                                    return;
                                case 883768472427978812:
                                    var parentLocal = await discordLocal.GetChannelAsync(999394047321968672);
                                    await guildLocal.CreateTextChannelAsync(newChannelName, parentLocal, newChannelName);
                                    await eventLocal.Message.RespondAsync("<#" + channelTag.Id + ">");
                                    return;
                            }
                            return;
                        case var temp when (message.ToLower().StartsWith("jm")):
                            cur_file = "jm.txt";
                            respondMessage = File.ReadAllText(files_path + cur_file);
                            await discordLocal.SendMessageAsync(cur_channel, respondMessage);
                            break;

                        case var temp when (message.ToLower().StartsWith("!list")):
                            await discordLocal.SendMessageAsync(cur_channel, "Lista komend: ");
                            return;

                        case var temp when (message.ToLower().Contains("1989")):
                            cur_file = "tiananmen.txt";
                            respondMessage = File.ReadAllText(files_path + cur_file);
                            await discordLocal.SendMessageAsync(cur_channel, respondMessage);
                            return;

                        case var temp when (message.ToLower().Contains("😎")):
                            await discordLocal.SendMessageAsync(cur_channel, ":sunglasses:");
                            return;

                        case var temp when (message.ToLower().Contains("cockstein")):
                            cur_file = "cockstein.txt";
                            respondMessage = File.ReadAllText(files_path + cur_file);
                            await discordLocal.SendMessageAsync(cur_channel, respondMessage);
                            return;

                        case var temp when (message.ToLower().Contains("wot")):
                            await eventLocal.Message.RespondAsync(":poop:");
                            return;

                        case var temp when (message.ToLower().Contains("zezagi")):
                            await eventLocal.Message.RespondAsync("<@907719094592225350>");
                            return;
                        default:
                            break;
                    }
                }
            }
            await Task.Delay(-1);
        }
    }
}
