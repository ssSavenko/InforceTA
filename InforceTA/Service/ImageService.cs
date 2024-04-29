using DB.DBModels;
using DB;
using Microsoft.EntityFrameworkCore;
using InforceTA.Models;

namespace InforceTA.Service
{
    public interface IImageService
    {

        public Task<IList<Image>> GetImages(int albumId, int index, int countElements);
        public Task<Image> AddImage(ImageInput newItem, int usersId);
        public Task DeleteImage(int imageId, int userId);
    }

    public class ImageService : IImageService
    {
        private Func<PhotoGalleryContext> dbFactory;
        private ILikesService likesService;
        private IDislikesService dislikesService;

        public ImageService(Func<PhotoGalleryContext> dbFactory,
            ILikesService likesService,
            IDislikesService dislikesService)
        {
            this.dbFactory = dbFactory;
            this.likesService = likesService;
            this.dislikesService = dislikesService;

        }

        public async Task<IList<Image>> GetImages(int albumId, int index, int countElements)
        {
            var result = new List<Image>();

            using (var dbContext = dbFactory())
            {
                result = await dbContext.Images.Where(x => x.AlbumId == albumId).Skip(index).Take(countElements).ToListAsync();
            }

            return result;
        }

        public async Task<Image> AddImage(ImageInput newItem, int userId)
        {
            Image result = null;

            using (var dbContext = dbFactory())
            {
                var currentUser = dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
                var currentAlbum = dbContext.Albums.Where(x => x.OwnerId == userId && x.Id == newItem.AlbumId).FirstOrDefault();
                if (currentUser != null && currentAlbum != null)
                {
                    result = new Image()
                    {
                        AlbumId = newItem.AlbumId,
                        URL = newItem.Base64Code,
                        BlobReference = ""
                    };
                    dbContext.Images.Add(result);
                    await dbContext.SaveChangesAsync();
                }
            }
            return result!;
        }

        public async Task DeleteImage(int imageId, int userId)
        {
            using (var dbContext = dbFactory())
            {
                var currentImage = dbContext.Images.Where(x => x.Id == imageId).FirstOrDefault();
                var currentAlbum = dbContext.Albums.Where(x => x.Id == imageId).FirstOrDefault();
                var currentUser = dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();

                if (currentImage != null && (currentUser?.isAdmin ?? false || currentAlbum?.OwnerId == userId))
                {
                    await dislikesService.DeleteDislike(imageId, userId);
                    await likesService.DeleteLike(imageId, userId);
                    dbContext.Images.Remove(currentImage);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("");
                }
            }
        }

    }
}
