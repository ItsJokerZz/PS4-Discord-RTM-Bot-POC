using Discord.Commands;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PS4_Discord_RTM_Bot_POC.Commands
{
    public class Inject : ModuleBase<SocketCommandContext>
    {
        public static string IPAddress;

        void SocketConnection(string IP, string Payload, int port)
        {
            Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.ReceiveTimeout = 1500;
            Socket.SendTimeout = 1500;
            Socket.Connect(new IPEndPoint(System.Net.IPAddress.Parse(IP), port));
            Socket.SendFile(Payload);
            Socket.Close();
        }

        readonly string jkPatch_Payload = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\payload.bin";
        readonly string jkPatch_ELF = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\kpayload.elf";

        [Command("inject")]

        public async Task InjectAsync(string IP)
        {
            var Message = await ReplyAsync($"Sending payload.bin to {IP}:9020...");
            try
            {
                SocketConnection(IP, jkPatch_Payload.Replace("file:\\", ""), 9020);

                Thread.Sleep(2000);
                await Message.ModifyAsync(x => x.Content = $"Sending kpayload.elf to {IP}:9020...");
                SocketConnection(IP, jkPatch_ELF.Replace("file:\\", ""), 9023);

                Thread.Sleep(1000);
                await Message.ModifyAsync(x => x.Content = "jkPatch injected successfully!");

                IPAddress = IP;
            }
            catch
            {
                await Message.ModifyAsync(x => x.Content = "jkPatch injection failed!");
            }
        }
    }
}
