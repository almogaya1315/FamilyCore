using FCore.Common.Enums;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    internal static class RelData
    {
        #region opposite relationship
        internal static string GetOppositeForChildren(GenderType creatorGender)
        {
            if (creatorGender == GenderType.Male) return RelationshipType.Father.ToString();
            else if (creatorGender == GenderType.Female) return RelationshipType.Mother.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }
        internal static string GetOppositeForParents(GenderType childGender)
        {
            if (childGender == GenderType.Male) return RelationshipType.Son.ToString();
            else if (childGender == GenderType.Female) return RelationshipType.Daughter.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForSiblings(GenderType siblingGender)
        {
            if (siblingGender == GenderType.Male) return RelationshipType.Brother.ToString();
            else if (siblingGender == GenderType.Female) return RelationshipType.Sister.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForSpouses(GenderType spouseGender)
        {
            if (spouseGender == GenderType.Male) return RelationshipType.Husband.ToString();
            else if (spouseGender == GenderType.Female) return RelationshipType.Wife.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForSiblingsInLaw(GenderType inLawGender)
        {
            if (inLawGender == GenderType.Male) return RelationshipType.Brother_in_law.ToString();
            else if (inLawGender == GenderType.Female) return RelationshipType.Sister_in_law.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForParentsInLaws(GenderType inLawGender)
        {
            if (inLawGender == GenderType.Male) return RelationshipType.Son_in_law.ToString();
            else if (inLawGender == GenderType.Female) return RelationshipType.Daughter_in_law.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForChildrenInLaws(GenderType inLawGender)
        {
            if (inLawGender == GenderType.Male) return RelationshipType.Father_in_law.ToString();
            else if (inLawGender == GenderType.Female) return RelationshipType.Mother_in_law.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForNephew(GenderType relGender)
        {
            if (relGender == GenderType.Male) return RelationshipType.Uncle.ToString();
            else if (relGender == GenderType.Female) return RelationshipType.Aunt.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForGrandChildren(GenderType relGender)
        {
            if (relGender == GenderType.Male) return RelationshipType.Grandfather.ToString();
            else if (relGender == GenderType.Female) return RelationshipType.Grandmother.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForGrandParents(GenderType relGender)
        {
            if (relGender == GenderType.Male) return RelationshipType.Grandson.ToString();
            else if (relGender == GenderType.Female) return RelationshipType.Granddaughter.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForGreatGrandChild(GenderType relGender)
        {
            if (relGender == GenderType.Male) return RelationshipType.Great_GrandFather.ToString();
            else if (relGender == GenderType.Female) return RelationshipType.Great_GrandMother.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }
        #endregion

        #region third level relationship
        internal static string GetRelForParents(RelativeModel relativeRelativeRel)
        {
            
            if (relativeRelativeRel.Relationship == RelationshipType.Brother.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Brother_in_law.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Daughter.ToString()) return RelationshipType.Sister.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Father.ToString()) return RelationshipType.Grandfather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Father_in_law.ToString()) return RelationshipType.Grandfather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandfather.ToString()) return RelationshipType.Great_GrandFather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandmother.ToString()) return RelationshipType.Great_GrandMother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Husband.ToString()) return RelationshipType.Father.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Mother.ToString()) return RelationshipType.Grandmother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Mother_in_law.ToString()) return RelationshipType.Grandmother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Nephew.ToString()) return RelationshipType.Cousin.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Sister.ToString()) return RelationshipType.Aunt.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Sister_in_law.ToString()) return RelationshipType.Aunt.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Son.ToString()) return RelationshipType.Brother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Wife.ToString()) return RelationshipType.Mother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Son_in_law.ToString()) return RelationshipType.Brother_in_law.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Daughter_in_law.ToString()) return RelationshipType.Sister_in_law.ToString();

            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandMother.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandFather.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Cousin.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Uncle.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Aunt.ToString()) return relativeRelativeRel.Relationship.ToString();

            // could also return 'Daughter'. needs to response to ui for user choice.
            if (relativeRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Son'. needs to response to ui for user choice.
            if (relativeRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Grandson'. needs to response to ui for user choice.
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return RelationshipType.Granddaughter.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }

        internal static string GetRelForSiblings(RelativeModel relativeRelativeRel)
        {
            if (relativeRelativeRel.Relationship == RelationshipType.Husband.ToString()) return RelationshipType.Brother_in_law.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Wife.ToString()) return RelationshipType.Sister_in_law.ToString();

            if (relativeRelativeRel.Relationship == RelationshipType.Father.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Mother.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Brother.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Sister.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandfather.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandmother.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Aunt.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Uncle.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Cousin.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Nephew.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Brother_in_law.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Sister_in_law.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Father_in_law.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Mother_in_law.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandFather.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandMother.ToString()) return relativeRelativeRel.Relationship.ToString();

            // could also return 'Aunt'. needs to response to ui for user choice. Depends on the created's gender (for all 4 cases)
            if (relativeRelativeRel.Relationship == RelationshipType.Son.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Daughter.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Son_in_law.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Daughter_in_law.ToString()) return RelationshipType.Uncle.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }

        internal static string GetRelForAuntOrUncle(RelativeModel relativeRelativeRel) // copied from above func. not yet modified!
        {
            
            if (relativeRelativeRel.Relationship == RelationshipType.Brother.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Brother_in_law.ToString()) return RelationshipType.Uncle.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Daughter.ToString()) return RelationshipType.Sister.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Father.ToString()) return RelationshipType.Grandfather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Father_in_law.ToString()) return RelationshipType.Grandfather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandfather.ToString()) return RelationshipType.Great_GrandFather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Grandmother.ToString()) return RelationshipType.Great_GrandFather.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Husband.ToString()) return RelationshipType.Father.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Mother.ToString()) return RelationshipType.Grandmother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Mother_in_law.ToString()) return RelationshipType.Grandmother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Nephew.ToString()) return RelationshipType.Cousin.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Sister.ToString()) return RelationshipType.Aunt.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Sister_in_law.ToString()) return RelationshipType.Aunt.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Son.ToString()) return RelationshipType.Brother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Wife.ToString()) return RelationshipType.Mother.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Son_in_law.ToString()) return RelationshipType.Cousin.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Daughter_in_law.ToString()) return RelationshipType.Cousin.ToString();

            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandMother.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandFather.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Cousin.ToString()) return relativeRelativeRel.Relationship.ToString();
            if (relativeRelativeRel.Relationship == RelationshipType.Uncle.ToString()) return relativeRelativeRel.Relationship.ToString();

            // could also return 'Daughter'. needs to response to ui for user choice.
            if (relativeRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Son'. needs to response to ui for user choice.
            if (relativeRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Grandson'. needs to response to ui for user choice.
            if (relativeRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return RelationshipType.Granddaughter.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }
        #endregion
    }
}
