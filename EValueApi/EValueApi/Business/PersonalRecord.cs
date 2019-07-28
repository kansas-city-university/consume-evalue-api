using System;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;

namespace EValueApi.Business
{
    public class PersonalRecord : IEquatable<PersonalRecord>
    {
        public DateTime? ExpireDate { get; set; }
        public int? IcId { get; set; }
        public DateTime? EventDate { get; set; }
        public int? RequirementId { get; set; }
        public int? TypeId { get; set; }
        public string Note { get; set; }
        public int? StatusId { get; set; }
        public int? UserId { get; set; }
        public bool? IsArchive { get; set; }

        public PersonalRecord()
        {

        }

        public static bool operator ==(PersonalRecord obj1, PersonalRecord obj2)
        {
            if (object.ReferenceEquals(null, obj1))
            {
                return object.ReferenceEquals(null, obj2);
            }

            if (object.ReferenceEquals(null, obj2))
            {
                return object.ReferenceEquals(null, obj1);
            }

            if (!obj1.UserId.HasValue || 
                !obj2.UserId.HasValue || 
                //!obj1.StatusId.HasValue || 
                //!obj2.StatusId.HasValue ||
                !obj1.TypeId.HasValue ||
                !obj2.TypeId.HasValue// ||
                //!obj1.RequirementId.HasValue ||
                //!obj2.RequirementId.HasValue ||
                //!obj1.EventDate.HasValue ||
                //!obj2.EventDate.HasValue
                )
            {
                return false;
            }

            return
                obj1.UserId.Value == obj2.UserId.Value &&
                obj1.TypeId.Value == obj2.TypeId.Value;
        }

        public static bool operator !=(PersonalRecord obj1, PersonalRecord obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(Object obj)
        {
            return this == (PersonalRecord)obj;
        }

        public bool Equals(PersonalRecord obj)
        {
            return this == obj;
        }

        public override int GetHashCode()
        {
            return IcId.Value.GetHashCode();
        }
    }
}
