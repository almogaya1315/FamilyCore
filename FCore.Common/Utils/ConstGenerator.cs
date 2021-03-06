﻿using FCore.Common.Enums;
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
        public static string UserIdentityCookieName
        {
            get
            {
                return "FCore.Acount.userIdentityModel";
            }
        }

        public static string UserContextConnectionString
        {
            get
            {
                return "name=UserContext";
            }
        }

        public static string VideoPath
        {
            get
            {
                return "~/Videos/libId";
            }
        }

        public static string ImagePath
        {
            get
            {
                return "~/Images/Profiles/";
            }
        }

        public static ICollection<SelectListItem> RelTypes
        {
            get
            {
                ICollection<SelectListItem> rels = new List<SelectListItem>();
                foreach (var rel in Enum.GetNames(typeof(RelationshipType)))
                {
                    if (rel.Contains("_"))
                    {
                        string r = rel.Replace('_', ' ');
                        rels.Add(new SelectListItem() { Text = r });
                        continue;
                    }
                    rels.Add(new SelectListItem() { Text = rel });
                }
                return rels;
            }
            private set { }
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

        public static ICollection<SelectListItem> GetMemberSelectListItems(ICollection<FamilyMemberModel> memberModels = null)
        {
            ICollection<SelectListItem> members = new List<SelectListItem>();
            if (memberModels == null)
            {
                return SetPlaceHolder("Choose family first"); 
            }
            else if (memberModels.Count > 0)
            {
                members = SetPlaceHolder("Choose"); 
                foreach (var member in memberModels)
                    members.Add(new SelectListItem() { Text = member.FirstName });
                return members;
            }
            else return SetPlaceHolder("No match"); 
        }

        public static ICollection<SelectListItem> GetRelTypesSelectedItem(string selected)
        {
            ICollection<SelectListItem> rels = new List<SelectListItem>();
            foreach (string rel in Enum.GetNames(typeof(RelationshipType)))
            {
                if (rel == selected) rels.Add(new SelectListItem() { Text = rel, Selected = true });
                else rels.Add(new SelectListItem() { Text = rel });
            }
            return rels;
        }

        public static ICollection<SelectListItem> GetCitiesSelectedItem(string selected)
        {
            ICollection<SelectListItem> cities = new List<SelectListItem>();
            foreach (string city in GetCities())
            {
                if (city == selected) cities.Add(new SelectListItem() { Text = city, Selected = true });
                else cities.Add(new SelectListItem() { Text = city });
            }
            return cities;
        }

        private static ICollection<SelectListItem> SetPlaceHolder(string text)
        {
            return new List<SelectListItem>() { new SelectListItem()
            {
                Value = "ph",
                Disabled = true,
                Selected = true,
                Text = text
            }};
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
