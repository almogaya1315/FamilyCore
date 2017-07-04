using FCore.Common.Enums;
using FCore.Common.Models.Albums;
using FCore.Common.Models.ChatGroups;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Users;
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
        ICollection<FamilyModel> GetFamiliesDynamic(string text);
        FamilyModel GetFamily(int id);
        FamilyModel GetFamily(string name);

        ICollection<FamilyMemberModel> GetMembersDynamic(string familyName, string text = null);
        ICollection<FamilyMemberModel> GetFamilyMember(string family, string memberName);
        FamilyMemberModel GetFamilyMember(int id);
        FamilyMemberModel GetLastMemberJoined();
        FamilyMemberModel SetPersonalInfo(FamilyMemberModel posted, string filePath);
        FamilyMemberModel SetContactInfo(FamilyMemberModel posted, ContactInfoModel info);
        FamilyMemberModel CreateMember(FamilyMemberModel postedMember, int relativeId, string relationship);
        FamilyMemberModel ConnectRelatives(FamilyMemberModel relative, FamilyMemberModel newMember);
        IDictionary<FamilyMemberModel, ICollection<string>> VerifyMultipleRels(FamilyMemberModel relative, string relationship);

        PermissionsModel GetPermissionsModel(int id);

        ICollection<AlbumModel> GetAlbums();
        AlbumModel GetAlbum(int id);
        ImageModel GetLastImageUploaded();

        ICollection<VideoLibraryModel> GetVideoLibraries();
        VideoLibraryModel GetVideoLibrary(int id);
        VideoModel GetMostViewedVideo();
        VideoModel UpdateVideoDesc(int videoId, string newDesc);
        void DeleteVideo(VideoModel video, out int libraryId);

        ICollection<ChatGroupModel> GetChatGroups();
        ChatGroupModel GetChatGroup(int id);
        
        MessageModel GetMessage(int id);

        ContactInfoModel GetContactInfo(int id);

        void UpdateUserAbout(int memberId, string about);
        void UpdateMemberProfileImage(int memberId, HttpPostedFileBase file, bool updateDatabase);
        void UpdateUserDetails(ContactInfoModel postedInfo);
        void UpdateUserPermissions(int memberId, PermissionsModel postedPerms);
    }
}
