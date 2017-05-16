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
                    return RelData.GetOppositeForChildren(creatorGender);

                case RelationshipType.Son:
                    return RelData.GetOppositeForChildren(creatorGender);

                default:
                    throw new InvalidOperationException("Invalid relationship type passed to function.");
            }
        }

        public static string GetThirdLevelRelationship(RelativeModel creatorRelativeRel, RelationshipType createdCreatorRel)
        {
            switch (createdCreatorRel)
            {
                case RelationshipType.Mother:
                    return RelData.GetThirdLevelForParents(creatorRelativeRel);

                case RelationshipType.Father:
                    return RelData.GetThirdLevelForParents(creatorRelativeRel);

                case RelationshipType.Aunt:
                    return RelData.GetThirdLevelForAuntOrUncle(creatorRelativeRel);

                case RelationshipType.Uncle:
                    return RelData.GetThirdLevelForAuntOrUncle(creatorRelativeRel);

                default:
                    throw new InvalidOperationException("Invalid relationship type passed to function.");
            }
        }
    }
}
