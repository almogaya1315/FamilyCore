using FCore.Common.Enums;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    public static class TreeHelper
    {
        public static string GetOppositeRelationship(RelationshipType relationship, GenderType creatorGender)
        {
            switch (relationship)
            {
                case RelationshipType.Daughter:
                    if (creatorGender == GenderType.Male) return RelationshipType.Father.ToString();
                    else if (creatorGender == GenderType.Female) return RelationshipType.Mother.ToString();
                    break;
                case RelationshipType.Son:
                    break;
            }
            throw new InvalidOperationException("Invalid relationship type passed to function.");
        }

        public static string GetThirdLevelRelationship(RelativeModel creatorRelative, RelativeModel createdCreatorRel)
        {
            return null;
        }
    }
}
