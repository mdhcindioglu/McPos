using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace McPos.Shared.Extenstions
{
    public static class HttpClientExtenstions
    {
        public static async Task<T> GetDataFromUrlAsync<T>(this HttpClient _httpClient, string url)
        {
            var res = await _httpClient.GetAsync(url);
            string json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
        public static async Task<T> PostDataFromUrlAsync<T>(this HttpClient _httpClient, string url, object o)
        {
            var res = await _httpClient.PostAsJsonAsync(url, o);
            string json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
        public static async Task<T> PutDataFromUrlAsync<T>(this HttpClient _httpClient, string url, object o)
        {
            var res = await _httpClient.PutAsJsonAsync(url, o);
            string json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
        public static async Task<T> DeleteDataFromUrlAsync<T>(this HttpClient _httpClient, string url)
        {
            var res = await _httpClient.DeleteAsync(url);
            string json = await res.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
