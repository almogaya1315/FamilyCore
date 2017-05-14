using FCore.Common.Interfaces;
using FCore.DAL.Entities.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Albums;
using FCore.Common.Models.Videos;
using FCore.Common.Models.ChatGroups;
using FCore.DAL.Entities;
using FCore.DAL.Entities.Contacts;
using FCore.DAL.Entities.Members;
using FCore.Common.Enums;
using FCore.DAL.Entities.Albums;
using FCore.DAL.Entities.Videos;
using FCore.DAL.Entities.ChatGroups;
using System.Web;
using System.IO;
using FCore.Common.Utils;
using System.Web.Mvc;

namespace FCore.BL.Repositories
{
    public class FCoreRepository : RepositoryConverter, ICoreRepository
    {
        FamilyContext CoreDB { get; set; }

        public FCoreRepository() : base(new FamilyContext())
        {
            CoreDB = new FamilyContext();
        }

        #region IRepository
        public ICollection<FamilyModel> GetFamilies()
        {
            ICollection<FamilyModel> families = new List<FamilyModel>();
            foreach (FamilyEntity family in CoreDB.GetFamilies())
            {
                families.Add(ConvertToModel(family));
            }
            return families;
        }
        public FamilyModel GetFamily(string name)
        {
            return ConvertToModel(CoreDB.GetFamily(name));
        }
        public FamilyModel GetFamily(int id)
        {
            return ConvertToModel(CoreDB.GetFamily(id));
        }

        public FamilyMemberModel GetFamilyMember(int id)
        {
            return ConvertToModel(CoreDB.GetFamilyMember(id));
        }
        public FamilyMemberModel GetLastMemberJoined()
        {
            return ConvertToModel(CoreDB.GetLastMemberJoined());
        }
        /// <summary>
        /// Sets the personal info properties that were posted in html form, 
        /// by creating a new member object without the initial creator model credentials 
        /// such as Id, PermissionsId & ContactInfoId. 
        /// For add-child wizard step 1.
        /// Updates the profile image path also. Doesn't save file. 
        /// This action accures in create-child wizard step 4.
        /// </summary>
        /// <param name="posted">The posted model object created with only the personal info data, for add-child wizard step 1</param>
        /// <param name="filePath">The HttpPostedFileBase string path</param>
        /// <returns>The temporary member object with personal info & profile picutre data</returns>
        public FamilyMemberModel SetPersonalInfo(FamilyMemberModel posted, string filePath)
        {
            return new FamilyMemberModel()
            {
                BirthDate = posted.BirthDate,
                BirthPlace = posted.BirthPlace,
                FirstName = posted.FirstName,
                Gender = posted.Gender,
                LastName = posted.LastName,
                ProfileImagePath = filePath
            };
        }
        public FamilyMemberModel SetContactInfo(FamilyMemberModel posted, ContactInfoModel info)
        {
            return new FamilyMemberModel()
            {
                BirthDate = posted.BirthDate,
                BirthPlace = posted.BirthPlace,
                FirstName = posted.FirstName,
                LastName = posted.LastName,
                Gender = posted.Gender,
                ProfileImagePath = posted.ProfileImagePath,
                ContactInfo = new ContactInfoModel()
                {
                    Country = info.Country,
                    City = info.City,
                    Street = info.Street,
                    HouseNo = info.HouseNo,
                    PhoneNo = info.PhoneNo,
                    Email = info.Email
                }
            };
        }
        public FamilyMemberModel CreateMember(int creatorId, FamilyMemberModel postedMember, string relationship)
        {
            if ((bool)postedMember.IsAdult)
            {
                // to do.. in sigh-in feature
                // CoreDB.CreateAdult(ConvertToEntity(postedMember));
                return null;
            }
            else
            {
                return ConvertToModel(CoreDB.CreateChild(creatorId, ConvertToEntity(postedMember), relationship));
            }
        }

        public PermissionsModel GetPermissionsModel(int id)
        {
            return ConvertToModel(CoreDB.GetPermissionsEntity(id));
        }

        public VideoLibraryModel GetVideoLibrary(int id)
        {
            return ConvertToModel(CoreDB.GetVideoLibrary(id));
        }
        public ICollection<VideoLibraryModel> GetVideoLibraries()
        {
            ICollection<VideoLibraryModel> libraries = new List<VideoLibraryModel>();
            foreach (VideoLibraryEntity library in CoreDB.GetVideoLibraries())
            {
                libraries.Add(ConvertToModel(library));
            }
            return libraries;
        }
        public VideoModel GetMostViewedVideo()
        {
            return ConvertToModel(CoreDB.GetMostViewedVideo());
        }

        public ICollection<AlbumModel> GetAlbums()
        {
            ICollection<AlbumModel> albums = new List<AlbumModel>();
            foreach (AlbumEntity album in CoreDB.GetAlbums())
            {
                albums.Add(ConvertToModel(album));
            }
            return albums;
        }
        public AlbumModel GetAlbum(int id)
        {
            return ConvertToModel(CoreDB.GetAlbum(id));
        }
        public ImageModel GetLastImageUploaded()
        {
            return ConvertToModel(CoreDB.GetLastImageUploaded());
        }

