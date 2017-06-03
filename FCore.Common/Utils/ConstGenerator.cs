using FCore.Common.Enums;
using FCore.Common.Models.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        public static ICollection<SelectListItem> ChildRelTypes
        {
            get
            {
                ICollection<SelectListItem> relListItems = new List<SelectListItem>();
                foreach (string rel in GetChildRelTypeList())
                    relListItems.Add(new SelectListItem() { Text = rel });
                return relListItems;
            }
            private set { }
        }

        public static ICollection<SelectListItem> GenderTypes
        {
            get
            {
                ICollection<SelectListItem> genListItems = new List<SelectListItem>();
                foreach (string gen in Enum.GetNames(typeof(GenderType)).ToList())
                    genListItems.Add(new SelectListItem() { Text = gen });
                return genListItems;
            }
            private set { }
        }

        public static ICollection<SelectListItem> Cities
        {
            get
            {
                ICollection<SelectListItem> cities = new List<SelectListItem>();
                foreach (string city in GetCities())
                    cities.Add(new SelectListItem() { Text = city });
                return cities;
            }
            private set { }
        }

        public static ICollection<SelectListItem> GetFamilies(ICollection<FamilyModel> familyModels)
        {
            ICollection<SelectListItem> families = new List<SelectListItem>();
            foreach (var family in familyModels)
                families.Add(new SelectListItem() { Text = family.Name });
            return families;
        }

        private static ICollection<string> GetChildRelTypeList()
        {
            return new List<string>()
            {
                "Daughter", "Son", "Grand-Daughter", "Grand-Son",
                "Sister", "Brother", "Uncle", "Aunt", "Cousin",
                "Great Grand-Child", "Sister in-law", "Brother in-law", "Nephew"
            };
        }

        private static ICollection<string> GetCities()
        {
            return new List<string>()
            {
                "Petah-Tikva", "Tel-Aviv", "Eilat", "Ramat-Gan", "Bat-Yam", "Afula", "Haifa", "Jerusalem", "Kfar-Saba"
            };
        }
    }
}
