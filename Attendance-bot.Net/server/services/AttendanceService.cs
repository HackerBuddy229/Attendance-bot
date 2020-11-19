using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Attendance_bot.Server.models;
using Discord;

namespace Attendance_bot.Server.services
{
    public class AttendanceService
    {
        private readonly GroupStorage _groupStorage;

        public AttendanceService(GroupStorage groupStorage)
        {
            _groupStorage = groupStorage;
        }

        public async Task<AttendanceResult> Check(string groupName, IChannel channel)
        {
            var group = _groupStorage.GetGroup(groupName);
            if (group == null)
                return new AttendanceResult
                    {Errors = new List<string>{$"The class {groupName} does not exist"}};

            var users = GetUsers(channel.GetUsersAsync());

            //TODO: cóntinue here
            
            
            throw new NotImplementedException();
        }

        private IList<IUser> GetUsers(IAsyncEnumerable<IReadOnlyCollection<IUser>> users)
        {
            var enumerator = users.GetAsyncEnumerator();
            var list = enumerator.Current;

            return list.ToList();
        }
    }
}
