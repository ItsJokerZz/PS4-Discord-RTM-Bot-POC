using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace PS4_Discord_RTM_Bot_POC.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient Client;
        public static CommandService Commands;
        private IServiceProvider Provider;

        public CommandHandler(DiscordSocketClient Discord, CommandService Command, IServiceProvider IService)
        {
            Client = Discord;
            Commands = Command;
            Provider = IService;

            Client.MessageReceived += MessageReceived;
        }

        public async Task InitializeAsync(IServiceProvider IService)
        {
            Provider = IService;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Provider);
        }

        private async Task MessageReceived(SocketMessage Message)
        {
            if (!(Message is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            int Arg = 0;
            string Prefix = "PrefixGoesHere";
            if (message.HasStringPrefix(Prefix, ref Arg, StringComparison.CurrentCultureIgnoreCase) || message.HasMentionPrefix(Client.CurrentUser, ref Arg))
            {
                var Context = new SocketCommandContext(Client, message);
                var Result = await Commands.ExecuteAsync(Context, Arg, Provider);

                if (!Result.IsSuccess)
                {
                    await Context.Channel.SendMessageAsync(Result.ToString());
                }
            }
        }
    }
}