using Newtonsoft.Json;
using System.Text;

namespace MinecraftSkinAPI.Models
{
    public class TextureProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Signature { get; set; }
        public TextureObject TextureObject
        {
            get
            {
                if(Value == null)
                {
                    throw new ArgumentException("Texture property does not have Base64 value for texture");
                }
                byte[] data = Convert.FromBase64String(Value);
                string decodedString = Encoding.UTF8.GetString(data);
                TextureObject textureObject = JsonConvert
                    .DeserializeObject<TextureObject>(decodedString);
                return textureObject;
            }
        }
    }
}
