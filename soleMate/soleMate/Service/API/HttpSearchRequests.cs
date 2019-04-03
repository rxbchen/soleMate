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
using System.Web;

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

        public async Task<SearchResult> GetShoes(ShoeSearch shoeSearch)
        {
            SearchResult searchResult = new SearchResult();

            if (shoeSearch == null) {
                throw new Exception("ShoeSearch not found");
            }

            // Create Payload
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["model"] = shoeSearch.model;
            query["size"] = shoeSearch.size.ToString();
            query["priceMin"] = shoeSearch.low_price.ToString();
            query["priceMax"] = shoeSearch.high_price.ToString();
            query["sortLowHigh"] = shoeSearch.sortLowToHigh.ToString();

            string queryString = "shoes?"+ query;
            Console.WriteLine("queryString");
            Console.WriteLine(queryString);

            // GET request and wait for success
            var response = await client.GetAsync(queryString);

            if (response.IsSuccessStatusCode)
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

        //TODO: Actually implement properly
        //public async Task<List<string>> GetWishList()
        //{
        //    List<string> WishList = new List<string>();

        //    // Endpoint
        //    //String urlParam = "supportedShoes";

        //    // GET request and wait for success
        //    var response = await client.GetAsync("wishList");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Since return is "shoe": [{...}] need to parse first
        //        var content = await response.Content.ReadAsStringAsync();
        //        JObject jsonObject = JObject.Parse(content);
        //        JEnumerable<JToken> shoes = jsonObject["shoes"].Children();
        //        IList<JToken> shoeModels = shoes["model"].ToList();

        //        foreach (JToken result in shoeModels)
        //        {
        //            // JToken.ToObject is a helper method that uses JsonSerializer internally
        //            // Remove + signs with space
        //            string model = result.ToString().Replace("+", " ");
        //            WishList.Add(model);
        //        }
        //    }

        //    return WishList;
        //}

        public async Task<bool> AddToWatchList(string cUsername, string cPassword)
        {
            bool isAuth = false;
            // Create the payload
            JObject jsonData = new JObject(
                new JProperty("username", cUsername),
                new JProperty("password", cPassword));
            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
            // Debugging
            Console.WriteLine(content);
            var result = await client.PostAsync("login", content);
            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine(result.IsSuccessStatusCode);
                var response = await result.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(response);
                isAuth = (bool)jsonObject["auth"];
            }
            return isAuth;

        }
    }
}
