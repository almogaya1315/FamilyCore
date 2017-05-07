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

        string GetFilePath(HttpPostedFileBase file);
        ICollection<string> GetChildRelationshipTypes();
        ICollection<string> GetModelKeys(ModelStateSet forPage);

        void UpdateUserAbout(int memberId, string about);
        void UpdateMemberProfileImage(int memberId, HttpPostedFileBase file, bool updateDatabase);
        void UpdateUserDetails(ContactInfoModel postedInfo);
        void UpdateUserPermissions(int memberId, PermissionsModel postedPerms);
    }
}
