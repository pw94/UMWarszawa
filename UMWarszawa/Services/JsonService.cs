using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UMWarszawa.Interfaces;

namespace UMWarszawa.Services
{
    public class JsonService : IHttpService
    {
        public async Task<T> GetObjectAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}