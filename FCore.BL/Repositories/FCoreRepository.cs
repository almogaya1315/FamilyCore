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
using FCore.Common.Models.Users;

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
        public ICollection<FamilyModel> GetFamiliesDynamic(string text)
        {
            ICollection<FamilyModel> families = new List<FamilyModel>();
            foreach (FamilyEntity family in CoreDB.GetFamilies())
            {
                if (family.Name.ToLower().Contains(text.ToLower()))
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

        public ICollection<FamilyMemberModel> GetMembersDynamic(string familyName, string text = null)
        {
            var family = GetFamily(familyName);
            ICollection<FamilyMemberModel> members = new List<FamilyMemberModel>();
            foreach (var member in family.FamilyMembers)
            {
                if (text != null)
                    if (!member.FirstName.ToLower().Contains(text.ToLower())) continue;
                members.Add(member);
            }

            return members;
        }
        public FamilyMemberModel GetFamilyMember(int id)
        {
            return ConvertToModel(CoreDB.GetFamilyMember(id));
        }
        public ICollection<FamilyMemberModel> GetFamilyMember(string family, string memberName)
        {
            ICollection<FamilyMemberModel> members = new List<FamilyMemberModel>();
            foreach (var member in GetFamily(family).FamilyMembers)
                if (member.FirstName == memberName) members.Add(member);
            return members;
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
        public FamilyMemberModel CreateMember(FamilyMemberModel postedMember, int relativeId, string relationship)
        {
            return ConvertToModel(CoreDB.CreateMember(relativeId, ConvertToEntity(postedMember), relationship));
        }
        public FamilyMemberModel ConnectRelatives(FamilyMemberModel relative, FamilyMemberModel newMember)
        {
            var createdRelativeEnum = (RelationshipType)Enum.Parse(typeof(RelationshipType), newMember.Relatives.FirstOrDefault().Relationship);
            foreach (var relativeModel in relative.Relatives)
            {
                if (relativeModel.RelativeId == newMember.Id) continue;
                var secondRelGender = (GenderType)Enum.Parse(typeof(GenderType), CoreDB.GetFamilyMember(relativeModel.RelativeId).Gender);
                var createdGender = (GenderType)Enum.Parse(typeof(GenderType), newMember.Gender);
                string createdRelativeRel = TreeHelper.GetThirdLevelRelationship(relativeModel, createdRelativeEnum, secondRelGender, createdGender);
                var createdRelativeModel = new RelativeModel(newMember.Id, relativeModel.RelativeId,
                                                            (RelationshipType)Enum.Parse(typeof(RelationshipType), createdRelativeRel))
                {
                    Member = newMember,
                    Relative = GetFamilyMember(relativeModel.RelativeId)
                };
                newMember.Relatives.Add(createdRelativeModel);
                CoreDB.UpdateMemberRelatives(ConvertToEntity(newMember), ConvertToEntity(createdRelativeModel));
            }
            return newMember;
        }
        public IDictionary<FamilyMemberModel, ICollection<string>> VerifyMultipleRels(FamilyMemberModel relative, string relationship)
        {
            return null;
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
        public VideoModel UpdateVideoDesc(int videoId, string newDesc)
        {
            return ConvertToModel(CoreDB.UpdateVideoDesc(videoId, newDesc));
        }
        public ICollection<VideoModel> GetVideoByDescription(int libId, string searchText)
        {
            ICollection<VideoModel> videos = new List<VideoModel>();
            var videoEntities = CoreDB.GetVideoByDescription(libId, searchText);
            if (videoEntities.Count > 0)
                foreach (var entity in videoEntities)
                    videos.Add(ConvertToModel(entity));
            return videos;
        }
        public void DeleteVideo(VideoModel video, out int libraryId)
        {
            CoreDB.DeleteVideo(video.Id, out libraryId);

            // todo.. remove actual video file
        }
        public void SaveVideo(string filePath, int libId)
        {
            CoreDB.SaveVideo(filePath, libId);
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

        public void UpdateUserAbout(int memberId, string about)
        {
            CoreDB.UpdateUserAbout(CoreDB.GetFamilyMember(memberId), about); // ConvertToEntity
        }
        public void UpdateMemberProfileImage(int memberId, HttpPostedFileBase file, bool updateDatabase)
        {
            InputHelper.UploadProfileImage(file);
            if (updateDatabase)
            {
                CoreDB.UpdateMemberProfileImage(CoreDB.GetFamilyMember(memberId), InputHelper.GetFilePath(file)); // ConvertToEntity
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
