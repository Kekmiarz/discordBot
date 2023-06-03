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

            await discord.ConnectAsync();
            async Task DailyReminder(DiscordClient discordLocal)
{
                var guilds_created = await discord.GetGuildAsync(883768472427978812, null);
                var channels = guilds_created.Channels;

                string message;
                string message_replaced;

                DateTime currentDateTime = DateTime.Now;

                string formattedDateTime = currentDateTime.ToString("dd MMMM yyyy");

                string data = "data.txt";

                string files_path = @"C:\Users\cojat\Documents\repki\discord_bot\ConsoleApp1\pliki_pomocnicze\";

                string file_read = File.ReadAllText(files_path + data);
                if (file_read != formattedDateTime)
                {
                    File.WriteAllText(files_path + data, formattedDateTime);

                    Console.WriteLine(File.ReadAllText(files_path + data));

                    foreach (var kanal in channels)
                    {
                        if (kanal.Value.Name.Contains("daily-reminder"))
                        {
                            message = kanal.Value.Name;
                            message_replaced = message.Replace('-', ' ');
                            await discordLocal.SendMessageAsync(await discordLocal.GetChannelAsync(kanal.Key), "@everyone " + message_replaced);
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
            async Task papiez_reminder(DiscordClient discordLocal)
            {
                DiscordChannel papiezawka = await discordLocal.GetChannelAsync(1112102955181674607);

                string papiezFile = @"C:\Users\cojat\Documents\repki\discord_bot\ConsoleApp1\pliki_pomocnicze\papiez.txt";
                string papiezMessage = File.ReadAllText(papiezFile);

                var time = DateTime.Now;
                DateTime papiez = new DateTime(time.Year, time.Month, time.Day, 21, 37, 00);

                DateTime timeHandler = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second + 0);

                int until_papiez = (int)(papiez - timeHandler).TotalMilliseconds;

                await Task.Delay(until_papiez);
                await discordLocal.SendMessageAsync(papiezawka, papiezMessage);
            }
            async Task messageListener(DiscordClient discordLocal, MessageCreateEventArgs eventLocal)
            {
                ulong[] blacklist = { 897941922105163776, 703133081254756425, 614140239606317066 }; //id botow 

                string files_path = @"C:\Users\cojat\Documents\repki\discord_bot\ConsoleApp1\pliki_pomocnicze\";
                string cur_file;

                string message = eventLocal.Message.Content;
                string respond_message;
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
                            respond_message = File.ReadAllText(files_path + cur_file);
                            await discordLocal.SendMessageAsync(cur_channel, respond_message);
                            break;

                        case var temp when (message.ToLower().StartsWith("!list")):
                            await discordLocal.SendMessageAsync(cur_channel, "Lista komend: ");
                            return;

                        case var temp when (message.ToLower().Contains("1989")):
                            cur_file = "tiananmen.txt";
                            respond_message = File.ReadAllText(files_path + cur_file);
                            await discordLocal.SendMessageAsync(cur_channel, respond_message);
                            return;

                        case var temp when (message.ToLower().Contains("😎")):
                            await discordLocal.SendMessageAsync(cur_channel, ":sunglasses:");
                            return;

                        case var temp when (message.ToLower().Contains("cockstein")):
                                cur_file = "cockstein.txt";
                                respond_message = File.ReadAllText(files_path + cur_file);
                                await discordLocal.SendMessageAsync(cur_channel, respond_message);
                                return;

                        case var temp when (message.ToLower().Contains("wot")):
                            await eventLocal.Message.RespondAsync(":poop:");
                            return;

                        case var temp when (message.ToLower().Contains("zezagi")):
                            await eventLocal.Message.gRespondAsync("<@907719094592225350>");
                            return;
                        default:
                            break;
                    }
                }
            }
            papiez_reminder(discord);
            discord.MessageCreated += async(discordLocal, eventLocal) => await messageListener(discordLocal, eventLocal);
            await DailyReminder(discord);
            await Task.Delay(-1);
        }
    }
}
