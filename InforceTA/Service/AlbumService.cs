using DB;
using DB.DBModels;
using InforceTA.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace InforceTA.Service
{
    public interface IAlbumService
    {

        public Task<IList<Album>> GetUsersAlbums(int usersId);
        public Task<IList<Album>> GetAlbumsList(int index, int countElements);
        public Task<Album> AddAlbum(AlbumInput newItem, int usersId);
        public Task DeleteAlbum(int albumId, int userId);
    }

    public class AlbumService : IAlbumService
    {
        private Func<PhotoGalleryContext> dbFactory;
        private IImageService imageService;

        public AlbumService(Func<PhotoGalleryContext> dbFactory, IImageService imageService)
        {
            this.dbFactory = dbFactory;
            this.imageService = imageService;
        }  

        public async Task<IList<Album>> GetUsersAlbums(int usersId)
        {
            var result = new List<Album>();

            using (var dbContext = dbFactory())
            {
                result = await dbContext.Albums.Where(x => x.OwnerId == usersId).ToListAsync();
            }

            return result;
        }

        public async Task<IList<Album>> GetAlbumsList(int index, int countElements)
        {
            var result = new List<Album>();

            using (var dbContext = dbFactory())
            {
                result = await dbContext.Albums.Skip(index).Take(countElements).ToListAsync();
            }

            return result;
        }

        public async Task<Album> AddAlbum(AlbumInput newItem, int usersId)
        {
            Album result = null;

            using (var dbContext = dbFactory())
            {
                result = new Album()
                {
                    Name = newItem.Name,
                    TimeCreated = DateTime.Now,
                    OwnerId = usersId,
                };
                dbContext.Albums.Add(result);
                await dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteAlbum(int albumId, int userId)
        {
            using (var dbContext = dbFactory())
            {
                var currentAlbum = dbContext.Albums.Where(x => x.Id == albumId).FirstOrDefault();
                var currentUser = dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
                var images = dbContext.Images.Where(x => x.AlbumId == albumId);

                if (currentAlbum != null && (currentUser?.isAdmin ?? false || currentAlbum?.OwnerId == userId))
                {
                    foreach (var image in images)
                        await imageService.DeleteImage(image.Id, userId);
                    dbContext.Albums.Remove(currentAlbum);
                    await dbContext.SaveChangesAsync();
                }
                else {
                    throw new Exception("");
                }
            }
        }

    }
}
