﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Attendance_bot.Server.modules
{
    public class AttendanceModule : ModuleBase<SocketCommandContext>
    {
        public AttendanceModule()
        {
            
        }

        [Command("Check")]
        public async Task CheckAttendance(string list, string channel)
        {

        }
    }
}
