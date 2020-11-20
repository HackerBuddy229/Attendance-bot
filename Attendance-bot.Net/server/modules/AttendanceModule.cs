using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attendance_bot.Server.services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Attendance_bot.Server.modules
{
    public class AttendanceModule : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly AttendanceService _attendanceService;
        private readonly LoggingService _loggingService;

        public AttendanceModule(DiscordSocketClient discordSocketClient,
            AttendanceService attendanceService,
            LoggingService loggingService)
        {
            _discordSocketClient = discordSocketClient;
            _attendanceService = attendanceService;
            _loggingService = loggingService;
        }

        [Command("Check")]
        public async Task CheckAttendance(string list, ulong channelId)
        {
            //fetch result
            var channel = _discordSocketClient.GetChannel(channelId);
            if (channel == null)
                await ReplyAsync("Invalid channel specified");

            var result = _attendanceService.Check(list, channel);

            //log result
            await _loggingService.LogAsync(
                new LogMessage(LogSeverity.Info, "", message: result.Succeeded switch
                {
                    true => result.StringRepresentation,
                    false => result.Errors.ToString()
                }));


            //message result
            await ReplyAsync(message: result.Succeeded switch
            {
                true => result.StringRepresentation,
                false => result.Errors.ToString()
            });

        }
    }
}
