using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EValueApi.SSISComponents
{
    public class ColumnLog : LogGroup
    {
        public string InputColumnName { get; set; } = string.Empty;
        public object InputColumnValue { get; set; } = null;
        public object InputColumnValueForLogging
        {
            get
            {
                if (InputColumnValue == null)
                {
                    return "NULL";
                }
                if (InputColumnValue.GetType() == System.Type.GetType("System.DateTime"))
                {
                    return ((DateTime)InputColumnValue).ToShortDateString();
                }
                return InputColumnValue.ToString();
            }
        }
        public string ProcessingResult { get; set; } = string.Empty;

        public ColumnLog()
        {
            
        }

        public XElement GetXElement()
        {
            var columnElement = new XElement("column",
                new XAttribute("input_column", InputColumnName),
                new XAttribute("input_value", InputColumnValueForLogging),
                new XAttribute("duration", Duration),
                new XAttribute("start", StartTime.ToString("hhmmss.FFF")),
                new XAttribute("end", EndTime.ToString("hhmmss.FFF")),
                new XAttribute("processing_result", ProcessingResult));

            foreach (DictionaryEntry att in Attributes)
            {
                columnElement.Add(new XAttribute(att.Key.ToString(), att.Value));
            }

            foreach (var item in Items)
            {
                var logElement = new XElement("log",
                    new XAttribute("time", item.Time.ToString("hhmmss.FFF")),
                    new XAttribute("message", item.Message)
                );

                if (!string.IsNullOrEmpty(item.Value))
                {
                    logElement.Add(XElement.Parse(item.Value));
                }
                columnElement.Add(logElement);
            }

            return columnElement;
        }
    }
}
