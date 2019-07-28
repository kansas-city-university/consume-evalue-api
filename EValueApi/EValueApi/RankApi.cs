using System.Collections.Generic;
using System.Xml;
using EValueApi.Business;
using EValueApi.Communication;

namespace EValueApi
{
    public class RankApi : EvalueApi
    {

        private readonly string _url;

        public RankApi(string clientId, string password, string subUnitId, string url)
            : base(clientId, password, subUnitId)
        {
            _url = url;
        }

        /// <summary>
        /// Gets all of the personal records for a given user.
        /// </summary>
        /// <returns></returns>
        public RanksResponse GetAll()
        {

            // Add the proper XML to the Call node
            XmlDocument newRequest = new XmlDocument();
            newRequest.LoadXml(RequestBase.InnerXml);

            var eValueApiService = new EValueRankApi.Rank_1_0Service() { Url = _url };

            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(eValueApiService.getAll(newRequest.InnerXml));

            return ExtractResponseFromXml(responseXml);

        }

        /// <summary>
        /// Read the response XML and create a response in object format.
        /// </summary>
        /// <param name="responseXml"></param>
        /// <returns></returns>
        public static RanksResponse ExtractResponseFromXml(XmlDocument responseXml)
        {

            List<Rank> resultValue;

            var responseValue = (responseXml.GetElementsByTagName("resp")[0].Attributes?["status"].Value == "1");

            if (responseValue)
            {

                resultValue = new List<Rank>();

                foreach (XmlElement elementXml in responseXml.GetElementsByTagName("r"))
                {

                    // Put it into an xml Document to load
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(elementXml.OuterXml);

                    resultValue.Add(new Rank()
                    {
                        RankId = int.Parse(doc.SelectNodes("//d[@NAME='rankid']")?[0].InnerText),
                        Label = doc.SelectNodes("//d[@NAME='rankLabel']")?[0].InnerText
                    });

                }

            }
            else
            {
                resultValue = null;
            }

            // Now return your results
            return new RanksResponse()
            {
                Ranks = resultValue,
                Status = responseValue
            };

        }
    }
}
