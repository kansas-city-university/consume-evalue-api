using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class UserApi : EvalueApi
    {

        private readonly string _url;

        public UserApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Get a user with the EValue User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserResponse Get(string userId)
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

            var eValueApiService = new EValueUserApi.User_1_0Service {Url = _url};

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            var result = ExtractResponseFromXml(responseXml);

            return result;

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static UserResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            User resultUser;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {
                resultUser = new User
                {
                    UserId = int.Parse(responseXml.SelectNodes("//d[@NAME='userid']")?[0].InnerText),
                    Title = responseXml.SelectNodes("//d[@NAME='title']")?[0].InnerText,
                    Ssn = responseXml.SelectNodes("//d[@NAME='ssn']")?[0].InnerText,
                    RankId = int.Parse(responseXml.SelectNodes("//d[@NAME='rankid']")?[0].InnerText),
                    Password = responseXml.SelectNodes("//d[@NAME='password']")?[0].InnerText,
                    Login = responseXml.SelectNodes("//d[@NAME='login']")?[0].InnerText,
                    LastName = responseXml.SelectNodes("//d[@NAME='lastname']")?[0].InnerText,
                    Initial = responseXml.SelectNodes("//d[@NAME='initial']")?[0].InnerText,
                    Gender = responseXml.SelectNodes("//d[@NAME='gender']")?[0].InnerText,
                    FirstName = responseXml.SelectNodes("//d[@NAME='firstname']")?[0].InnerText,
                    Email = responseXml.SelectNodes("//d[@NAME='email']")?[0].InnerText,
                    Birthdate = ConvertXmlDateValue(responseXml.SelectNodes("//d[@NAME='birthdate']")?[0].InnerText)
                };

            }
            else
            {
                resultUser = null;
            }

            // Now return your results
            return new UserResponse
            {
                User = resultUser,
                Status = responseValue
            };

        }

        /// <summary>
        /// Get roles for a user with the EValue User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RolesResponse GetRoles(string userId)
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

            var eValueApiService = new EValueUserApi.User_1_0Service { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getRoles(newRequest.InnerXml));

            var result = RoleApi.ExtractResponseFromXml(responseXml);

            return result;

        }



    }
}
