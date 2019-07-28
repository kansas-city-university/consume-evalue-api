using System;
using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class TimeFrameApi : EvalueApi
    {

        private readonly string _url;

        public TimeFrameApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Get all of the time frames in the system between two given dates
        /// </summary>
        /// <returns></returns>
        public TimeFramesResponse GetAllTimeFrames(DateTime beginDate, DateTime endDate)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // ********************* Parameter ***********************************
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "startdate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(beginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // ********************* Parameter ***********************************
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "enddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(endDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);


            // ********************* Parameter ***********************************
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "statusid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode("1"));

            // Get the call node
            callNode.AppendChild(argNode3);

            // ************************** Actual Call *****************************
            var eValueApiService = new EValueTimeFrameApi.TimeFrame_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getAll(newRequest.InnerXml));

            var result = ExtractResponseFromXml(responseXml);

            return result;

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static TimeFramesResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<TimeFrame> result;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                result = new List<TimeFrame>();

                foreach (XmlElement siteXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(siteXml.OuterXml);

                    result.Add(new TimeFrame()
                    {
                        TimeFrameId = int.Parse(doc.SelectNodes("//d[@NAME='timeframeid']")?[0].InnerText),
                        TimeFrameLabel = doc.SelectNodes("//d[@NAME='timeframelabel']")?[0].InnerText,
                        TimeFrameBegin = DateTime.Parse(doc.SelectNodes("//d[@NAME='timeframebegin']")?[0].InnerText),
                        TimeFrameEnd = DateTime.Parse(doc.SelectNodes("//d[@NAME='timeframeend']")?[0].InnerText)
                    });
                }
            }
            else
            {
                result = null;
            }

            // Now return your results
            return new TimeFramesResponse()
            {
                TimeFrames = result,
                Status = responseValue
            };

        }


        /// <summary>
        /// Get all of the time frames in the system between two given dates
        /// </summary>
        /// <returns></returns>
        public TimeFrameResponse Get(int timeFrameId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // ********************* Parameter ***********************************
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "timeframeid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(timeFrameId.ToString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // ************************** Actual Call *****************************
            var eValueApiService = new EValueTimeFrameApi.TimeFrame_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            var result = ExtractSingleResponseFromXml(responseXml, timeFrameId);

            return result;

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        private TimeFrameResponse ExtractSingleResponseFromXml(XmlDocument responseXml, int timeFrameId)
        {

            TimeFrame result;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {
                result = new TimeFrame()
                {
                    TimeFrameId = int.Parse(timeFrameId.ToString()),
                    TimeFrameBegin = DateTime.Parse(responseXml.SelectNodes("//d[@NAME='timeframebegin']")?[0].InnerText),
                    TimeFrameEnd = DateTime.Parse(responseXml.SelectNodes("//d[@NAME='timeframeend']")?[0].InnerText),
                    TimeFrameLabel = responseXml.SelectNodes("//d[@NAME='timeframelabel']")?[0].InnerText
                };

            }
            else
            {
                result = null;
            }

            // Now return your results
            return new TimeFrameResponse()
            {
                TimeFrame = result,
                Status = responseValue
            };

        }

    }
}
