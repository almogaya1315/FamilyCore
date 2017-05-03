using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    public static class ConstGenerator
    {
        public static ICollection<string> ChildRelTypes
        {
            get
            {
                return GetChildRelTypeList();
            }
            private set { }
        }

        private static ICollection<string> GetChildRelTypeList()
        {
            return ChildRelTypes = new List<string>()
            {
                "Daughter", "Son", "Granddaughter", "Grandson",
                "Sister", "Brother", "Uncle", "Aunt", "Cousin",
                "Great-GrandChild", "Sister in-law", "Brother in-law", "Nephew"
            };
        }
    }
}
