using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EValueApi.SSISComponents
{
    public abstract class LogGroup
    {
        public Hashtable Attributes { get; set; } = new Hashtable();
        protected DateTime StartTime { get; set; } = DateTime.Now;
        protected DateTime EndTime { get; set; } = DateTime.MaxValue;
        protected Stopwatch StopWatch { get; set; } = new Stopwatch();
        public IList<LogItem> Items { get; set; } = new List<LogItem>();
        public string Duration
        {
            get
            {
                if (StopWatch.Elapsed.TotalMilliseconds < 1000)
                {
                    return string.Format("{0}ms", StopWatch.Elapsed.TotalMilliseconds.ToString("N0"));
                }
                else if (StopWatch.Elapsed.TotalSeconds < 60)
                {
                   return string.Format("{0}s", StopWatch.Elapsed.TotalSeconds.ToString("N2"));
                }
                else
                {
                    return string.Format("{0}m", StopWatch.Elapsed.TotalMinutes.ToString("N2"));
                }
            }
        }
        public void Start()
        {
            StartTime = DateTime.Now;
            StopWatch.Start();
        }

        public void End()
        {
            EndTime = DateTime.Now;
            StopWatch.Stop();
        }

        public void Append(string message)
        {
            Items.Add(new LogItem { Message = message, Time = DateTime.Now });
        }

        public void Append(string message, string value)
        {
            Items.Add(new LogItem { Message = message, Time = DateTime.Now, Value = value });
        }
    }
}
