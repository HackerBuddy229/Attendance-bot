using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_bot.Server.models
{
    public class Group
    {
        public string Name { get; set; }
        public IList<Member> Members { get; set; }
    }
}
