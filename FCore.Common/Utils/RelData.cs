﻿using FCore.Common.Enums;
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

        internal static string GetRelForParents(RelativeModel creatorRelativeRel)
        {
            if (creatorRelativeRel.Relationship == RelationshipType.Aunt.ToString()) return RelationshipType.Aunt.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Brother.ToString()) return RelationshipType.Uncle.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Brother_in_law.ToString()) return RelationshipType.Uncle.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Cousin.ToString()) return RelationshipType.Cousin.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Daughter.ToString()) return RelationshipType.Sister.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Father.ToString()) return RelationshipType.Grandfather.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Father_in_law.ToString()) return RelationshipType.Grandfather.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Grandfather.ToString()) return RelationshipType.Grandfather.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Grandmother.ToString()) return RelationshipType.Grandmother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Husband.ToString()) return RelationshipType.Father.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Mother.ToString()) return RelationshipType.Grandmother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Mother_in_law.ToString()) return RelationshipType.Grandmother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Nephew.ToString()) return RelationshipType.Cousin.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Sister.ToString()) return RelationshipType.Aunt.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Sister_in_law.ToString()) return RelationshipType.Aunt.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Son.ToString()) return RelationshipType.Brother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Uncle.ToString()) return RelationshipType.Uncle.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Wife.ToString()) return RelationshipType.Mother.ToString();

            // could also return 'Daughter'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Son'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Grandson'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return RelationshipType.Granddaughter.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }

        internal static string GetRelForAuntOrUncle(RelativeModel creatorRelativeRel) // copied from above func. not yet modified!
        {
            if (creatorRelativeRel.Relationship == RelationshipType.Aunt.ToString()) return RelationshipType.Undefined.ToString(); // modified
            if (creatorRelativeRel.Relationship == RelationshipType.Brother.ToString()) return RelationshipType.Uncle.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Brother_in_law.ToString()) return RelationshipType.Uncle.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Cousin.ToString()) return RelationshipType.Cousin.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Daughter.ToString()) return RelationshipType.Sister.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Father.ToString()) return RelationshipType.Grandfather.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Father_in_law.ToString()) return RelationshipType.Grandfather.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Grandfather.ToString()) return RelationshipType.Grandfather.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Grandmother.ToString()) return RelationshipType.Grandmother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Husband.ToString()) return RelationshipType.Father.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Mother.ToString()) return RelationshipType.Grandmother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Mother_in_law.ToString()) return RelationshipType.Grandmother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Nephew.ToString()) return RelationshipType.Cousin.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Sister.ToString()) return RelationshipType.Aunt.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Sister_in_law.ToString()) return RelationshipType.Aunt.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Son.ToString()) return RelationshipType.Brother.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Uncle.ToString()) return RelationshipType.Uncle.ToString();
            if (creatorRelativeRel.Relationship == RelationshipType.Wife.ToString()) return RelationshipType.Mother.ToString();

            // could also return 'Daughter'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Son'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return RelationshipType.Nephew.ToString();
            // could also return 'Grandson'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return RelationshipType.Granddaughter.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }
    }
}