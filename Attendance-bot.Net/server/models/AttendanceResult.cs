using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_bot.Server.models
{
    public class AttendanceResult
    {
        public bool Succeeded => !Errors.Any();
        public IList<string> Errors { get; set; }

        public IList<Member> Present { get; set; }
        public IList<Member> Missing { get; set; }

        public string StringRepresentation => 
        $"{Present.Length ?? 0} Present out of {Present.Length + Missing.Length ?? 0}";
    }
}
