using FCore.Common.Models.Albums;
using FCore.Common.Models.Contacts;
using FCore.Common.Models.Families;
using FCore.Common.Models.Members;
using FCore.Common.Models.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Interfaces
{
    public interface ICoreRepository : IDisposable
    {
        ICollection<FamilyModel> GetFamilies();

        FamilyModel GetFamily(int id);
        FamilyModel GetFamily(string name);

        FamilyMemberModel GetFamilyMember(int id);
        FamilyMemberModel GetLastMemberJoined();

        ICollection<AlbumModel> GetAlbums();
        AlbumModel GetAlbum(int id);
        ImageModel GetLastImageUploaded();

        ICollection<VideoLibraryModel> GetVideoLibraries();
        VideoLibraryModel GetVideoLibrary(int id);
        VideoModel GetMostViewedVideo();
    }
}
