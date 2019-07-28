using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EValueApi;
using System.Configuration;
using EValueApi.Business;

namespace WebApiTest
{
    public class PersonalRecordsTest
    {
        public PersonalRecordsTest(string clientId, string password, string subUnitId)
        {
            Console.WriteLine("Beginning personal records test");

            var api = new PersonalRecordApi(clientId, password, subUnitId, "https://test-api.e-value.net/IandC_1_0.cfc");

            var result = api.GetUserPersonalRecords("1287294");
            

            //api.Create(new PersonalRecord
            //{

            //});

            //api.Update(new PersonalRecord { UserId = 999 });
            
            Thread.Sleep(10000);
        }
    }
}
