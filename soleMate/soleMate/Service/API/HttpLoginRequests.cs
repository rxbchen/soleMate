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

namespace soleMate.Service.API
{
    public class HttpLoginRequests
    {
        public HttpClient client;

        public HttpLoginRequests(RestClient restClient)
        {
            client = restClient.httpClient;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // This might change depending what we want to get back from the backend
        public async Task<bool> Login(string cUsername, string cPassword)
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
