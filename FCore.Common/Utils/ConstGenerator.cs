using FCore.Common.Enums;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
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

        public static ICollection<SelectListItem> GetFamilySelectListItems(ICollection<FamilyModel> familyModels)
        {
            ICollection<SelectListItem> families = new List<SelectListItem>();
            if (familyModels.Count > 0)
            {
                families.Add(new SelectListItem() { Value = "ph", Disabled = true, Selected = true, Text = "Choose" });
                foreach (var family in familyModels)
                    families.Add(new SelectListItem() { Text = family.Name });
            }
            else families.Add(new SelectListItem() { Value = "ph", Disabled = true, Selected = true, Text = "No match" });
            
            return families;
        }

        public static ICollection<SelectListItem> GetMemberSelectListItems(ICollection<FamilyMemberModel> memberModels)
        {
            ICollection<SelectListItem> members = new List<SelectListItem>();
            foreach (var member in memberModels)
                members.Add(new SelectListItem() { Text = member.FirstName });
            return members;
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
