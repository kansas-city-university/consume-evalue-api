using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace EValueApi.Communication
{
    /// <summary>
    /// This class contains helpers and properties that pertain to all api responses
    /// </summary>
    public class BaseResponse
    {
        public string RequestXml { get; set; } = string.Empty;

        protected XDocument Response { get; set; }
        protected XElement ResponseNode { get; set; } //some response nodes contain children, so return the entire node
        public bool Status { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        public BaseResponse(string xmlResponseStr)
        {
            Response = XDocument.Parse(xmlResponseStr);
            ResponseNode = Response.Descendants().Where(x => x.Name == "resp").First();

            var statusValue = ResponseNode.Attributes().Where(a => a.Name == "status").First().Value;

            bool b = false;
            int i = 0;
            if (bool.TryParse(statusValue, out b))
            {
                Status = b;
            }
            else if (int.TryParse(statusValue, out i))
            {
                Status = Convert.ToBoolean(i);
            }
            else
            {
                Status = false;
            }

            var errorNode = ResponseNode.Descendants().Where(x => x.Name == "error").FirstOrDefault();
            if (errorNode != null)
            {
                var descNode = errorNode.Descendants().Where(x => x.Name == "desc").FirstOrDefault();
                if (descNode != null)
                {
                    ErrorMessage = descNode.Value;
                }
            }
        }

        protected string GetValueForNode(string elementNameMatch, string attributeNameMatch, string attributeValueMatch)
        {
            try
            {
                return
                    Response
                    .Descendants()
                    .Where(x => x.Name.ToString().ToLower() == elementNameMatch.ToLower() && x.Attributes().Any(a => a.Name.ToString().ToLower() == attributeNameMatch.ToLower() && a.Value.ToLower() == attributeValueMatch.ToLower()))
                    .First()
                    .Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        protected int? GetValueForNodeInt(string elementNameMatch, string attributeNameMatch, string attributeValueMatch)
        {
            int r;

            if (int.TryParse(GetValueForNode(elementNameMatch, attributeNameMatch, attributeValueMatch), out r))
            {
                return r;
            }

            return null;
        }

        protected bool? GetValueForNodeBool(string elementNameMatch, string attributeNameMatch, string attributeValueMatch)
        {
            bool r;

            if (bool.TryParse(GetValueForNode(elementNameMatch, attributeNameMatch, attributeValueMatch), out r))
            {
                return r;
            }

            return null;
        }

        protected DateTime? GetValueForNodeDate(string elementNameMatch, string attributeNameMatch, string attributeValueMatch)
        {
            DateTime r;

            var val = GetValueForNode(elementNameMatch, attributeNameMatch, attributeValueMatch);
            val =val.Replace("{ts '", "").Replace("'}", "");

            if (DateTime.TryParse(val, out r))
            {
                return r;
            }

            return null;
        }
    }
}
