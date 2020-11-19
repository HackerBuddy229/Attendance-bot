using System;
using System.Threading.Tasks;

namespace Attendance_bot.Server
{
    class Program
    {
        static void Main(string[] args) => 
            new Program().MainAsync();

        private async Task MainAsync()
        {


            await Task.Delay(TimeSpan.MaxValue);
        }
    }
}
