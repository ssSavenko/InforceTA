using DB;
using DB.DBModels;
using Microsoft.EntityFrameworkCore;

namespace InforceTA.Service
{
    public interface ILikesService
    {
        public Task<int> GetLikesCount(int imageId);
        public Task AddLike(  int imageId, int userId);
        public Task DeleteLike( int imageId, int userId);
    }

    public class LikesService : ILikesService
    {
        private Func<PhotoGalleryContext> dbFactory;

        public LikesService(Func<PhotoGalleryContext> dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public async Task<int> GetLikesCount(int imageId)
        {
            int result = 0;
            using (var dbContext = dbFactory())
            {
                result= await dbContext.Likes.Where(x => x.ImageId == imageId).CountAsync();
            }
            return result;
        }

        public async Task AddLike( int imageId, int userId)
        {
            using (var dbContext = dbFactory())
            {
                if (dbContext.Likes.Where(x => x.UserId == userId && x.ImageId == imageId).Count() == 0)
                {
                    dbContext.Likes.Add(new Like() { UserId = userId, ImageId = imageId });
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteLike( int imageId, int userId)
        {

            using (var dbContext = dbFactory())
            {
                dbContext.Likes.RemoveRange(dbContext.Likes.Where(x => x.UserId == userId && x.ImageId == imageId));
                await dbContext.SaveChangesAsync();
            }
        }

    }
}