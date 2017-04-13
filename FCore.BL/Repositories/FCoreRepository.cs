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
    public class FCoreRepository : ICoreRepository, 
                                   IRepositoryConverter<FamilyModel, FamilyEntity>,
                                   IRepositoryConverter<FamilyMemberModel, FamilyMemberEntity>,
                                   IRepositoryConverter<ContactInfoModel, ContactInfoEntity>,
                                   IRepositoryConverter<PermissionsModel, MemberPermissions>,
                                   IRepositoryConverter<RelativeModel, MemberRelative>,
                                   IRepositoryConverter<ContactBookModel, ContactBookEntity>,
                                   IRepositoryConverter<ImageModel, ImageEntity>,
                                   IRepositoryConverter<VideoModel, VideoEntity>,
                                   IRepositoryConverter<VideoLibraryModel, VideoLibraryEntity>
    {
        protected FamilyContext CoreDB { get; private set; }
        
        public FCoreRepository()
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

        #region IConverter
        public FamilyModel ConvertToModel(FamilyEntity entity)
        {
            return new FamilyModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                FamilyMembers = entity.FamilyMembers.Select(e => ConvertToModel(e)).ToList(),
                ContactBooks = entity.ContactBooks.Select(e => ConvertToModel(e)).ToList(),
                Albums = new List<AlbumModel>(),
                VideoLibraries = new List<VideoLibraryModel>(),
                ChatGroups = new List<ChatGroupModel>()
            };
        }
        public FamilyEntity ConvertToEntity(FamilyModel model)
        {
            throw new NotImplementedException();
        }

        public FamilyMemberModel ConvertToModel(FamilyMemberEntity entity)
        {
            return new FamilyMemberModel()
            {
                BirthDate = entity.BirthDate,
                BirthPlace = entity.BirthPlace,
                ContactInfo = ConvertToModel(CoreDB.GetContactInfo(entity.ContactInfoId)),
                ContactInfoId = entity.ContactInfoId,
                //Family = ConvertToModel(entity.Family), 
                FamilyId = entity.FamilyId,
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName,
                PermissionId = entity.PermissionId,
                Permissions = ConvertToModel(entity.Permissions),
                ProfileImagePath = entity.ProfileImagePath,
                Relatives = entity.Relatives.Select(e => ConvertToModel(e)).ToList()
            };
        }
        public FamilyMemberEntity ConvertToEntity(FamilyMemberModel model)
        {
            throw new NotImplementedException();
        }

        public ContactInfoModel ConvertToModel(ContactInfoEntity entity)
        {
            return new ContactInfoModel()
            {
                City = entity.City,
                //ContactBook = ConvertToModel(CoreDB.GetContactBook(entity.ContactBookId)),
                ContactBookId = entity.ContactBookId,
                Country = entity.Country,
                Email = entity.Email,
                HouseNo = entity.HouseNo,
                Id = entity.Id,
                MemberName = entity.MemberName,
                PhoneNo = entity.PhoneNo,
                Street = entity.Street
            };
        }
        public ContactInfoEntity ConvertToEntity(ContactInfoModel model)
        {
            throw new NotImplementedException();
        }

        public PermissionsModel ConvertToModel(MemberPermissions entity)
        {
            return new PermissionsModel()
            {
                Create = entity.Create,
                Edit = entity.Edit,
                Id = entity.Id,
                ManageChat = entity.ManageChat
            };
        }
        public MemberPermissions ConvertToEntity(PermissionsModel model)
        {
            throw new NotImplementedException();
        }

        public RelativeModel ConvertToModel(MemberRelative entity)
        {
            return new RelativeModel(entity.MemberId, entity.RelativeId,
                                     (RelationshipType)Enum.Parse(typeof(RelationshipType), entity.Relationship));
        }
        public MemberRelative ConvertToEntity(RelativeModel model)
        {
            throw new NotImplementedException();
        }

        public ContactBookModel ConvertToModel(ContactBookEntity entity)
        {
            return new ContactBookModel()
            {
                ContactInfoes = entity.ContactInfoes.Select(e => ConvertToModel(e)).ToList(),
                //Family = ConvertToModel(entity.Family),
                FamilyId = entity.FamilyId,
                FamilyName = entity.FamilyName,
                Id = entity.Id
            };
        }
        public ContactBookEntity ConvertToEntity(ContactBookModel model)
        {
            throw new NotImplementedException();
        }

        public ImageModel ConvertToModel(ImageEntity entity)
        {
            return new ImageModel()
            {
                AlbumId = entity.AlbumId,
                Description = entity.Description,
                Id = entity.Id,
                Path = entity.Path
            };
        }
        public ImageEntity ConvertToEntity(ImageModel model)
        {
            throw new NotImplementedException();
        }

        public VideoModel ConvertToModel(VideoEntity entity)
        {
            return new VideoModel()
            {
                Description = entity.Description,
                Id = entity.Id,
                Libraryid = entity.Libraryid,
                Path = entity.Path
            };
        }
        public VideoEntity ConvertToEntity(VideoModel model)
        {
            throw new NotImplementedException();
        }

        public VideoLibraryModel ConvertToModel(VideoLibraryEntity entity)
        {
            return new VideoLibraryModel()
            {
                FamilyId = entity.FamilyId,
                FamilyName = entity.FamilyName,
                Id = entity.Id,
                Name = entity.Name,
                Videos = entity.Videos.Select(v => ConvertToModel(v)).ToList()
            };
        }
        public VideoLibraryEntity ConvertToEntity(VideoLibraryModel model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
