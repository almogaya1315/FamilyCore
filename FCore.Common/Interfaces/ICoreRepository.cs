using FCore.Common.Enums;
using FCore.Common.Models.Albums;
using FCore.Common.Models.ChatGroups;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FCore.Common.Interfaces
{
    public interface ICoreRepository : IDisposable
    {
        ICollection<FamilyModel> GetFamilies();
        FamilyModel GetFamily(int id);
        FamilyModel GetFamily(string name);

        FamilyMemberModel GetFamilyMember(int id);
        FamilyMemberModel GetLastMemberJoined();
        FamilyMemberModel SetPersonalInfo(FamilyMemberModel posted, string filePath);
        FamilyMemberModel SetContactInfo(FamilyMemberModel posted, ContactInfoModel info);
        FamilyMemberModel CreateMember(int creatorId, FamilyMemberModel postedMember, string relationship);
        FamilyMemberModel ConnectRelatives(FamilyMemberModel creator, FamilyMemberModel newMember);

        PermissionsModel GetPermissionsModel(int id);

        ICollection<AlbumModel> GetAlbums();
        AlbumModel GetAlbum(int id);
        ImageModel GetLastImageUploaded();

        ICollection<VideoLibraryModel> GetVideoLibraries();
        VideoLibraryModel GetVideoLibrary(int id);
        VideoModel GetMostViewedVideo();

        ICollection<ChatGroupModel> GetChatGroups();
        ChatGroupModel GetChatGroup(int id);
        MessageModel GetMessage(int id);

        ContactInfoModel GetContactInfo(int id);

        #region should be in supporting classes
        string GetFilePath(HttpPostedFileBase file);
        ICollection<SelectListItem> GetChildRelationshipTypes();
        ICollection<SelectListItem> GetGenderTypes();
        ICollection<SelectListItem> GetCities();
        #endregion

        void UpdateUserAbout(int memberId, string about);
        void UpdateMemberProfileImage(int memberId, HttpPostedFileBase file, bool updateDatabase);
        void UpdateUserDetails(ContactInfoModel postedInfo);
        void UpdateUserPermissions(int memberId, PermissionsModel postedPerms);
    }
}
