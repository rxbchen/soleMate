namespace soleMate.Service.API {
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
    using System.Web;

    public class HttpWatchlistRequests {
        public HttpClient client;

        public HttpWatchlistRequests(RestClient restClient) {
            client = restClient.httpClient;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> AddToWatchList(string cUsername, ShoeSearch shoe) {

            bool addedToWatchList = false;

            // Create the payload

            JObject jsonData = new JObject(
                new JProperty("username", cUsername),
                new JProperty("model", shoe.model),
                new JProperty("size", shoe.size),
                new JProperty("priceMin", shoe.low_price),
                new JProperty("priceMax", shoe.high_price)
                );

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

            // Debugging

            Console.WriteLine(content);
            var result = await client.PostAsync("watchlist/add", content);
            if (result.IsSuccessStatusCode) {
                addedToWatchList = true;
            }

            return addedToWatchList;

        }

        public async Task<bool> Delete(string username, string model, float size, float priceMin, float priceMax)
        {

            bool deletedFromWatchlist = false;

            // Create the payload

            JObject jsonData = new JObject(
                new JProperty("username", username),
                new JProperty("model", model),
                new JProperty("size", size),
                new JProperty("priceMin", priceMin),
                new JProperty("priceMax", priceMax)
                );

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

            // Debugging

            Console.WriteLine(content);
            var result = await client.PostAsync("watchlist/delete", content);
            if (result.IsSuccessStatusCode)
            {
                deletedFromWatchlist = true;
            }

            return deletedFromWatchlist;

        }

        public async Task<List<WatchListItem>> GetWatchlist(string cUsername)
        {
            List<WatchListItem> WatchlistList = new List<WatchListItem>();

            // Create Payload
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["username"] = cUsername;
            string queryString = "watchlist?" + query;

            var result = await client.GetAsync(queryString);
            if (result.IsSuccessStatusCode)
            {
                // Since return is "watchlist": [{...}] need to parse first
                var data = await result.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(data);
                IList<JToken> watchlistItems = jsonObject["watchlist"].Children().ToList();

                foreach (JToken watchlistItem in watchlistItems)
                {
                    WatchListItem item = watchlistItem.ToObject<WatchListItem>();
                    WatchlistList.Add(item);
                }
            }
            // Debugging
            Console.WriteLine(WatchlistList); 
            return WatchlistList;
        }
    }
}
