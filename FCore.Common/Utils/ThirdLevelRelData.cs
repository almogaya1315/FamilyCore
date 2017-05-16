using FCore.Common.Enums;
using FCore.Common.Models.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    internal static class ThirdLevelRelData
    {
        public static string GetMother(RelativeModel creatorRelativeRel)
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

            // could also be 'Daughter'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return RelationshipType.Nephew.ToString();
            // could also be 'Son'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return RelationshipType.Nephew.ToString();
            // could also be 'Grandson'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return RelationshipType.Granddaughter.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }

        public static string GetFather(RelativeModel creatorRelativeRel) // copied from above func. yet to be modified!
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

            // could also be 'Daughter'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Granddaughter.ToString()) return RelationshipType.Nephew.ToString();
            // could also be 'Son'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Grandson.ToString()) return RelationshipType.Nephew.ToString();
            // could also be 'Grandson'. needs to response to ui for user choice.
            if (creatorRelativeRel.Relationship == RelationshipType.Great_GrandChild.ToString()) return RelationshipType.Granddaughter.ToString();

            else throw new InvalidOperationException("Invalid relationship type passed to function.");
        }
    }
}
