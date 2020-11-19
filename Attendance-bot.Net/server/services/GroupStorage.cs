using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Attendance_bot.Server.models;
using System.Text.Json;

namespace Attendance_bot.Server.services
{
    public class GroupStorage //TODO:add file safety func
    {

        private readonly string _location = "storage.json";
        public IList<Group> GetGroups()
        {
            var file = GetFile();
            return string.IsNullOrWhiteSpace(file) ? 
                null : JsonSerializer.Deserialize<IList<Group>>(file);
        }

        public Group GetGroup(string groupName)
        {
            var file = GetFile();
            if (string.IsNullOrWhiteSpace(file))
                return null;

            var list = JsonSerializer.Deserialize<IList<Group>>(file);

            return list?.FirstOrDefault(g => g.Name == groupName);
        }

        private string GetFile()
        {
            return File.ReadAllText(_location);
        }

        



    }
}
