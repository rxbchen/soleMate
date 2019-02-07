using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using soleMate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<SearchResult> GetAllShoes ()
        {
            SearchResult searchResult = new SearchResult();
            // Endpoint
            String urlParam = "shoes";

            // GET request and wait for success
            var response = await client.GetAsync(urlParam);
            if(response.IsSuccessStatusCode)
            {
                // Since return is "shoe": [{...}] need to parse first
                var content = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(content);
                IList<JToken> results = jsonObject["shoes"].Children().ToList();

                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Shoe shoe = result.ToObject<Shoe>();
                    searchResult.Shoes.Add(shoe);
                }
            }

            return searchResult;
        }

    }
}
