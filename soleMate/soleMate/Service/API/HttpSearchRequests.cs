using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace soleMate.Service.API
{
    public class HttpSearchRequests
    {
        public HttpClient client;

        public HttpSearchRequests(RestClient restClient)
        {
            client = restClient.httpClient;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<String> GetAllShoes ()
        {
            String urlParam = "/shoes";
            var response = await client.GetAsync(urlParam);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }
            return "Error!";
        }

    }
}
