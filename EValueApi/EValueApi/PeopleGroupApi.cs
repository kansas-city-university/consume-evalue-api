using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class PeopleGroupApi : EvalueApi
    {

        private readonly string _url;

        public PeopleGroupApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public PeopleGroupsResponse GetAll()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValuePeopleGroupApi.PeopleGroup_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getAll(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }

        public InstitutionUserResponse GetUsers(string peopleGroupId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);
            
            // Parameter 1
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "groupid";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(peopleGroupId));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter 2
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "subunitid";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(SubUnitId));

            // Get the call node
            callNode.AppendChild(argNode2);

            var eValueApiService = new EValuePeopleGroupApi.PeopleGroup_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getUsersByGroup(newRequest.InnerXml));

            return ExtractGroupUserResponseFromXml(responseXml);
        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static PeopleGroupsResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<PeopleGroup> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<PeopleGroup>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new PeopleGroup()
                    {
                        GroupId = int.Parse(doc.SelectNodes("//d[@NAME='groupid']")?[0].InnerText),
                        Name = doc.SelectNodes("//d[@NAME='groupName']")?[0].InnerText
                    });

                }

            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new PeopleGroupsResponse()
            {
                PeopleGroups = resultValue,
                Status = responseValue
            };

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static InstitutionUserResponse ExtractGroupUserResponseFromXml(XmlDocument responseXml)
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
                        RankId = 0,
                        LastName = doc.SelectNodes("//d[@NAME='lastname']")?[0].InnerText,
                        Initial = doc.SelectNodes("//d[@NAME='initial']")?[0].InnerText,
                        FirstName = doc.SelectNodes("//d[@NAME='firstname']")?[0].InnerText,
                        RankLabel = "Not Defined in this API",
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
