using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class TeamApi : EvalueApi
    {

        private readonly string _url;

        public TeamApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public TeamsResponse GetAllTeams(string activityId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            var eValueApiService = new EValueTeamApi.Team_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getAll(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, activityId);

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public static TeamsResponse ExtractResponseFromXml(XmlDocument responseXml, string activityId)
        {

            List<Team> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<Team>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new Team()
                    {
                        TeamId = int.Parse(doc.SelectNodes("//d[@NAME='teamid']")?[0].InnerText),
                        Name = doc.SelectNodes("//d[@NAME='name']")?[0].InnerText,
                        ActivityId = int.Parse(activityId)
                    });

                }

            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new TeamsResponse()
            {
                Teams = resultValue,
                Status = responseValue
            };

        }
    }
}
