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
            await using var services = ConfigureServices();

            await services.GetRequiredService<BotStartup>().Start();

            await Task.Delay(Timeout.Infinite);
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection() //TODO: add command handling service
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<BotStartup>()
                .AddSingleton<LoggingService>()
                .AddSingleton<AttendanceService>()
                .BuildServiceProvider();
        }
    }
}
