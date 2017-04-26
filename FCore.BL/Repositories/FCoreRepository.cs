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

        public void UpdateUserAbout(int memberId, string about)
        {
            CoreDB.UpdateUserAbout(CoreDB.GetFamilyMember(memberId), about);
        }
        #endregion

        public void Dispose()
        {
            CoreDB.Dispose();
        }
    }
}
