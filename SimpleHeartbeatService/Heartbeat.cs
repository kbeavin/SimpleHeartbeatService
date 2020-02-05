using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SimpleHeartbeatService
{
    public class Heartbeat
    {
        private readonly Timer _timer; //System.Timers.Timer class is a thread safe class 

        public Heartbeat()
        {
            _timer = new Timer(1000) { AutoReset = true }; // Timer resets every 1 second
            _timer.Elapsed += TimerElapsed; // When Timer elapses call event
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() }; // add one string to array then use AppendAllLines for convenience when you need to add more lines
            File.AppendAllLines(@"C:\temp\Heartbeat.txt", lines); // In a real world situation the path would be in a config file not hardcoded
        }

        public void Start()
        {
            _timer.Start(); // start timer
        }

        public void Stop()
        {
            _timer.Stop(); // stop timer
        }
    }
}
