﻿using FCore.Common.Enums;
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
        public static string GetOppositeRelationship(RelationshipType relationship, GenderType relativeGender)
        {
            switch (relationship)
            {
                case RelationshipType.Daughter:
                    return RelData.GetOppositeForChildren(relativeGender);

                case RelationshipType.Son:
                    return RelData.GetOppositeForChildren(relativeGender);

                case RelationshipType.Father:
                    return RelData.GetOppositeForParents(relativeGender);

                case RelationshipType.Mother:
                    return RelData.GetOppositeForParents(relativeGender);

                case RelationshipType.Sister:
                    return RelData.GetOppositeForSiblings(relativeGender);

                case RelationshipType.Brother:
                    return RelData.GetOppositeForSiblings(relativeGender);

                case RelationshipType.Husband:
                    return RelData.GetOppositeForSpouses(relativeGender);

                case RelationshipType.Wife:
                    return RelData.GetOppositeForSpouses(relativeGender);

                case RelationshipType.Brother_in_law:
                    return RelData.GetOppositeForSiblingsInLaw(relativeGender);

                case RelationshipType.Sister_in_law:
                    return RelData.GetOppositeForSiblingsInLaw(relativeGender);

                case RelationshipType.Father_in_law:
                    return RelData.GetOppositeForParentsInLaw(relativeGender);

                case RelationshipType.Mother_in_law:
                    return RelData.GetOppositeForParentsInLaw(relativeGender);

                case RelationshipType.Son_in_law:
                    return RelData.GetOppositeForChildrenInLaw(relativeGender);

                case RelationshipType.Daughter_in_law:
                    return RelData.GetOppositeForChildrenInLaw(relativeGender);

                case RelationshipType.Aunt:
                    return RelationshipType.Nephew.ToString();

                case RelationshipType.Uncle:
                    return RelationshipType.Nephew.ToString();

                case RelationshipType.Nephew:
                    return RelData.GetOppositeForNephew(relativeGender);

                case RelationshipType.Cousin:
                    return relationship.ToString();

                case RelationshipType.Granddaughter:
                    return RelData.GetOppositeForGrandChildren(relativeGender);

                case RelationshipType.Grandson:
                    return RelData.GetOppositeForGrandChildren(relativeGender);

                case RelationshipType.Grandfather:
                    return RelData.GetOppositeForGrandParents(relativeGender);

                case RelationshipType.Grandmother:
                    return RelData.GetOppositeForGrandParents(relativeGender);

                case RelationshipType.Great_GrandChild:
                    return RelData.GetOppositeForGreatGrandChild(relativeGender);

                case RelationshipType.Great_GrandMother:
                    return RelationshipType.Great_GrandChild.ToString();

                case RelationshipType.Great_GrandFather:
                    return RelationshipType.Great_GrandChild.ToString();

                default:
                    throw new InvalidOperationException("Invalid relationship type passed to function.");
            }
        }

        public static string GetThirdLevelRelationship(RelativeModel relativeRelativeRel, RelationshipType createdRelativeRel,
                                                       GenderType createdGender, GenderType secondRelGender)
        {
            switch (createdRelativeRel)
            {
                case RelationshipType.Mother:
                    return RelData.GetThirdRelForParents((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Father:
                    return RelData.GetThirdRelForParents((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Brother:
                    return RelData.GetThirdRelForSiblings((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), createdGender);

                case RelationshipType.Sister:
                    return RelData.GetThirdRelForSiblings((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), createdGender);

                case RelationshipType.Aunt:
                    return RelData.GetThirdRelForAuntOrUncle((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship));

                case RelationshipType.Uncle:
                    return RelData.GetThirdRelForAuntOrUncle((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship));

                case RelationshipType.Son:
                    return RelData.GetThirdRelForChildren((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Daughter:
                    return RelData.GetThirdRelForChildren((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Husband:
                    return RelData.GetThirdRelForSpouses((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship));

                case RelationshipType.Wife:
                    return RelData.GetThirdRelForSpouses((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship));

                case RelationshipType.Grandfather:
                    return RelData.GetThirdRelForGrandParents((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Grandmother:
                    return RelData.GetThirdRelForGrandParents((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Grandson:
                    return RelData.GetThirdRelForGrandChildren((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), createdGender, secondRelGender);

                case RelationshipType.Granddaughter:
                    return RelData.GetThirdRelForGrandChildren((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), createdGender, secondRelGender);

                case RelationshipType.Brother_in_law:
                    return RelData.GetThirdRelForSiblingsInLaw((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                case RelationshipType.Sister_in_law:
                    return RelData.GetThirdRelForSiblingsInLaw((RelationshipType)Enum
                                  .Parse(typeof(RelationshipType), relativeRelativeRel
                                  .Relationship), secondRelGender);

                //case RelationshipType.Mother_in_law:
                //    return RelData.GetThirdRelForParentInLaw((RelationshipType)Enum
                //                  .Parse(typeof(RelationshipType), relativeRelativeRel
                //                  .Relationship));

                //case RelationshipType.Father_in_law:
                //    return RelData.GetThirdRelForParentInLaw((RelationshipType)Enum
                //                  .Parse(typeof(RelationshipType), relativeRelativeRel
                //                  .Relationship));

                //case RelationshipType.Son_in_law:
                //    return RelData.GetThirdRelForChildrenInLaw((RelationshipType)Enum
                //                  .Parse(typeof(RelationshipType), relativeRelativeRel
                //                  .Relationship));

                //case RelationshipType.Daughter_in_law:
                //    return RelData.GetThirdRelForChildrenInLaw((RelationshipType)Enum
                //                  .Parse(typeof(RelationshipType), relativeRelativeRel
                //                  .Relationship));

                default:
                    throw new InvalidOperationException("Invalid relationship type passed to function.");
            }
        }
    }
}
