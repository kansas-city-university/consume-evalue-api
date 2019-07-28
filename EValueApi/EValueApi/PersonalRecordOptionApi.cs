using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class PersonalRecordOptionApi : EvalueApi
    {

        private readonly string _url;

        public PersonalRecordOptionApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public RequirementOptionsResponse GetAllRequirementOptions()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValuePersonalRecordsOptionsApi.IandCOptions_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getRequirementOptions(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }
        

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static RequirementOptionsResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<RequirementOption> result;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                result = new List<RequirementOption>();

                foreach (XmlElement institutionUserXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(institutionUserXml.OuterXml);

                    result.Add(new RequirementOption()
                    {
                        RequirementId = int.Parse(doc.SelectNodes("//d[@NAME='requirementid']")?[0].InnerText),
                        RequirementLabel = doc.SelectNodes("//d[@NAME='reqLabel']")?[0].InnerText
                    });

                }

            }
            else
            {
                result = null;
            }

            // Now return your results
            return new RequirementOptionsResponse()
            {
                RequirementOptions = result,
                Status = responseValue
            };

        }


        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public RequirementStatusOptionsResponse GetAllRequirementStatusOptions()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValuePersonalRecordsOptionsApi.IandCOptions_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getStatusOptions(newRequest.InnerXml));

            return ExtractStatusOptionResponseFromXml(responseXml);

        }


        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static RequirementStatusOptionsResponse ExtractStatusOptionResponseFromXml(XmlDocument responseXml)
        {

            List<RequirementStatusOption> result;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                result = new List<RequirementStatusOption>();

                foreach (XmlElement institutionUserXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(institutionUserXml.OuterXml);

                    result.Add(new RequirementStatusOption()
                    {
                        StatusId = int.Parse(doc.SelectNodes("//d[@NAME='statusid']")?[0].InnerText),
                        StatusLabel = doc.SelectNodes("//d[@NAME='statusLabel']")?[0].InnerText
                    });

                }

            }
            else
            {
                result = null;
            }

            // Now return your results
            return new RequirementStatusOptionsResponse()
            {
                RequirementStatusOptions = result,
                Status = responseValue
            };

        }


        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public RequirementTypeOptionsResponse GetAllRequirementTypeOptions()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValuePersonalRecordsOptionsApi.IandCOptions_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getTypeOptions(newRequest.InnerXml));

            return ExtractTypeOptionResponseFromXml(responseXml);

        }


        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static RequirementTypeOptionsResponse ExtractTypeOptionResponseFromXml(XmlDocument responseXml)
        {

            List<RequirementTypeOption> result;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                result = new List<RequirementTypeOption>();

                foreach (XmlElement institutionUserXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(institutionUserXml.OuterXml);

                    result.Add(new RequirementTypeOption()
                    {
                        TypeId = int.Parse(doc.SelectNodes("//d[@NAME='typeid']")?[0].InnerText),
                        TypeLabel = doc.SelectNodes("//d[@NAME='typeLabel']")?[0].InnerText
                    });

                }

            }
            else
            {
                result = null;
            }

            // Now return your results
            return new RequirementTypeOptionsResponse()
            {
                RequirementTypeOptions = result,
                Status = responseValue
            };

        }

    }
}
