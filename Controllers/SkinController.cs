using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinecraftSkinAPI.Controllers
{
    [ApiController]
    [Route("skin")]
    public class SkinController : ControllerBase
    {
        private readonly SkinFetcher skinFetcher;
        private readonly HeadCropper headCropper;
        public SkinController(SkinFetcher skinFetcher, HeadCropper headCropper)
        {
            this.headCropper = headCropper;
            this.skinFetcher = skinFetcher;
        }
        [HttpGet("{playerName}/head")]
        public IActionResult GetHead(string playerName)
        {
            try
            {
                string playerUUID = skinFetcher
                    .getPlayerUUID(playerName);
                Stream playerSkinStream = skinFetcher
                    .getPlayerSkinStream(playerUUID);
                MemoryStream headStream = headCropper
                    .cropHead(playerSkinStream);
                playerSkinStream.Dispose();
                return File(headStream.ToArray(), "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{playerName}")]
        public IActionResult GetSkin(string playerName)
        {
            try
            {
                string playerUUID = skinFetcher
                    .getPlayerUUID(playerName);
                Stream playerSkinStream = skinFetcher
                    .getPlayerSkinStream(playerUUID);
                MemoryStream skinMemoryStream = new MemoryStream();
                playerSkinStream.CopyTo(skinMemoryStream);
                playerSkinStream.Dispose();
                return File(skinMemoryStream.ToArray(), "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
