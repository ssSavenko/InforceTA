using DB.DBModels;
using DB;
using Microsoft.EntityFrameworkCore;

namespace InforceTA.Service
{
    public interface IDislikesService
    {
        public Task<int> GetDislikesCount(int imageId);
        public Task AddDislike(int imageId, int userId);
        public Task DeleteDislike(int imageId, int userId);
    }

    public class DislikesService : IDislikesService
    {
        private Func<PhotoGalleryContext> dbFactory;

        public DislikesService(Func<PhotoGalleryContext> dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public async Task<int> GetDislikesCount(int imageId)
        {
            int result = 0;
            using (var dbContext = dbFactory())
            {
                result = await dbContext.Dislikes.Where(x => x.ImageId == imageId).CountAsync();
            }
            return result;
        }

        public async Task AddDislike(int imageId, int userId)
        {
            using (var dbContext = dbFactory())
            {
                if (dbContext.Dislikes.Where(x => x.UserId == userId && x.ImageId == imageId).Count() == 0)
                {
                    dbContext.Dislikes.Add(new Dislike() { UserId = userId, ImageId = imageId });
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteDislike(int imageId, int userId)
        {

            using (var dbContext = dbFactory())
            {
                dbContext.Dislikes.RemoveRange(dbContext.Dislikes.Where(x => x.UserId == userId && x.ImageId == imageId));
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
