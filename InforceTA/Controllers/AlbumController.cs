using DB;
using DB.DBModels;
using InforceTA.Models;
using InforceTA.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InforceTA.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : APIControllerBase
    {

        private PhotoGalleryContext dbContext;
        private IAlbumService albumService;
        private IImageService imageService;

        public AlbumController(PhotoGalleryContext dbContext, IAlbumService albumService, IImageService imageService)
        {
            this.dbContext = dbContext;
            this.albumService = albumService;
            this.imageService = imageService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(int index = 0, int count = 5)
        {
            var result = (await albumService.GetAlbumsList(index, count));
            return Ok(result);
        }


        [AllowAnonymous]
        [HttpGet, Route("total")]
        public async Task<IActionResult> GetCount()
        {
            return Ok(await dbContext.Albums.CountAsync());
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}/list")]
        public async Task<IActionResult> Get(int albumId, int index = 0, int count = 5)
        {
            return Ok(await imageService.GetImages(albumId, index, count));
        }


        [AllowAnonymous]
        [HttpGet, Route("{id}/count")]
        public async Task<IActionResult> ImagesCount(int id)
        {
            return Ok(await dbContext.Images.Where(x => x.AlbumId == id).CountAsync());
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}/firstImage")]
        public async Task<IActionResult> FirstImage(int id)
        {
            var result = await dbContext.Images.Where(x => x.AlbumId == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet, Route("getByOwnerId/{id}")]
        public async Task<IActionResult> GetByOwner(int id)
        {
            return Ok(await dbContext.Albums.AsNoTracking().FirstOrDefaultAsync(x => x.OwnerId == id));
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AlbumInput value)
        {

            var result = await albumService.AddAlbum(value, UsersId());
            return Ok(result);
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] int albumId)
        {
            await albumService.DeleteAlbum(albumId, UsersId());
            return Ok();
        }
    }
}