        public ICollection<ChatGroupModel> GetChatGroups()
        {
            ICollection<ChatGroupModel> groups = new List<ChatGroupModel>();
            foreach (ChatGroupEntity group in CoreDB.GetChatGroups())
            {
                groups.Add(ConvertToModel(group));
            }
            return groups;
        }
        public ChatGroupModel GetChatGroup(int id)
        {
            return ConvertToModel(CoreDB.GetChatGroup(id));
        }
        public MessageModel GetMessage(int id)
        {
            return ConvertToModel(CoreDB.GetMessage(id));
        }

        public ContactInfoModel GetContactInfo(int id)
        {
            return ConvertToModel(CoreDB.GetContactInfo(id));
        }

        public string GetFilePath(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                return $"~/Images/Profiles/{pic}";
            }
            else return string.Empty;
        }

        public ICollection<SelectListItem> GetChildRelationshipTypes()
        {
            ICollection<SelectListItem> relListItems = new List<SelectListItem>();
            foreach (string rel in ConstGenerator.ChildRelTypes)
                relListItems.Add(new SelectListItem() { Text = rel });
            return relListItems;
        }
        public ICollection<SelectListItem> GetGenderTypes()
        {
            ICollection<SelectListItem> genListItems = new List<SelectListItem>();
            foreach (string gen in Enum.GetNames(typeof(GenderType)).ToList())
                genListItems.Add(new SelectListItem() { Text = gen });
            return genListItems;
        }
        public ICollection<SelectListItem> GetCities()
        {
            ICollection<SelectListItem> cities = new List<SelectListItem>();
            foreach (string city in ConstGenerator.Cities)
                cities.Add(new SelectListItem() { Text = city });
            return cities;
        }

        public ICollection<string> GetModelKeys(ModelStateSet forPage)
        {
            switch (forPage)
            {
                case ModelStateSet.ForProfileImage:
                    return new List<string>()
                {
                    "FamilyId", "PermissionId", "ContactInfoId", "FirstName", "LastName", "About", "Gender", "BirthPlace", "BirthDate"
                };
                case ModelStateSet.ForPersonalInfo:
                    return new List<string>()
                {
                    "Id", "FamilyId", "PermissionId", "ContactInfoId", "About", "ProfileImagePath"
                };
                case ModelStateSet.ForContactInfo:
                    return new List<string>() { "Email" };
                case ModelStateSet.ForLifeStory:
                    return new List<string>() { "Id", "FamilyId", "PermissionId", "ContactInfoId", };
                default:
                    throw new InvalidOperationException("The ModelStateSet passed was not implemented in switch.");
            }
        }
        public ViewDataDictionary SetModelState(ViewDataDictionary viewData, ModelStateDictionary modelState, ModelStateSet forPage)
        {
            switch (forPage)
            {
                case ModelStateSet.ForProfileImage:
                    break;
                case ModelStateSet.ForPersonalInfo:
                    if (modelState.FirstOrDefault(state => state.Key == "FirstName").Value.Errors.Count > 0)
                    {
                        viewData["fnstate"] = true;
                    }
                    if (modelState.FirstOrDefault(state => state.Key == "LastName").Value.Errors.Count > 0)
                    {
                        viewData["lnstate"] = true;
                    }
                    if (modelState.FirstOrDefault(state => state.Key == "BirthDate").Value.Errors.Count > 0) // Value.AttemptedValue == string.Empty)
                    {
                        viewData["bdstate"] = true;
                    }
                    if (modelState.FirstOrDefault(state => state.Key == "BirthPlace").Value.Errors.Count > 0)
                    {
                        viewData["bpstate"] = true;
                    }
                    break;
                case ModelStateSet.ForContactInfo:
                    break;
                case ModelStateSet.ForLifeStory:
                    break;
            }
            
            return viewData;
        }

        public void UpdateUserAbout(int memberId, string about)
        {
            CoreDB.UpdateUserAbout(CoreDB.GetFamilyMember(memberId), about); // ConvertToEntity
        }
        public void UpdateMemberProfileImage(int memberId, HttpPostedFileBase file, bool updateDatabase)
        {
            string pic = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Profiles/"), pic);
            file.SaveAs(path);

            if (updateDatabase)
            {
                CoreDB.UpdateMemberProfileImage(CoreDB.GetFamilyMember(memberId), GetFilePath(file)); // ConvertToEntity
            }
        }
        public void UpdateUserDetails(ContactInfoModel postedInfo)
        {
            CoreDB.UpdateUserDetails(ConvertToEntity(postedInfo));
        }
        public void UpdateUserPermissions(int memberId, PermissionsModel postedPerms)
        {
            CoreDB.UpdateUserPermissions(memberId, ConvertToEntity(postedPerms));
        }
        #endregion

        public void Dispose()
        {
            CoreDB.Dispose();
        }
    }
}
