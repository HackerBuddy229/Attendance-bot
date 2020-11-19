using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Attendance_bot.Server.services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Attendance_bot.Server
{
    public class BotStartup
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly CommandService _commandService;
        private readonly LoggingService _loggingService;
        private readonly CommandHandlingService _commandHandlingService;

        public BotStartup(IServiceProvider serviceProvider,
            DiscordSocketClient discordSocketClient,
            CommandService commandService,
            LoggingService loggingService,
            CommandHandlingService commandHandlingService)
        {
            _serviceProvider = serviceProvider;
            _discordSocketClient = discordSocketClient;
            _commandService = commandService;
            _loggingService = loggingService;
            _commandHandlingService = commandHandlingService;
        }

        public async Task Start()
        {

            _discordSocketClient.Log += _loggingService.LogAsync;
            _commandService.Log += _loggingService.LogAsync;

            await _discordSocketClient.LoginAsync(TokenType.Bot, ""); //TODO: create env var for token
            await _discordSocketClient.StartAsync();

            await _commandHandlingService.InitializeAsync();
        }
    }
}
