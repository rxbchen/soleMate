﻿using Newtonsoft.Json;
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

        public async Task<SearchResult> GetAllShoes()
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
                    searchResult.ShoeList.Add(shoe);
                }
            }

            return searchResult;
        }

        public async Task<List<string>> GetSupportedShoes()
        {
            List<string> ModelList = new List<string>();

            // Endpoint
            //String urlParam = "supportedShoes";

            // GET request and wait for success
            var response = await client.GetAsync("supportedShoes");
            if (response.IsSuccessStatusCode)
            {
                // Since return is "shoe": [{...}] need to parse first
                var content = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(content);
                JEnumerable<JToken> shoes = jsonObject["shoes"].Children();
                IList<JToken> shoeModels = shoes["model"].ToList();

                foreach (JToken result in shoeModels)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    // Remove + signs with space
                    string model = result.ToString().Replace("+", " ");
                    ModelList.Add(model);
                }
            }

            return ModelList;
        }

    }
}
