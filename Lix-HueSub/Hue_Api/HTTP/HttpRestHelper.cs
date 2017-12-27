using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hue.HTTP
{
    /// <summary>
    /// Send Async REST Json Requests
    /// </summary>
    internal static class HttpRestHelper
    {
        public static async Task<string> Post(string url, string body)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"))
                                  .ContinueWith(response => response.Result.Content.ReadAsStringAsync());
                string responseFromServer = result.Result;
                return responseFromServer;
            }
        }

        public static bool Put(string url, string body)
        {
            using (var client = new HttpClient())
            {
                var result = client.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json")).Result;
                if (result.IsSuccessStatusCode)
                {
                    var r = result.Content.ReadAsStringAsync().Result;
                    return !r.Contains("error");
                }
            }
            return false;
        }
    }
}