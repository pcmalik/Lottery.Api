using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lottery.UI
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetContentAs<T>(this HttpResponseMessage httpResponseMessage) where T : class
        {
            var contentString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(contentString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}