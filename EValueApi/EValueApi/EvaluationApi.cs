using System;
using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class EvaluationApi : EvalueApi
    {

        private readonly string _url;

        public EvaluationApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }


        /// <summary>
        /// Gets the evaluation responses
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId">-1=Suspended, 0=Open, 1=Complete, 2=Review release, 3=Deleted, 4=Review Release on Hold, 5=Deleted, 6=Completed By Trainee, 7=Expired, 8=Revived Expired</param>
        /// <returns></returns>
        public EvaluationItemsResponse GetResponses(string activityId, DateTime scheduleBeginDate, DateTime scheduleEndDate, int statusId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "schedulebegindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(scheduleBeginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "scheduleenddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(scheduleEndDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            callNode.AppendChild(argNode3);

            // Parameter
            XmlNode argNode4 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute4 = newRequest.CreateAttribute("name");
            nameAttribute4.Value = "EvaluationStatus";

            // ReSharper disable once PossibleNullReferenceException
            argNode4.Attributes.Append(nameAttribute4);
            argNode4.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode4);
            
            var eValueApiService = new EValueEvaluationApi.Evaluation_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, scheduleBeginDate, scheduleEndDate, statusId, 0);

        }

        /// <summary>
        /// Gets the evaluation responses
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId">-1=Suspended, 0=Open, 1=Complete, 2=Review release, 3=Deleted, 4=Review Release on Hold, 5=Deleted, 6=Completed By Trainee, 7=Expired, 8=Revived Expired</param>
        /// <param name="modifiedSince"></param>
        /// <returns></returns>
        public EvaluationItemsResponse GetResponses(string activityId, DateTime scheduleBeginDate, DateTime scheduleEndDate, int statusId, DateTime modifiedSince)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "schedulebegindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(scheduleBeginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "scheduleenddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(scheduleEndDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            callNode.AppendChild(argNode3);

            // Parameter
            XmlNode argNode4 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute4 = newRequest.CreateAttribute("name");
            nameAttribute4.Value = "EvaluationStatus";

            // ReSharper disable once PossibleNullReferenceException
            argNode4.Attributes.Append(nameAttribute4);
            argNode4.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode4);

            // Parameter
            XmlNode argNode5 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute5 = newRequest.CreateAttribute("name");
            nameAttribute5.Value = "modifiedsince";

            // ReSharper disable once PossibleNullReferenceException
            argNode5.Attributes.Append(nameAttribute5);
            argNode5.AppendChild(newRequest.CreateTextNode(modifiedSince.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode5);


            var eValueApiService = new EValueEvaluationApi.Evaluation_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, scheduleBeginDate, scheduleEndDate, statusId, 0);

        }


        /// <summary>
        /// Gets the evaluation responses
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId">-1=Suspended, 0=Open, 1=Complete, 2=Review release, 3=Deleted, 4=Review Release on Hold, 5=Deleted, 6=Completed By Trainee, 7=Expired, 8=Revived Expired</param>
        /// <param name="evaluationFormTypeId"></param>
        /// <returns></returns>
        public EvaluationItemsResponse GetResponses(string activityId, DateTime scheduleBeginDate, DateTime scheduleEndDate, int statusId, int evaluationFormTypeId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "schedulebegindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(scheduleBeginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "scheduleenddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(scheduleEndDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            callNode.AppendChild(argNode3);

            // Parameter
            XmlNode argNode4 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute4 = newRequest.CreateAttribute("name");
            nameAttribute4.Value = "EvaluationStatus";

            // ReSharper disable once PossibleNullReferenceException
            argNode4.Attributes.Append(nameAttribute4);
            argNode4.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode4);

            // Parameter
            XmlNode argNode5 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute5 = newRequest.CreateAttribute("name");
            nameAttribute5.Value = "formid";

            // ReSharper disable once PossibleNullReferenceException
            argNode5.Attributes.Append(nameAttribute5);
            argNode5.AppendChild(newRequest.CreateTextNode(evaluationFormTypeId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode5);

            var eValueApiService = new EValueEvaluationApi.Evaluation_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, scheduleBeginDate, scheduleEndDate, statusId, evaluationFormTypeId);

        }

        /// <summary>
        /// Gets the evaluation responses
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId">-1=Suspended, 0=Open, 1=Complete, 2=Review release, 3=Deleted, 4=Review Release on Hold, 5=Deleted, 6=Completed By Trainee, 7=Expired, 8=Revived Expired</param>
        /// <param name="evaluationFormTypeId"></param>
        /// <param name="modifiedSince"></param>
        /// <returns></returns>
        public EvaluationItemsResponse GetResponses(string activityId, DateTime scheduleBeginDate, DateTime scheduleEndDate, int statusId, int evaluationFormTypeId, DateTime modifiedSince)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "schedulebegindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(scheduleBeginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "scheduleenddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(scheduleEndDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            callNode.AppendChild(argNode3);

            // Parameter
            XmlNode argNode4 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute4 = newRequest.CreateAttribute("name");
            nameAttribute4.Value = "EvaluationStatus";

            // ReSharper disable once PossibleNullReferenceException
            argNode4.Attributes.Append(nameAttribute4);
            argNode4.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode4);

            // Parameter
            XmlNode argNode5 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute5 = newRequest.CreateAttribute("name");
            nameAttribute5.Value = "formid";

            // ReSharper disable once PossibleNullReferenceException
            argNode5.Attributes.Append(nameAttribute5);
            argNode5.AppendChild(newRequest.CreateTextNode(evaluationFormTypeId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode5);

            // Parameter
            XmlNode argNode6 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute6 = newRequest.CreateAttribute("name");
            nameAttribute6.Value = "modifiedsince";

            // ReSharper disable once PossibleNullReferenceException
            argNode6.Attributes.Append(nameAttribute6);
            argNode6.AppendChild(newRequest.CreateTextNode(modifiedSince.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode6);

            var eValueApiService = new EValueEvaluationApi.Evaluation_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, scheduleBeginDate, scheduleEndDate, statusId, evaluationFormTypeId);

        }


        /// <summary>
        /// Gets the evaluation responses
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId">-1=Suspended, 0=Open, 1=Complete, 2=Review release, 3=Deleted, 4=Review Release on Hold, 5=Deleted, 6=Completed By Trainee, 7=Expired, 8=Revived Expired</param>
        /// <param name="evaluationFormTypeId"></param>
        /// <param name="subjectUserId"></param>
        /// <returns></returns>
        public EvaluationItemsResponse GetResponsesUsingSubjectId(string activityId, DateTime scheduleBeginDate, DateTime scheduleEndDate, int statusId, int evaluationFormTypeId, int subjectUserId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "schedulebegindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(scheduleBeginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "scheduleenddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(scheduleEndDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            callNode.AppendChild(argNode3);

            // Parameter
            XmlNode argNode4 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute4 = newRequest.CreateAttribute("name");
            nameAttribute4.Value = "EvaluationStatus";

            // ReSharper disable once PossibleNullReferenceException
            argNode4.Attributes.Append(nameAttribute4);
            argNode4.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode4);

            // Parameter
            XmlNode argNode5 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute5 = newRequest.CreateAttribute("name");
            nameAttribute5.Value = "formid";

            // ReSharper disable once PossibleNullReferenceException
            argNode5.Attributes.Append(nameAttribute5);
            argNode5.AppendChild(newRequest.CreateTextNode(evaluationFormTypeId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode5);

            // Parameter
            XmlNode argNode6 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute6 = newRequest.CreateAttribute("name");
            nameAttribute6.Value = "SubjectUserId";

            // ReSharper disable once PossibleNullReferenceException
            argNode6.Attributes.Append(nameAttribute6);
            argNode6.AppendChild(newRequest.CreateTextNode(subjectUserId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode6);

            var eValueApiService = new EValueEvaluationApi.Evaluation_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, scheduleBeginDate, scheduleEndDate, statusId, evaluationFormTypeId);

        }


        /// <summary>
        /// Gets the evaluation responses
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId">-1=Suspended, 0=Open, 1=Complete, 2=Review release, 3=Deleted, 4=Review Release on Hold, 5=Deleted, 6=Completed By Trainee, 7=Expired, 8=Revived Expired</param>
        /// <param name="evaluationFormTypeId"></param>
        /// <param name="evaluatorUserId"></param>
        /// <returns></returns>
        public EvaluationItemsResponse GetResponsesUsingEvaluatorId(string activityId, DateTime scheduleBeginDate, DateTime scheduleEndDate, int statusId, int evaluationFormTypeId, int evaluatorUserId)
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            // Parameter
            XmlNode argNode = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute = newRequest.CreateAttribute("name");
            nameAttribute.Value = "schedulebegindate";

            // ReSharper disable once PossibleNullReferenceException
            argNode.Attributes.Append(nameAttribute);
            argNode.AppendChild(newRequest.CreateTextNode(scheduleBeginDate.ToShortDateString()));

            // Get the call node
            var callNode = newRequest.GetElementsByTagName("call")[0];  // Assumption this is here.  It is built in the constructor
            callNode.AppendChild(argNode);

            // Parameter
            XmlNode argNode2 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute2 = newRequest.CreateAttribute("name");
            nameAttribute2.Value = "scheduleenddate";

            // ReSharper disable once PossibleNullReferenceException
            argNode2.Attributes.Append(nameAttribute2);
            argNode2.AppendChild(newRequest.CreateTextNode(scheduleEndDate.ToShortDateString()));

            // Get the call node
            callNode.AppendChild(argNode2);

            // Parameter
            XmlNode argNode3 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute3 = newRequest.CreateAttribute("name");
            nameAttribute3.Value = "activityid";

            // ReSharper disable once PossibleNullReferenceException
            argNode3.Attributes.Append(nameAttribute3);
            argNode3.AppendChild(newRequest.CreateTextNode(activityId));

            // Get the call node
            callNode.AppendChild(argNode3);

            // Parameter
            XmlNode argNode4 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute4 = newRequest.CreateAttribute("name");
            nameAttribute4.Value = "EvaluationStatus";

            // ReSharper disable once PossibleNullReferenceException
            argNode4.Attributes.Append(nameAttribute4);
            argNode4.AppendChild(newRequest.CreateTextNode(statusId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode4);

            // Parameter
            XmlNode argNode5 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute5 = newRequest.CreateAttribute("name");
            nameAttribute5.Value = "formid";

            // ReSharper disable once PossibleNullReferenceException
            argNode5.Attributes.Append(nameAttribute5);
            argNode5.AppendChild(newRequest.CreateTextNode(evaluationFormTypeId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode5);

            // Parameter
            XmlNode argNode6 = newRequest.CreateElement("arg");

            XmlAttribute nameAttribute6 = newRequest.CreateAttribute("name");
            nameAttribute6.Value = "EvaluatorUserId";

            // ReSharper disable once PossibleNullReferenceException
            argNode6.Attributes.Append(nameAttribute6);
            argNode6.AppendChild(newRequest.CreateTextNode(evaluatorUserId.ToString()));

            // Get the call node
            callNode.AppendChild(argNode6);

            var eValueApiService = new EValueEvaluationApi.Evaluation_1_0bService() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.get(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml, scheduleBeginDate, scheduleEndDate, statusId, evaluationFormTypeId);

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <param name="scheduleEndDate"></param>
        /// <param name="statusId"></param>
        /// <param name="scheduleBeginDate"></param>
        /// <param name="evaluationFormTypeId"></param>
        /// <returns></returns>
        public static EvaluationItemsResponse ExtractResponseFromXml(XmlDocument responseXml, DateTime scheduleBeginDate,
            DateTime scheduleEndDate, int statusId, int evaluationFormTypeId)
        {

            List<EvaluationItem> resultValue;
            int? nullIntegerValue = new Nullable<int>();

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<EvaluationItem>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new EvaluationItem()
                    {
                        SiteId = int.Parse(doc.SelectNodes("//d[@NAME='siteid']")?[0].InnerText),
                        SiteName = doc.SelectNodes("//d[@NAME='sitename']")?[0].InnerText,
                        ActivityId = int.Parse(doc.SelectNodes("//d[@NAME='activityid']")?[0].InnerText),
                        Name = doc.SelectNodes("//d[@NAME='name']")?[0].InnerText,
                        Abbreviation = doc.SelectNodes("//d[@NAME='abbr']")?[0].InnerText,
                        TimeFrameId = int.Parse(doc.SelectNodes("//d[@NAME='timeframeid']")?[0].InnerText),
                        TimeFrameLabel = doc.SelectNodes("//d[@NAME='timeframelabel']")?[0].InnerText,
                        Choice = doc.SelectNodes("//d[@NAME='Choice']")?[0].InnerText,
                        EssayText = doc.SelectNodes("//d[@NAME='EssayText']")?[0].InnerText,
                        EvaluatorUserId = int.Parse(doc.SelectNodes("//d[@NAME='EvaluatorUserId']")?[0].InnerText),
                        EvaluatorExternalId = doc.SelectNodes("//d[@NAME='EvaluatorExternalId']")?[0].InnerText,
                        EvaluatorExternalIdLabel = doc.SelectNodes("//d[@NAME='EvaluatorExternalIdLabel']")?[0].InnerText,
                        NumericAnswer = doc.SelectNodes("//d[@NAME='NumericAnswer']")?[0].InnerText,
                        IsConfidential = doc.SelectNodes("//d[@NAME='Confidential']")?[0].InnerText == "1",
                        IsMandatory = doc.SelectNodes("//d[@NAME='Mandatory']")?[0].InnerText == "1",
                        QuestionId = ToNullableInt(doc.SelectNodes("//d[@NAME='QuestionId']")?[0].InnerText),
                        QuestionText = doc.SelectNodes("//d[@NAME='QuestionText']")?[0].InnerText,
                        QuestionTopic = doc.SelectNodes("//d[@NAME='QuestionTopic']")?[0].InnerText,
                        QuestionTypeId = ToNullableInt(doc.SelectNodes("//d[@NAME='QuestionTypeId']")?[0].InnerText),
                        QuestionTypeDesc = doc.SelectNodes("//d[@NAME='QuestionTypeDesc']")?[0].InnerText,
                        RowId = doc.SelectNodes("//d[@NAME='RowId']")?[0].InnerText,
                        SortOrder = ToNullableInt(doc.SelectNodes("//d[@NAME='SortOrder']")?[0].InnerText),
                        SubjectUserId = ToNullableInt(doc.SelectNodes("//d[@NAME='SubjectUserId']")?[0].InnerText),
                        SubjectExternalId = doc.SelectNodes("//d[@NAME='SubjectExternalId']")?[0].InnerText,
                        SubjectExternalIdLabel = doc.SelectNodes("//d[@NAME='SubjectExternalIdLabel']")?[0].InnerText,
                        SubjectLegacyExternalId = doc.SelectNodes("//d[@NAME='SubjectLegacyExternalId']")?[0].InnerText,
                        EvaluatorLegacyExternalId = doc.SelectNodes("//d[@NAME='EvaluatorLegacyExternalId']")?[0].InnerText,
                        Eimnum = int.Parse(doc.SelectNodes("//d[@NAME='eimnum']")?[0].InnerText),
                        EvaluationRequestId = int.Parse(doc.SelectNodes("//d[@NAME='evaluationrequestid']")?[0].InnerText),
                        CompletedDate = ConvertXmlDateValue(doc.SelectNodes("//d[@NAME='CompletedDate']")?[0].InnerText),
                        LastEvaluatorUpdateDate = ConvertXmlDateValue(doc.SelectNodes("//d[@NAME='LastEvaluatorUpdateDate']")?[0].InnerText),
                        StatusId = int.Parse(doc.SelectNodes("//d[@NAME='EvaluationStatus']")?[0].InnerText),
                        EvaluationFormTypeId = int.Parse(doc.SelectNodes("//d[@NAME='formid']")?[0].InnerText)
                    });
                }
            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new EvaluationItemsResponse()
            {
                EvaluationItems = resultValue,
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
