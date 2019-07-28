using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EValueApi.Business;

namespace EValueApi.Communication.PersonalRecords
{
    public class UpdateResponse : BaseResponse
    {
        public PersonalRecord PersonalRecord { get; set; }

        public UpdateResponse(string xmlResponseStr) : base(xmlResponseStr)
        {
            try
            {
                if (base.Status)
                {
                    PersonalRecord = new PersonalRecord
                    {
                        UserId = GetValueForNodeInt("arg", "name", "userid"),
                        IcId = GetValueForNodeInt("arg", "name", "icid"),
                        EventDate = GetValueForNodeDate("arg", "name", "eventdate"),
                        ExpireDate = GetValueForNodeDate("arg", "name", "expiredate"),
                        RequirementId = GetValueForNodeInt("arg", "name", "requirementid"),
                        TypeId = GetValueForNodeInt("arg", "name", "typeid"),
                        Note = GetValueForNode("arg", "name", "note"),
                        StatusId = GetValueForNodeInt("arg", "name", "statusid"),
                        IsArchive = GetValueForNodeBool("arg", "name", "archive")
                    };
                }
            }
            catch (Exception ex)
            {
                base.ErrorMessage = ex.Message;
            }
        }
    }
}
