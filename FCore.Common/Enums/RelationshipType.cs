using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Enums
{
    [Flags]
    public enum RelationshipType
    {
        Wife = 1,
        Husband = 2,
        Mother = 3,
        Father = 4,
        Daughter = 5,
        Son = 6,
        Grandmother = 7,
        Grandfather = 8,
        Granddaughter = 9,
        Grandson = 10,
        Sister = 11,
        Brother = 12,
        Uncle = 13,
        Aunt = 14,
        Cousin = 15,
        Great_GrandChild,
        Mother_in_law,
        Father_in_law,
        Brother_in_law,
        גיס,
        אחיין
    }
}
