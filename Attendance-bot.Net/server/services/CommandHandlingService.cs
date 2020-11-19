using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Attendance_bot.Server.services
{
    public class CommandHandlingService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandService _commandService;
        private readonly DiscordSocketClient _discordSocketClient;

        public CommandHandlingService(IServiceProvider serviceProvider,
            CommandService commandService, 
            DiscordSocketClient discordSocketClient)
        {
            _serviceProvider = serviceProvider;
            _commandService = commandService;
            _discordSocketClient = discordSocketClient;

            _commandService.CommandExecuted += CommandExecutedAsync;
            _discordSocketClient.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
        }

        public async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // Ignore system messages, or messages from other bots
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            // This value holds the offset where the prefix ends
            var argPos = 0; //TODO: set

            // Perform prefix check. You may want to replace this with
            // (!message.HasCharPrefix('!', ref argPos))
            // for a more traditional command format like !help.
            if (!message.HasMentionPrefix(_discordSocketClient.CurrentUser, ref argPos)) return;

            var context = new SocketCommandContext(_discordSocketClient, message);


            // Perform the execution of the command. In this method,
            // the command service will perform precondition and parsing check
            // then execute the command if one is matched.
            await _commandService.ExecuteAsync(context, argPos, _serviceProvider);

            // Note that normally a result will be returned by this format, but here
            // we will handle the result in CommandExecutedAsync,
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // command is unspecified when there was a search failure (command not found); we don't care about these errors
            if (!command.IsSpecified)
                return;

            // the command was successful, we don't care about this result, unless we want to log that a command succeeded.
            if (result.IsSuccess)
                return;

            // the command failed, let's notify the user that something happened.
            await context.Channel.SendMessageAsync($"error: {result}"); //TODO: fines for launch
        }
    }
}
