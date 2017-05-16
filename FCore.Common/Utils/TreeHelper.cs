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
                default:
                    throw new InvalidOperationException("Invalid relationship type passed to function.");
            }
            return null;
        }

        public static string GetThirdLevelRelationship(RelativeModel creatorRelativeRel, RelationshipType createdCreatorRel)
        {
            switch (createdCreatorRel)
            {
                case RelationshipType.Mother:
                    return ThirdLevelRelData.GetMother(creatorRelativeRel);

                case RelationshipType.Father:
                    return ThirdLevelRelData.GetFather(creatorRelativeRel);
            }
            return null;
        }
    }
}
