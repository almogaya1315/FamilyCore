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

namespace FCore.BL.Repositories
{
    public class FCoreRepository : ICoreRepository, 
                                   IRepositoryConverter<FamilyModel, FamilyEntity>,
                                   IRepositoryConverter<FamilyMemberModel, FamilyMemberEntity>
    {
        protected FamilyContext CoreDB { get; private set; }

        

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
            throw new NotImplementedException();
        }

        public FamilyModel GetFamily(int id)
        {
            throw new NotImplementedException();
        }

        #region converter methods
        public FamilyEntity ConvertToEntity(FamilyModel model)
        {
            throw new NotImplementedException();
        }
        public FamilyModel ConvertToModel(FamilyEntity entity)
        {
            return new FamilyModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                FamilyMembers = entity.FamilyMembers.Select(e => ConvertToModel(e)).ToList(),
                ContactBooks = new List<ContactBookModel>(),
                Albums = new List<AlbumModel>(),
                VideoLibraries = new List<VideoLibraryModel>(),
                ChatGroups = new List<ChatGroupModel>()
            };
        }

        public FamilyMemberModel ConvertToModel(FamilyMemberEntity entity)
        {
            return new FamilyMemberModel()
            {
                BirthDate = entity.BirthDate,
                BirthPlace = entity.BirthPlace,
                ContactInfo = ConvertToModel(entity.ContactInfo),
                ContactInfoId = entity.ContactInfoId,
                Family = ConvertToModel(entity.Family), // possible infinite loop 
                FamilyId = entity.FamilyId,
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName,
                PermissionId = entity.PermissionId,
                Permissions = ConvertToModel(entity.Permissions),
                ProfileImagePath = entity.ProfileImagePath,
                Relatives = entity.Relatives.Select(e => ConvertToModel(e))
            };
        }
        public FamilyMemberEntity ConvertToEntity(FamilyMemberModel model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
