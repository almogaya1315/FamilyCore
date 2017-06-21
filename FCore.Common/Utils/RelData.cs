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

        internal static string GetOppositeForParentsInLaw(GenderType inLawGender)
        {
            if (inLawGender == GenderType.Male) return RelationshipType.Son_in_law.ToString();
            else if (inLawGender == GenderType.Female) return RelationshipType.Daughter_in_law.ToString();
            else throw new InvalidOperationException("Invalid gender type passed to function.");
        }

        internal static string GetOppositeForChildrenInLaw(GenderType inLawGender)
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
        internal static string GetThirdRelForParents(RelationshipType relativeRelativeRel, GenderType secondRelGender)
        {
            switch (relativeRelativeRel)
            {
                // if my parent has a brother, he is my uncle
                case RelationshipType.Brother: return RelationshipType.Uncle.ToString();
                case RelationshipType.Brother_in_law: return RelationshipType.Uncle.ToString();
                case RelationshipType.Daughter: return RelationshipType.Sister.ToString();
                case RelationshipType.Father: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Father_in_law: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Grandfather: return RelationshipType.Great_GrandFather.ToString();
                case RelationshipType.Grandmother: return RelationshipType.Great_GrandMother.ToString();
                case RelationshipType.Husband: return RelationshipType.Father.ToString();
                case RelationshipType.Mother: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Mother_in_law: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Nephew: return RelationshipType.Cousin.ToString();
                case RelationshipType.Sister: return RelationshipType.Aunt.ToString();
                case RelationshipType.Sister_in_law: return RelationshipType.Aunt.ToString();
                case RelationshipType.Son: return RelationshipType.Brother.ToString();
                case RelationshipType.Wife: return RelationshipType.Mother.ToString();
                case RelationshipType.Son_in_law: return RelationshipType.Brother_in_law.ToString();
                case RelationshipType.Daughter_in_law: return RelationshipType.Sister_in_law.ToString();

                // could also return 'Daughter'. needs to response to ui for user choice.
                case RelationshipType.Granddaughter: return RelationshipType.Nephew.ToString();
                // could also return 'Son'. needs to response to ui for user choice.
                case RelationshipType.Grandson: return RelationshipType.Nephew.ToString();

                case RelationshipType.Great_GrandChild: return GetOppositeForGrandParents(secondRelGender);
                default: return relativeRelativeRel.ToString(); // for 'Great_GrandMother', 'Great_GrandFather', 'Cousin', 'Uncle', 'Aunt', 'Undefined'
            }
        }

        internal static string GetThirdRelForSiblings(RelationshipType relativeRelativeRel, GenderType createdGender)
        {
            switch (relativeRelativeRel)
            {
                // if my sibling has a husband, he is my brother in-law
                case RelationshipType.Husband: return RelationshipType.Brother_in_law.ToString();
                case RelationshipType.Wife: return RelationshipType.Sister_in_law.ToString();

                case RelationshipType.Son: return GetOppositeForNephew(createdGender);
                case RelationshipType.Daughter: return GetOppositeForNephew(createdGender);
                case RelationshipType.Son_in_law: return GetOppositeForNephew(createdGender);
                case RelationshipType.Daughter_in_law: return GetOppositeForNephew(createdGender);

                default: return relativeRelativeRel.ToString(); // for all but the assosiated 6 cases
            }
        }

        internal static string GetThirdRelForAuntOrUncle(RelationshipType relativeRelativeRel)
        {
            switch (relativeRelativeRel)
            {
                // if my uncle or aunt has a wife, she is my aunt
                case RelationshipType.Wife: return RelationshipType.Aunt.ToString();
                case RelationshipType.Husband: return RelationshipType.Uncle.ToString();
                case RelationshipType.Mother: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Father: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Daughter: return RelationshipType.Cousin.ToString();
                case RelationshipType.Son: return RelationshipType.Cousin.ToString();
                case RelationshipType.Grandmother: return RelationshipType.Great_GrandMother.ToString();
                case RelationshipType.Grandfather: return RelationshipType.Great_GrandFather.ToString();
                case RelationshipType.Granddaughter: return RelationshipType.Cousin.ToString();
                case RelationshipType.Grandson: return RelationshipType.Cousin.ToString();
                case RelationshipType.Uncle: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Aunt: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Great_GrandChild: return RelationshipType.Cousin.ToString();
                case RelationshipType.Mother_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Father_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Sister_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Brother_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Son_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Daughter_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Nephew: return RelationshipType.Cousin.ToString();

                // could also return 'Aunt'. needs to response to ui for user choice. 
                case RelationshipType.Sister: return RelationshipType.Mother.ToString();
                // could also return 'Uncle'. needs to response to ui for user choice. 
                case RelationshipType.Brother: return RelationshipType.Father.ToString();

                default: return relativeRelativeRel.ToString(); // for 'Cousin', 'Great_GrandFather', 'Great_GrandMother', 'Undefined'
            }
        }

        internal static string GetThirdRelForChildren(RelationshipType relativeRelativeRel, GenderType secondRelGender)
        {
            switch (relativeRelativeRel)
            {
                // if my son or daugher have a wife, she is my daugher in-law
                case RelationshipType.Wife: return RelationshipType.Daughter_in_law.ToString();
                case RelationshipType.Husband: return RelationshipType.Son_in_law.ToString();
                case RelationshipType.Daughter: return RelationshipType.Granddaughter.ToString();
                case RelationshipType.Son: return RelationshipType.Grandson.ToString();
                case RelationshipType.Granddaughter: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Grandson: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Cousin: return RelationshipType.Nephew.ToString();
                case RelationshipType.Great_GrandFather: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Great_GrandMother: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Mother_in_law: return RelationshipType.In_law.ToString();
                case RelationshipType.Father_in_law: return RelationshipType.In_law.ToString();
                case RelationshipType.Sister_in_law: return RelationshipType.Daughter_in_law.ToString();
                case RelationshipType.Brother_in_law: return RelationshipType.Son_in_law.ToString();
                case RelationshipType.Son_in_law: return RelationshipType.Grandson.ToString();
                case RelationshipType.Daughter_in_law: return RelationshipType.Granddaughter.ToString();

                case RelationshipType.Nephew: return GetOppositeForGrandParents(secondRelGender);

                // could also be 'Divorcee'. needs to response to ui for user choice. 
                case RelationshipType.Mother: return RelationshipType.Wife.ToString();
                case RelationshipType.Father: return RelationshipType.Husband.ToString();

                // could also be 'Mother in-law'. needs to response to ui for user choice. 
                case RelationshipType.Grandmother: return RelationshipType.Mother.ToString();
                // could also be 'Father in-law'. needs to response to ui for user choice. 
                case RelationshipType.Grandfather: return RelationshipType.Father_in_law.ToString();

                // could also be 'Daugher in-law'. needs to response to ui for user choice.
                case RelationshipType.Sister: return RelationshipType.Daughter.ToString();
                // could also be 'Son in-law'. needs to response to ui for user choice.
                case RelationshipType.Brother: return RelationshipType.Son.ToString();

                // could also be 'Brother in-law'. needs to response to ui for user choice.
                case RelationshipType.Uncle: return RelationshipType.Brother.ToString();
                // could also be 'Sister in-law'. needs to response to ui for user choice.
                case RelationshipType.Aunt: return RelationshipType.Sister.ToString();

                default: return relativeRelativeRel.ToString(); // for 'Great_GrandChild', 'Undefined'
            }
        }

        internal static string GetThirdRelForSpouses(RelationshipType relativeRelativeRel)
        {
            switch (relativeRelativeRel)
            {
                // if my husband or wife have a wife, she is undefined
                case RelationshipType.Wife: return RelationshipType.Undefined.ToString();
                case RelationshipType.Husband: return RelationshipType.Undefined.ToString();
                case RelationshipType.Mother: return RelationshipType.Mother_in_law.ToString();
                case RelationshipType.Father: return RelationshipType.Father_in_law.ToString();
                case RelationshipType.Sister: return RelationshipType.Sister_in_law.ToString();
                case RelationshipType.Brother: return RelationshipType.Brother_in_law.ToString();
                case RelationshipType.Uncle: return RelationshipType.In_law.ToString();
                case RelationshipType.Aunt: return RelationshipType.In_law.ToString();
                case RelationshipType.Cousin: return RelationshipType.In_law.ToString();
                case RelationshipType.Mother_in_law: return RelationshipType.Mother.ToString();
                case RelationshipType.Father_in_law: return RelationshipType.Father.ToString();
                case RelationshipType.Sister_in_law: return RelationshipType.Sister.ToString();
                case RelationshipType.Brother_in_law: return RelationshipType.Brother.ToString();
                case RelationshipType.Son_in_law: return RelationshipType.Son.ToString();
                case RelationshipType.Daughter_in_law: return RelationshipType.Daughter.ToString();
                case RelationshipType.Divorcee: return RelationshipType.Undefined.ToString();

                // could also be 'Daughter in-law'. needs to response to ui for user choice. 
                case RelationshipType.Daughter: return relativeRelativeRel.ToString();
                // could also be 'Son in-law'. needs to response to ui for user choice. 
                case RelationshipType.Son: return relativeRelativeRel.ToString();
                // could also be 'Aunt', 'Cousin', 'Mother_in_law', 'Father_in_law'. needs to response to ui for user choice. 
                case RelationshipType.In_law: return RelationshipType.Uncle.ToString();
                // for 'Grandmother', 'Grandfather', 'Granddaughter', 'Grandson', 'Great_GrandChild', 'Great_GrandFather', 'Great_GrandMother', 'Nephew', 'Undefined' 
                default: return relativeRelativeRel.ToString();
            }
        }

        internal static string GetThirdRelForGrandParents(RelationshipType relativeRelativeRel, GenderType secondRelGender)
        {
            switch (relativeRelativeRel)
            {
                // if my grandmother or grandfather have a wife, she is Grandmother 
                case RelationshipType.Wife: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Husband: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Mother: return RelationshipType.Great_GrandMother.ToString();
                case RelationshipType.Father: return RelationshipType.Great_GrandFather.ToString();
                case RelationshipType.Grandmother: return RelationshipType.Great_GrandMother.ToString();
                case RelationshipType.Grandfather: return RelationshipType.Great_GrandFather.ToString();
                case RelationshipType.Sister: return RelationshipType.Grandmother.ToString();
                case RelationshipType.Brother: return RelationshipType.Grandfather.ToString();
                case RelationshipType.Uncle: return RelationshipType.Undefined.ToString();
                case RelationshipType.Aunt: return RelationshipType.Undefined.ToString();
                case RelationshipType.Great_GrandFather: return RelationshipType.Undefined.ToString();
                case RelationshipType.Great_GrandMother: return RelationshipType.Undefined.ToString();
                case RelationshipType.Mother_in_law: return RelationshipType.Great_GrandMother.ToString();
                case RelationshipType.Father_in_law: return RelationshipType.Great_GrandFather.ToString();
                case RelationshipType.Sister_in_law: return RelationshipType.Great_GrandMother.ToString();
                case RelationshipType.Brother_in_law: return RelationshipType.Great_GrandFather.ToString();
                case RelationshipType.In_law: return RelationshipType.Undefined.ToString();

                case RelationshipType.Cousin: return GetOppositeForGrandParents(secondRelGender);
                case RelationshipType.Nephew: return GetOppositeForNephew(secondRelGender);

                // could also be 'Aunt'. needs to respons to ui for user choice. 
                case RelationshipType.Daughter: return RelationshipType.Mother.ToString();
                // could also be 'Uncle'. needs to respond to ui for user choice.
                case RelationshipType.Son: return RelationshipType.Father.ToString();
                // could also be 'Cousin'. needs to respond to ui for user choice.
                case RelationshipType.Granddaughter: return RelationshipType.Sister.ToString();
                case RelationshipType.Grandson: return RelationshipType.Brother.ToString();
                // could also be 'Daughter', 'Nephew', 'Cousin'. needs to respond to ui for user choice.
                case RelationshipType.Great_GrandChild: return RelationshipType.Son.ToString();
                // could also be 'Uncle'. needs to respond to ui for user choice.
                case RelationshipType.Son_in_law: return RelationshipType.Father.ToString();
                // could also be 'Aunt'. needs to respond to ui for user choice.
                case RelationshipType.Daughter_in_law: return RelationshipType.Mother.ToString();
                // could also be 'Undefined'. needs to respond to ui for user choice.
                case RelationshipType.Divorcee: return RelationshipType.Grandfather.ToString();
                default: return relativeRelativeRel.ToString(); // for 'Undefined'
            }
        }

        internal static string GetThirdRelForGrandChildren(RelationshipType relativeRelativeRel, GenderType createdGender, GenderType secondRelGender)
        {
            switch (relativeRelativeRel)
            {
                case RelationshipType.Daughter: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Son: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Granddaughter: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Grandson: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Sister: return RelationshipType.Granddaughter.ToString();
                case RelationshipType.Brother: return RelationshipType.Grandson.ToString();
                case RelationshipType.Mother_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Father_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Sister_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Brother_in_law: return RelationshipType.Undefined.ToString();
                case RelationshipType.Nephew: return RelationshipType.Great_GrandChild.ToString();
                case RelationshipType.Divorcee: return RelationshipType.Undefined.ToString();
                case RelationshipType.In_law: return RelationshipType.Undefined.ToString();

                case RelationshipType.Grandmother: return GetOppositeForSpouses(createdGender);
                case RelationshipType.Grandfather: return GetOppositeForSpouses(createdGender);
                case RelationshipType.Cousin: return GetOppositeForGrandParents(secondRelGender);
                case RelationshipType.Son_in_law: return GetOppositeForGrandParents(secondRelGender);
                case RelationshipType.Daughter_in_law: return GetOppositeForGrandParents(secondRelGender);

                // could also be 'Granddaughter'. needs to respond to ui for user choice.
                case RelationshipType.Wife: return RelationshipType.In_law.ToString();
                // could also be 'Grandson'. needs to respond to ui for user choice.
                case RelationshipType.Husband: return RelationshipType.In_law.ToString();
                // could also be 'Daughter in-law'. needs to respond to ui for user choice.
                case RelationshipType.Mother: return RelationshipType.Daughter.ToString();
                case RelationshipType.Aunt: return RelationshipType.Daughter.ToString();
                // could also be 'Son in-law'. needs to respond to ui for user choice.
                case RelationshipType.Father: return RelationshipType.Son.ToString();
                case RelationshipType.Uncle: return RelationshipType.Son.ToString();
                // could also be 'Father in-law'. needs to respond to ui for user choice.
                case RelationshipType.Great_GrandFather: return RelationshipType.Father.ToString();
                // could also be 'Mother in-law'. needs to respond to ui for user choice.
                case RelationshipType.Great_GrandMother: return RelationshipType.Mother.ToString();
                // for 'Great_GrandChild', 'Undefined'
                default: return relativeRelativeRel.ToString(); 
            }
        }

        internal static string GetThirdRelForSiblingsInLaw(RelationshipType relativeRelativeRel)
        {
            switch (relativeRelativeRel)
            {
                // if my brother in-law or sister in-law has a wife, she is my sister in-law
                case RelationshipType.Wife: return RelationshipType.Sister_in_law.ToString();
                case RelationshipType.Husband: return RelationshipType.Brother_in_law.ToString();
                case RelationshipType.Mother: return RelationshipType.Mother_in_law.ToString();
                case RelationshipType.Father: return RelationshipType.Father_in_law.ToString();
                case RelationshipType.Daughter:
                    break;
                case RelationshipType.Son:
                    break;
                case RelationshipType.Grandmother:
                    break;
                case RelationshipType.Grandfather:
                    break;
                case RelationshipType.Granddaughter:
                    break;
                case RelationshipType.Grandson:
                    break;
                case RelationshipType.Sister:
                    break;
                case RelationshipType.Brother:
                    break;
                case RelationshipType.Uncle:
                    break;
                case RelationshipType.Aunt:
                    break;
                case RelationshipType.Cousin:
                    break;
                case RelationshipType.Great_GrandChild:
                    break;
                case RelationshipType.Great_GrandFather:
                    break;
                case RelationshipType.Great_GrandMother:
                    break;
                case RelationshipType.Mother_in_law:
                    break;
                case RelationshipType.Father_in_law:
                    break;
                case RelationshipType.Sister_in_law:
                    break;
                case RelationshipType.Brother_in_law:
                    break;
                case RelationshipType.Son_in_law:
                    break;
                case RelationshipType.Daughter_in_law:
                    break;
                case RelationshipType.Nephew:
                    break;
                case RelationshipType.Divorcee:
                    break;
                case RelationshipType.In_law:
                    break;
                case RelationshipType.Undefined:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
