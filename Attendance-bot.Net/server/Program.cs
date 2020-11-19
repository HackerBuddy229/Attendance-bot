using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Attendance_bot.Server.services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Attendance_bot.Server
{
    class Program
    {
        static void Main(string[] args) => 
            new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            using var services = ConfigureServices();

            var client = services.GetRequiredService<DiscordSocketClient>();
            var commandService = services.GetRequiredService<CommandService>();

            client.Log += LogAsync;
            commandService.Log += LogAsync;

            await client.LoginAsync(TokenType.Bot, ""); //TODO: create env var for token
            await client.StartAsync();

            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection() //TODO: add command handling service
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();
        }
    }
}
