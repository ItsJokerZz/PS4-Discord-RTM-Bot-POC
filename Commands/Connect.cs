using Discord.Commands;
using System.Threading.Tasks;

namespace PS4_Discord_RTM_Bot_POC.Commands
{
    public class Connect : ModuleBase<SocketCommandContext>
    {
        [Command("connect")]

        public async Task ConnectAsync()
        {
            try
            {
                Calling.version = 505;
                Calling.ps4.Connect();
                Calling.Notify("PS4 Discord RTM Bot POC\nCreated by ItsJokerZz\n\njkPatch injected successfully!\n\n\n", 210);
                await ReplyAsync($"Successfully connected to {Inject.IPAddress}!");
            }
            catch
            {
                await ReplyAsync($"Failed to connect to {Inject.IPAddress}!");
            }
        }
    }
}
