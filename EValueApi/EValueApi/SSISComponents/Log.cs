using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EValueApi.SSISComponents
{
    public class Log : LogGroup
    {
        public int BatchId { get; set; } = 0;
        public IList<RowLog> Rows { get; set; } = new List<RowLog>();
        public string ProcessingResult { get; set; } = string.Empty;

        public Log(int batchId)
        {
            BatchId = batchId;
        }

        public RowLog CurrentRow
        {
            get
            {
                return Rows[Rows.Count - 1];
            }
        }

        public void NewRow()
        {
            Rows.Add(new RowLog
            {
                ProcessingResult = RowProcessingResult.STARTED
            });
        }

        public XElement GetXElement()
        {
            var logElement = new XElement("log",
                new XAttribute("batch_id", BatchId),
                new XAttribute("start", StartTime.ToString("yyyy-MM-dd hh:mm:ss:FFF")),
                new XAttribute("end", EndTime.ToString("yyyy-MM-dd hh:mm:ss:FFF")),
                new XAttribute("duration", Duration),
                new XAttribute("processing_result", ProcessingResult)
            );

            foreach (var row in Rows)
            {
                logElement.Add(row.GetXElement());
            }

            return logElement;
        }
    }
}
