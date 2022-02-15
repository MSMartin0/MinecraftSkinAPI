using MinecraftSkinAPI.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace MinecraftSkinAPI
{
    public class SkinFetcher
    {
        private readonly HttpClient _httpClient;
        private static readonly string UUID_LINK = "https://api.mojang.com/users/profiles/minecraft/{0}";
        private static readonly string PROFILE_LINK = "https://sessionserver.mojang.com/session/minecraft/profile/{0}";
        public SkinFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string getPlayerUUID(string playerName)
        {
            string playerUUIDLink = string.Format(UUID_LINK, playerName);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, playerUUIDLink);
            HttpResponseMessage response = _httpClient.Send(message);
            int statusCode = ((int)response.StatusCode);
            if (statusCode == StatusCodes.Status204NoContent)
            {
                throw new ArgumentException("Player not found");
            }
            Stream responseStream = response.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string content = streamReader.ReadToEnd();
            streamReader.Dispose();
            UUIDResponse responseObject = JsonConvert
                .DeserializeObject<UUIDResponse>(content);
            return responseObject.ID;
        }
        public string getPlayerSkinLink(string playerUUID)
        {
            string playerProfileLink = string.Format(PROFILE_LINK, playerUUID);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, playerProfileLink);
            HttpResponseMessage response = _httpClient.Send(message);
            Stream responseStream = response.Content.ReadAsStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string content = streamReader.ReadToEnd();
            streamReader.Dispose();
            TextureResponse textureResponse = JsonConvert
                .DeserializeObject<TextureResponse>(content);
            if(textureResponse.Properties.Count <= 0)
            {
                throw new ArgumentException("Could not read player texture property");
            }
            TextureProperty textureProperty = textureResponse.Properties[0];
            TextureObject textureObject = textureProperty.TextureObject;
            return textureObject.SkinURL;
        }
        public Stream getPlayerSkinStream(string playerUUID)
        {
            string playerSkinLink = getPlayerSkinLink(playerUUID);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, playerSkinLink);
            HttpResponseMessage response = _httpClient.Send(request);
            return response.Content.ReadAsStream();
        }
    }
}
