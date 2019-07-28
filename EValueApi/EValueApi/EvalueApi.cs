using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Net;

namespace EValueApi
{
    public abstract class EvalueApi
    {
        protected XmlDocument RequestBase { get; set; }
        protected XDocument XRequestBase { get; set; }

        protected string ClientId;
        protected string SubUnitId;
        protected string Password;

        /// <summary>
        /// Constructor
        /// </summary>
        protected EvalueApi(string clientId, string password, string subUnitId)
        {
            ClientId = clientId;
            Password = password;
            SubUnitId = subUnitId;

            RequestBase = BuildBaseDocument();
            XRequestBase  = XDocument.Parse(RequestBase.OuterXml);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;
        }

        /// <summary>
        /// The base constructor needs to build up the base XML request document that will be used for the subsequent API calls
        /// </summary>
        /// <returns></returns>
        private XmlDocument BuildBaseDocument()
        {
            // Build up the base request
            var result = new XmlDocument { PreserveWhitespace = true };

            XmlNode docNode = result.CreateXmlDeclaration("1.0", "UTF-8", null);
            result.AppendChild(docNode);

            XmlNode mNode = result.CreateElement("m");

            XmlAttribute cidAttribute = result.CreateAttribute("cid");
            cidAttribute.Value = ClientId;

            // ReSharper disable once PossibleNullReferenceException - is defined above
            mNode.Attributes.Append(cidAttribute);

            XmlAttribute pwdAttribute = result.CreateAttribute("pwd");
            pwdAttribute.Value = Password;
            mNode.Attributes.Append(pwdAttribute);

            XmlAttribute subunitidAttribute = result.CreateAttribute("subunitid");
            subunitidAttribute.Value = SubUnitId;
            mNode.Attributes.Append(subunitidAttribute);

            XmlNode callNode = result.CreateElement("call");

            mNode.AppendChild(callNode);

            result.AppendChild(mNode);

            return result;
        }

        protected static string CleanEValueDateString(string eValueDate)
        {
            
            // Get rid of characters that EValue will be putting in here
            var cleanedString = eValueDate.Replace("{ts '", "").Replace("'}", "");

            return cleanedString;
        }

        /// <summary>
        /// Get a place to do this conversion easily to keep calling code clean
        /// </summary>
        /// <param name="xmlInnerText"></param>
        /// <returns></returns>
        protected static DateTime? ConvertXmlDateValue(string xmlInnerText)
        {
            if (xmlInnerText.Length != 0)
                return Convert.ToDateTime(CleanEValueDateString(xmlInnerText));

            return null;
        }

        public void RemoveChildrenFromCallNode()
        {
            var callNodes = XRequestBase.Descendants().Where(x => x.Name == "call").First().Descendants().ToList();

            foreach (var node in callNodes)
            {
                node.Remove();
            }
        }

        public void AddChildToCallNode(string nodeName, object nodeValue, string attributeName, object attributeValue)
        {
            if (nodeValue != null)
            {
                if (nodeValue.GetType() == System.Type.GetType("System.DateTime"))
                {
                    nodeValue = ((DateTime)nodeValue).ToShortDateString();
                }
                ((XElement)XRequestBase.Descendants().Where(x => x.Name == "call").First()).Add(new XElement(nodeName, nodeValue, new XAttribute(attributeName, attributeValue)));
            }
        }
    }
}