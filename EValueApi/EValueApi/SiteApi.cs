using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class SiteApi : EvalueApi
    {

        private readonly string _url;

        public SiteApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Get a user with the EValue User Id
        /// </summary>
        /// <returns></returns>
        public SitesResponse GetAllSites()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValueSiteApi.Site_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getAllLinked(newRequest.InnerXml));

            var result = ExtractResponseFromXml(responseXml);

            return result;

        }


        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static SitesResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<Site> resultSite;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultSite = new List<Site>();

                foreach (XmlElement siteXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(siteXml.OuterXml);

                    resultSite.Add(new Site()
                    {
                        SiteId = int.Parse(doc.SelectNodes("//d[@NAME='siteid']")?[0].InnerText),
                        SiteName = doc.SelectNodes("//d[@NAME='sitename']")?[0].InnerText
                    });

                }

            }
            else
            {
                resultSite = null;
            }

            // Now return your results
            return new SitesResponse()
            {
                Sites = resultSite,
                Status = responseValue
            };

        }

    }
}
