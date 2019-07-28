using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class ActivityApi : EvalueApi
    {

        private readonly string _url;

        public ActivityApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public ActivitiesResponse GetAllActivities()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "statusid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode("1"));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            var eValueApiService = new EValueActivityApi.Activity_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getAll(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        private ActivitiesResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<Activity> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<Activity>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new Activity()
                    {
                        SiteId = int.Parse(doc.SelectNodes("//d[@NAME='siteid']")?[0].InnerText),
                        ActivityId = int.Parse(doc.SelectNodes("//d[@NAME='activityid']")?[0].InnerText),
                        Name = doc.SelectNodes("//d[@NAME='name']")?[0].InnerText,
                        Abbreviation = doc.SelectNodes("//d[@NAME='abbr']")?[0].InnerText,
                        Credit = float.Parse(doc.SelectNodes("//d[@NAME='credit']")?[0].InnerText)
                    });

                }

            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new ActivitiesResponse()
            {
                Activities = resultValue,
                Status = responseValue
            };

        }
    }
}
