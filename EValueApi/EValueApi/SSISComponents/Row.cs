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
    public class RowLog : LogGroup
    {
        public IList<ColumnLog> Columns { get; set; } = new List<ColumnLog>();
        public string ProcessingResult { get; set; } = string.Empty;

        public ColumnLog CurrentColumn
        {
            get { return Columns[Columns.Count - 1];  }
        }

        public void NewColumn(string name, object value)
        {
            Columns.Add(new ColumnLog { InputColumnName = name, InputColumnValue = value, ProcessingResult = ColumnProcessingResult.STARTED });
        }

        public XElement GetXElement()
        {
            var rowElement = new XElement("row",
                new XAttribute("duration", Duration),
                new XAttribute("start", StartTime.ToString("hhmmss.FFF")),
                new XAttribute("end", EndTime.ToString("hhmmss.FFF")),
                new XAttribute("processing_result", ProcessingResult));

            foreach (DictionaryEntry att in Attributes)
            {
                rowElement.Add(new XAttribute(att.Key.ToString(), att.Value));
            }

            foreach (var column in Columns)
            {
                rowElement.Add(column.GetXElement());
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

                rowElement.Add(logElement);
            }

            return rowElement;
        }
    }
}
