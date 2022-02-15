using ImageMagick;
namespace MinecraftSkinAPI
{
    public class HeadCropper
    {
        private static readonly int HEAD_SIZE = 100;
        private readonly HttpClient _httpClient;
        public HeadCropper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public MemoryStream cropHead(Stream playerSkinStream)
        {
            MagickImage image = new MagickImage(playerSkinStream);
            MagickGeometry geometry = new MagickGeometry();
            geometry.X = 8;
            geometry.Y = 8;
            geometry.Width = 8;
            geometry.Height = 8;
            image.Crop(geometry);
            image.Scale(HEAD_SIZE, HEAD_SIZE);
            image.Format = MagickFormat.Png;
            MemoryStream headStream = new MemoryStream();
            image.Write(headStream);
            return headStream;
        }
    }
}
