using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using librpc;
using Microsoft.Extensions.DependencyInjection;
using PS4_Discord_RTM_Bot_POC.Services;

namespace PS4_Discord_RTM_Bot_POC
{
    class Program
    {
        private DiscordSocketClient Client;

        static void Main(string[] args)
        {
            Console.Title = "PS4 Discord RTM Bot POC";
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            Client = new DiscordSocketClient();

            var Services = ConfigureServices;
            await Services.GetRequiredService<CommandHandler>().InitializeAsync(Services);

            string Token = "TokenGoesHere";

            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            Console.WriteLine("Online and ready for usage.");

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices => new ServiceCollection().AddSingleton(Client).AddSingleton<CommandService>().AddSingleton<CommandHandler>().BuildServiceProvider();
    }
}
