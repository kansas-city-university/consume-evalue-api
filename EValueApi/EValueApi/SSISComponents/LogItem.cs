using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EValueApi.SSISComponents
{
    public class LogItem
    {
        public DateTime Time { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
