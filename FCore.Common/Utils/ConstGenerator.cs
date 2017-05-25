using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    public static class ConstGenerator
    {
        public static string UserContextConnectionString
        {
            get
            {
                return "name=UserContext";
            }
        }

        public static ICollection<string> ChildRelTypes
        {
            get
            {
                return GetChildRelTypeList();
            }
            private set { }
        }

        public static ICollection<string> Cities
        {
            get
            {
                return GetCities();
            }
            private set { }
        }

        private static ICollection<string> GetChildRelTypeList()
        {
            return ChildRelTypes = new List<string>()
            {
                "Daughter", "Son", "Grand-Daughter", "Grand-Son",
                "Sister", "Brother", "Uncle", "Aunt", "Cousin",
                "Great Grand-Child", "Sister in-law", "Brother in-law", "Nephew"
            };
        }

        private static ICollection<string> GetCities()
        {
            return Cities = new List<string>()
            {
                "Petah-Tikva", "Tel-Aviv", "Eilat", "Ramat-Gan", "Bat-Yam", "Afula", "Haifa", "Jerusalem", "Kfar-Saba"
            };
        }
    }
}
