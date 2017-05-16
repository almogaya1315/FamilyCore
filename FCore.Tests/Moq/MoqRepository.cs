using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCore.Common.Enums;
using FCore.Common.Models.Albums;
using FCore.Common.Models.ChatGroups;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Videos;
using System.Web.Mvc;
using System.Web;
using FCore.Common.Utils;
using FCore.DAL.Entities.Families;
using FCore.DAL.Entities;
using FCore.DAL.Entities.Contacts;
using FCore.BL.Repositories;

namespace FCore.Tests.Moq
{
    public class MoqRepository : RepositoryConverter, ICoreRepository
    {
        FamilyContext CoreDB { get; set; }

        public MoqRepository() : base(new FamilyContext())
        {
            CoreDB = new FamilyContext();
        }

        public FamilyMemberModel ConnectRelatives(FamilyMemberModel creator, FamilyMemberModel newMember)
        {
            var createdCreatorRel = (RelationshipType)Enum.Parse(typeof(RelationshipType), newMember.Relatives.FirstOrDefault().Relationship);
            foreach (var relativeModel in creator.Relatives)
            {
                if (relativeModel.Relative.Id == newMember.Id) continue;
                string createdRelativeRel = TreeHelper.GetThirdLevelRelationship(relativeModel, createdCreatorRel);
                newMember.Relatives.Add(new RelativeModel(newMember.Id, relativeModel.Relative.Id,
                                       (RelationshipType)Enum.Parse(typeof(RelationshipType), createdRelativeRel))
                {
                    Member = newMember,
                    Relative = relativeModel.Relative
                });
                //CoreDB.UpdateMemberRelatives(ConvertToEntity(newMember));
            }
            return newMember;
        }

        public FamilyMemberModel CreateMember(int creatorId, FamilyMemberModel postedMember, string relationship)
        {
            var gaya = new FamilyMemberModel()
            {
                Id = 3,
                FirstName = "Gaya",
                LastName = "Matsliah",
                About = "Test",
                BirthDate = new DateTime(2013, 6, 6),
                BirthPlace = "Test",
                Gender = GenderType.Female.ToString(),
                ProfileImagePath = "Test",
                ContactInfoId = 3,
                ContactInfo = new ContactInfoModel()
                {
                    Id = 3,
                    MemberId = 3,
                    Country = "Israel",
                    City = "Petah-Tikva",
                    Street = "",
                    HouseNo = 1,
                    PhoneNo = "",
                    Email = "",
                    MemberName = "Gaya Matsliah",
                    ContactBookId = 1,
                    ContactBook = new ContactBookModel() { Id = 1, FamilyId = 1, FamilyName = "Matsliah" }
                },
                FamilyId = 1,
                Family = new FamilyModel() { Id = 1, Name = "Matsliah" },
                PermissionId = 3,
                Permissions = new PermissionsModel() { Id = 3 }
            };
            gaya.Relatives.Add(new RelativeModel(3, 1, RelationshipType.Father)
            {
                Member = gaya,
                Relative = GetFamilyMember(1)
            });
            return gaya;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public AlbumModel GetAlbum(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AlbumModel> GetAlbums()
        {
            throw new NotImplementedException();
        }

        public ChatGroupModel GetChatGroup(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ChatGroupModel> GetChatGroups()
        {
            throw new NotImplementedException();
        }

        public ICollection<SelectListItem> GetChildRelationshipTypes()
        {
            throw new NotImplementedException();
        }

        public ICollection<SelectListItem> GetCities()
        {
            throw new NotImplementedException();
        }

        public ContactInfoModel GetContactInfo(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<FamilyModel> GetFamilies()
        {
            throw new NotImplementedException();
        }

        public FamilyModel GetFamily(string name)
        {
            throw new NotImplementedException();
        }

        public FamilyModel GetFamily(int id)
        {
            throw new NotImplementedException();
        }

        public FamilyMemberModel GetFamilyMember(int id)
        {
            if (id == 1) return new FamilyMemberModel()
            {
                Id = 1,
                FirstName = "Lior",
                LastName = "Matsliah",
                About = "Test",
                BirthDate = new DateTime(1985, 5, 23),
                BirthPlace = "Test",
                Gender = GenderType.Male.ToString(),
                ProfileImagePath = "Test",
                ContactInfoId = 1,
                ContactInfo = new ContactInfoModel()
                {
                    Id = 1,
                    MemberId = 1,
                    Country = "Israel",
                    City = "Petah-Tikva",
                    Street = "",
                    HouseNo = 1,
                    PhoneNo = "",
                    Email = "",
                    MemberName = "Lior Matsliah",
                    ContactBookId = 1,
                    ContactBook = new ContactBookModel() { Id = 1, FamilyId = 1, FamilyName = "Matsliah" }
                },
                FamilyId = 1,
                Family = new FamilyModel() { Id = 1, Name = "Matsliah" },
                PermissionId = 1,
                Permissions = new PermissionsModel() { Id = 1 }
            };
            else return new FamilyMemberModel()
            {
                Id = 2,
                FirstName = "Keren",
                LastName = "Matsliah",
                About = "Test",
                BirthDate = new DateTime(1984, 2, 5),
                BirthPlace = "Test",
                Gender = GenderType.Female.ToString(),
                ProfileImagePath = "Test",
                ContactInfoId = 2,
                ContactInfo = new ContactInfoModel()
                {
                    Id = 2,
                    MemberId = 2,
                    Country = "Israel",
                    City = "Petah-Tikva",
                    Street = "",
                    HouseNo = 1,
                    PhoneNo = "",
                    Email = "",
                    MemberName = "Keren Matsliah",
                    ContactBookId = 1,
                    ContactBook = new ContactBookModel() { Id = 1, FamilyId = 1, FamilyName = "Matsliah" }
                },
                FamilyId = 1,
                Family = new FamilyModel() { Id = 1, Name = "Matsliah" },
                PermissionId = 2,
                Permissions = new PermissionsModel() { Id = 2 }
            };
        }

        public string GetFilePath(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }

        public ICollection<System.Web.Mvc.SelectListItem> GetGenderTypes()
        {
            throw new NotImplementedException();
        }

        public ImageModel GetLastImageUploaded()
        {
            throw new NotImplementedException();
        }

        public FamilyMemberModel GetLastMemberJoined()
        {
            throw new NotImplementedException();
        }

        public MessageModel GetMessage(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> GetModelKeys(ModelStateSet forPage)
        {
            throw new NotImplementedException();
        }

        public VideoModel GetMostViewedVideo()
        {
            throw new NotImplementedException();
        }

        public PermissionsModel GetPermissionsModel(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<VideoLibraryModel> GetVideoLibraries()
        {
            throw new NotImplementedException();
        }

        public VideoLibraryModel GetVideoLibrary(int id)
        {
            throw new NotImplementedException();
        }

        public FamilyMemberModel SetContactInfo(FamilyMemberModel posted, ContactInfoModel info)
        {
            throw new NotImplementedException();
        }

        public ViewDataDictionary SetModelState(ViewDataDictionary viewData, ModelStateDictionary modelState, ModelStateSet forPage)
        {
            throw new NotImplementedException();
        }

        public FamilyMemberModel SetPersonalInfo(FamilyMemberModel posted, string filePath)
        {
            throw new NotImplementedException();
        }

        public void UpdateMemberProfileImage(int memberId, HttpPostedFileBase file, bool updateDatabase)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserAbout(int memberId, string about)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserDetails(ContactInfoModel postedInfo)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserPermissions(int memberId, PermissionsModel postedPerms)
        {
            throw new NotImplementedException();
        }
    }
}
