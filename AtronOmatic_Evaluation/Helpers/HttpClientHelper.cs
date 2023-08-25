using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtronOmatic_Evaluation.Helpers;
public class HttpClientHelper
{
    private static readonly HttpClient HttpClient;

    static HttpClientHelper()
    {
        HttpClient = new HttpClient();
    }

    static async Task<string> GetJsonFromUrl(string url)
    {
        var response = await HttpClient.GetAsync(url);

        return await response.Content.ReadAsStringAsync();
    }
}
