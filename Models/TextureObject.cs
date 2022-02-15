namespace MinecraftSkinAPI.Models
{
    public class TextureObject
    {
        public long Timestamp { get; set; }
        public string ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string SignatureRequired { get; set; }
        public TextureData Textures { get; set; }
        public string SkinURL
        {
            get
            {
                if(Textures.Skin != null)
                {
                    return Textures.Skin.URL;
                }
                throw new ArgumentException("Could not get skin texture URL");
            }
        }
    }
}
