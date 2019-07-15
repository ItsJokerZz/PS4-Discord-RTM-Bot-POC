using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace PS4_Discord_RTM_Bot_POC.Commands
{
    public class Notify : ModuleBase<SocketCommandContext>
    {
        [Command("notify")]

        public async Task NotifyAsync([Remainder] string Message)
        {
            try
            {
                Calling.Notify($"{Message}\n\n\n\n\n\n", 210);
                await ReplyAsync($"Notification sent Successfully!");
            }
            catch
            {
                await ReplyAsync($"Notification failed to send!");
            }
        }
    }
}
