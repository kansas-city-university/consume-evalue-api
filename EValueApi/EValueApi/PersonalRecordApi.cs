using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EValueApi.Business;
using EValueApi.Communication;
using EValueApi.Communication.PersonalRecords;

namespace EValueApi
{
    public class PersonalRecordApi : EvalueApi
    {
        private readonly string _url;
        private readonly EValuePersonalRecordsApi.IandC_1_0Service _service;

        public PersonalRecordApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
            _service = new EValuePersonalRecordsApi.IandC_1_0Service() { Url = _url };
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public PersonalRecordsResponse GetUserPersonalRecords(string userId)
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

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(_service.getUserData(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);
        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static PersonalRecordsResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<PersonalRecord> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<PersonalRecord>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new PersonalRecord()
                    {
                        ExpireDate = ConvertXmlDateValue(doc.SelectNodes("//d[@NAME='expiredate']")?[0].InnerText),
                        IcId = int.Parse(doc.SelectNodes("//d[@NAME='icid']")?[0].InnerText),
                        EventDate = ConvertXmlDateValue(doc.SelectNodes("//d[@NAME='eventdate']")?[0].InnerText),
                        RequirementId = int.Parse(doc.SelectNodes("//d[@NAME='requirementid']")?[0].InnerText),
                        TypeId = int.Parse(doc.SelectNodes("//d[@NAME='typeid']")?[0].InnerText),
                        Note = doc.SelectNodes("//d[@NAME='note']")?[0].InnerText,
                        StatusId = int.Parse(doc.SelectNodes("//d[@NAME='statusid']")?[0].InnerText),
                        UserId = int.Parse(doc.SelectNodes("//d[@NAME='userid']")?[0].InnerText),
                        IsArchive = (int.Parse(doc.SelectNodes("//d[@NAME='archive']")?[0].InnerText) == 1)
                    });
                }
            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new PersonalRecordsResponse()
            {
                PeronalRecords = resultValue,
                Status = responseValue
            };

        }

        public CreateResponse Create(PersonalRecord record)
        {
            base.RemoveChildrenFromCallNode();
           
            base.AddChildToCallNode("arg", record.UserId, "name", "userid");
            base.AddChildToCallNode("arg", record.TypeId, "name", "typeid");
            base.AddChildToCallNode("arg", record.RequirementId, "name", "requirementid");
            base.AddChildToCallNode("arg", record.StatusId, "name", "statusid");
            base.AddChildToCallNode("arg", record.EventDate, "name", "eventdate");
            base.AddChildToCallNode("arg", record.ExpireDate, "name", "expiredate");
            base.AddChildToCallNode("arg", record.Note, "name", "note");

            var xmlRequest = base.XRequestBase.ToString();
            var response = new CreateResponse(_service.create(xmlRequest));
            response.RequestXml = xmlRequest; //save for logging purposes

            return response;
        }

        public UpdateResponse Update(PersonalRecord record)
        {
            base.RemoveChildrenFromCallNode();

            base.AddChildToCallNode("arg", record.UserId, "name", "userid");
            base.AddChildToCallNode("arg", record.IcId, "name", "icid");
            base.AddChildToCallNode("arg", record.TypeId, "name", "typeid");
            base.AddChildToCallNode("arg", record.RequirementId, "name", "requirementid");
            base.AddChildToCallNode("arg", record.StatusId, "name", "statusid");
            base.AddChildToCallNode("arg", record.EventDate, "name", "eventdate");
            base.AddChildToCallNode("arg", record.ExpireDate, "name", "expiredate");
            base.AddChildToCallNode("arg", record.Note, "name", "note");
            base.AddChildToCallNode("arg", record.IsArchive, "name", "archive");

            var xmlRequest = base.XRequestBase.ToString();
            var response = new UpdateResponse(_service.update(xmlRequest));
            response.RequestXml = xmlRequest; //save for logging purposes

            return response;
        }
    }
}
