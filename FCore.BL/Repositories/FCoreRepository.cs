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

        public ImageModel GetLastImageUploaded()
        {
            return ConvertToModel(CoreDB.GetLastImageUploaded());
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
        #endregion

        public void Dispose()
        {
            CoreDB.Dispose();
        }
    }
}
