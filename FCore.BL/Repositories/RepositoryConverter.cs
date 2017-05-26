using FCore.Common.Enums;
using FCore.Common.Interfaces;
using FCore.Common.Models.Albums;
using FCore.Common.Models.ChatGroups;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Users;
using FCore.Common.Models.Videos;
using FCore.DAL.Entities;
using FCore.DAL.Entities.Albums;
using FCore.DAL.Entities.ChatGroups;
using FCore.DAL.Entities.Contacts;
using FCore.DAL.Entities.Families;
using FCore.DAL.Entities.Members;
using FCore.DAL.Entities.Videos;
using FCore.Identity.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.BL.Repositories
{
    public abstract class RepositoryConverter : IRepositoryConverterAsync<UserModel, UserEntity>,
                                                IRepositoryConverter<FamilyModel, FamilyEntity>,
                                                IRepositoryConverter<FamilyMemberModel, FamilyMemberEntity>,
                                                IRepositoryConverter<ContactInfoModel, ContactInfoEntity>,
                                                IRepositoryConverter<PermissionsModel, MemberPermissions>,
                                                IRepositoryConverter<RelativeModel, MemberRelative>,
                                                IRepositoryConverter<ContactBookModel, ContactBookEntity>,
                                                IRepositoryConverter<ImageModel, ImageEntity>,
                                                IRepositoryConverter<AlbumModel, AlbumEntity>,
                                                IRepositoryConverter<VideoModel, VideoEntity>,
                                                IRepositoryConverter<VideoLibraryModel, VideoLibraryEntity>,
                                                IRepositoryConverter<ChatGroupModel, ChatGroupEntity>,
                                                IRepositoryConverter<MessageModel, MessageEntity>

    {
        FamilyContext CoreDB { get; set; }
        UserContext UserDB { get; set; }

        public RepositoryConverter(FamilyContext db)
        {
            CoreDB = db;
        }
        public RepositoryConverter(UserContext db)
        {
            UserDB = db;
        }

        public async Task<UserModel> ConvertToModel(UserEntity entity)
        {
            var userModel = new UserModel()
            {
                MemberId = entity.MemberId,
                FamilyId = entity.FamilyId,
                FullName = entity.FullName,
                Id = entity.Id,
                UserName = entity.UserName,
                PasswordHash = entity.PasswordHash,
                Email = entity.Email,
                EmailConfirmed = entity.EmailConfirmed,
                PhoneNumber = entity.PhoneNumber,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                SecurityStamp = entity.SecurityStamp,
                AccessFailedCount = entity.AccessFailedCount,
                LockoutEnabled = entity.LockoutEnabled,
                LockoutEndDateUtc = entity.LockoutEndDateUtc,
                TwoFactorEnabled = entity.TwoFactorEnabled,
            };
            foreach (var claim in entity.Claims) userModel.Claims.Add(claim);
            foreach (var login in entity.Logins) userModel.Logins.Add(login);
            foreach (var role in entity.Roles) userModel.Roles.Add(role);

            return await Task.FromResult(userModel);
        }
        
        /*public IdentityUser ConvertToEntity(IdentityUser model)
        {
            // todo
            throw new NotImplementedException();

            //var userEntity = new UserEntity()
            //{
            //    MemberId = (model as UserModel).MemberId,
            //    FamilyId = (model as UserModel).FamilyId,
            //    FullName = (model as UserModel).FullName,
            //    Id = model.Id,
            //    UserName = model.UserName,
            //    PasswordHash = model.PasswordHash,
            //    Email = model.Email,
            //    EmailConfirmed = model.EmailConfirmed,
            //    PhoneNumber = model.PhoneNumber,
            //    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
            //    SecurityStamp = model.SecurityStamp,
            //    AccessFailedCount = model.AccessFailedCount,
            //    LockoutEnabled = model.LockoutEnabled,
            //    LockoutEndDateUtc = model.LockoutEndDateUtc,
            //    TwoFactorEnabled = model.TwoFactorEnabled,
            //};
            //foreach (var claim in model.Claims) userEntity.Claims.Add(claim);
            //foreach (var login in model.Logins) userEntity.Logins.Add(login);
            //foreach (var role in model.Roles) userEntity.Roles.Add(role);

            //return userEntity;
        }*/

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
                Gender = entity.Gender,
                About = entity.About,
                BirthDate = entity.BirthDate,
                BirthPlace = entity.BirthPlace,
                ContactInfo = ConvertToModel(CoreDB.GetContactInfo((int)entity.ContactInfoId)),
                ContactInfoId = (int)entity.ContactInfoId,
                FamilyId = (int)entity.FamilyId,
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName,
                PermissionId = (int)entity.PermissionId,
                Permissions = ConvertToModel(CoreDB.GetPermissionsEntity((int)entity.PermissionId)),
                ProfileImagePath = entity.ProfileImagePath,
                Relatives = entity.Relatives.Select(e => ConvertToModel(e)).ToList()
            };
        }
        public FamilyMemberEntity ConvertToEntity(FamilyMemberModel model)
        {
            return new FamilyMemberEntity()
            {
                Gender = model.Gender,
                About = model.About,
                BirthDate = model.BirthDate,
                BirthPlace = model.BirthPlace,
                ContactInfo = ConvertToEntity(model.ContactInfo),
                ContactInfoId = model.ContactInfoId,
                FamilyId = model.FamilyId,
                FirstName = model.FirstName,
                Id = model.Id,
                LastName = model.LastName,
                PermissionId = model.PermissionId,
                ProfileImagePath = model.ProfileImagePath,
                Relatives = model.Relatives.Select(r => ConvertToEntity(r)).ToList()
            };
        }

        public ContactInfoModel ConvertToModel(ContactInfoEntity entity)
        {
            return new ContactInfoModel()
            {
                City = entity.City,
                ContactBookId = (int)entity.ContactBookId,
                Country = entity.Country,
                Email = entity.Email,
                HouseNo = entity.HouseNo,
                Id = entity.Id,
                MemberName = entity.MemberName,
                PhoneNo = entity.PhoneNo,
                Street = entity.Street,
                MemberId = entity.MemberId
            };
        }
        public ContactInfoEntity ConvertToEntity(ContactInfoModel model)
        {
            return new ContactInfoEntity()
            {
                City = model.City,
                ContactBookId = model.ContactBookId,
                Country = model.Country,
                Email = model.Email,
                HouseNo = model.HouseNo,
                Id = model.Id,
                MemberName = model.MemberName,
                PhoneNo = model.PhoneNo,
                Street = model.Street,
                MemberId = model.MemberId
            };
        }

        public PermissionsModel ConvertToModel(MemberPermissions entity)
        {
            return new PermissionsModel()
            {
                Create = entity.Create,
                Edit = entity.Edit,
                Id = entity.Id,
                ManageChat = entity.ManageChat,
                Admin = entity.Admin
            };
        }
        public MemberPermissions ConvertToEntity(PermissionsModel model)
        {
            return new MemberPermissions()
            {
                Admin = model.Admin,
                Create = model.Create,
                Edit = model.Edit,
                Id = model.Id,
                ManageChat = model.ManageChat
            };
        }

        public RelativeModel ConvertToModel(MemberRelative entity)
        {
            return new RelativeModel(entity.MemberId, entity.RelativeId,
                                     (RelationshipType)Enum.Parse(typeof(RelationshipType), entity.Relationship));
        }
        public MemberRelative ConvertToEntity(RelativeModel model)
        {
            return new MemberRelative()
            {
                MemberId = model.MemberId,
                RelativeId = model.RelativeId,
                Relationship = model.Relationship
            };
        }

        public ContactBookModel ConvertToModel(ContactBookEntity entity)
        {
            return new ContactBookModel()
            {
                ContactInfoes = entity.ContactInfoes.Select(e => ConvertToModel(e)).ToList(),
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

        public AlbumModel ConvertToModel(AlbumEntity entity)
        {
            return new AlbumModel()
            {
                FamilyId = entity.Id,
                FamilyName = entity.FamilyName,
                Id = entity.Id,
                Images = entity.Images.Select(i => ConvertToModel(i)).ToList(),
                Name = entity.Name
            };
        }
        public AlbumEntity ConvertToEntity(AlbumModel model)
        {
            throw new NotImplementedException();
        }

        public ChatGroupModel ConvertToModel(ChatGroupEntity entity)
        {
            return new ChatGroupModel()
            {
                FamilyId = entity.FamilyId,
                Id = entity.Id,
                ManagerId = entity.ManagerId,
                Messages = entity.Messages.Select(m => ConvertToModel(m)).ToList(),
                Name = entity.Name
            };
        }
        public ChatGroupEntity ConvertToEntity(ChatGroupModel model)
        {
            throw new NotImplementedException();
        }

        public MessageModel ConvertToModel(MessageEntity entity)
        {
            return new MessageModel()
            {
                Content = entity.Content,
                GroupId = entity.GroupId,
                Id = entity.Id,
                RecieverId = entity.RecieverId,
                SenderId = entity.SenderId
            };
        }
        public MessageEntity ConvertToEntity(MessageModel model)
        {
            throw new NotImplementedException();
        }
    }
}
