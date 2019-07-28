using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class InstitutionApi : EvalueApi
    {

        private readonly string _url;

        public InstitutionApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the users for a given EValue sub unit.
        /// </summary>
        /// <returns></returns>
        public InstitutionUserResponse GetSubUnitUsers(int statusId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "subunitid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(SubUnitId));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "statusid";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            var eValueApiService = new EValueInstitutionApi.Institution_1_0Service {Url = _url};

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getSubUnitUsers(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }

        /// <summary>
        /// Gets all of the users for a given EValue sub unit.
        /// </summary>
        /// <returns></returns>
        public InstitutionUserResponse GetSubUnitUsers(int statusId, int rankTypeId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "subunitid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(SubUnitId));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "statusid";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "rankid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(rankTypeId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode3);

            var eValueApiService = new EValueInstitutionApi.Institution_1_0Service { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getSubUnitUsers(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }


        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static InstitutionUserResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<InstitutionUser> resultUser;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultUser = new List<InstitutionUser>();

                foreach (XmlElement institutionUserXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(institutionUserXml.OuterXml);

                    resultUser.Add(new InstitutionUser()
                    {
                        UserId = int.Parse(doc.SelectNodes("//d[@NAME='userid']")?[0].InnerText),
                        RankId = int.Parse(doc.SelectNodes("//d[@NAME='rankid']")?[0].InnerText),
                        LastName = doc.SelectNodes("//d[@NAME='lastname']")?[0].InnerText,
                        Initial = doc.SelectNodes("//d[@NAME='initial']")?[0].InnerText,
                        FirstName = doc.SelectNodes("//d[@NAME='firstname']")?[0].InnerText,
                        RankLabel = doc.SelectNodes("//d[@NAME='rankLabel']")?[0].InnerText,
                        StatusId = int.Parse(doc.SelectNodes("//d[@NAME='statusid']")?[0].InnerText)
                    });

                }

            }
            else
            {
                resultUser = null;
            }

            // Now return your results
            return new InstitutionUserResponse()
            {
                IntitutionUsers = resultUser,
                Status = responseValue
            };

        }



    }
}
