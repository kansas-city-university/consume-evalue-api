using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;
using System;

namespace EValueApi
{
    public class ScheduleApi : EvalueApi
    {

        private readonly string _url;

        public ScheduleApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public SchedulesResponse GetAll(string userId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "userid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(userId));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            var eValueApiService = new EValueScheduleApi.Schedule_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.findAll(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public SchedulesResponse GetAll(DateTime beginDate, DateTime endDate, string userId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "begindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(beginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "enddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(endDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "userid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(userId));

            // Get the call node
            callNode.AppendChild(argNode3);

            var eValueApiService = new EValueScheduleApi.Schedule_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.findAll(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }


        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static SchedulesResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<Schedule> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<Schedule>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new Schedule()
                    {
                        UserId = int.Parse(doc.SelectNodes("//d[@NAME='userid']")?[0].InnerText),
                        SiteId = int.Parse(doc.SelectNodes("//d[@NAME='siteid']")?[0].InnerText),
                        ActivityId = int.Parse(doc.SelectNodes("//d[@NAME='activityid']")?[0].InnerText),
                        TeamId = ToNullableInt(doc.SelectNodes("//d[@NAME='teamid']")?[0].InnerText),
                        TimeFrameId = int.Parse(doc.SelectNodes("//d[@NAME='timeframeid']")?[0].InnerText),
                        BeginDate = ConvertXmlDateValue(doc.SelectNodes("//d[@NAME='begindate']")?[0].InnerText),
                        EndDate = ConvertXmlDateValue(doc.SelectNodes("//d[@NAME='enddate']")?[0].InnerText),
                        EvaluationAction = int.Parse(doc.SelectNodes("//d[@NAME='evaluationaction']")?[0].InnerText),
                        ScheduleId = int.Parse(doc.SelectNodes("//d[@NAME='scheduleid']")?[0].InnerText)
                    });

                }

            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new SchedulesResponse()
            {
                Schedules = resultValue,
                Status = responseValue
            };

        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public EvaluationActionsResponse GetEvaluationActions()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValueScheduleApi.Schedule_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getEvaluationActionOptions(newRequest.InnerXml));

            return ExtractEvalActionsResponseFromXml(responseXml);

        }


        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static EvaluationActionsResponse ExtractEvalActionsResponseFromXml(XmlDocument responseXml)
        {

            List<EvaluationAction> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<EvaluationAction>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new EvaluationAction()
                    {
                        EvaluationActionId = int.Parse(doc.SelectNodes("//d[@NAME='evaluationaction']")?[0].InnerText),
                        Description = doc.SelectNodes("//d[@NAME='description']")?[0].InnerText
                    });

                }

            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new EvaluationActionsResponse()
            {
                EvaluationActions = resultValue,
                Status = responseValue
            };

        }


        public static int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

    }
}
