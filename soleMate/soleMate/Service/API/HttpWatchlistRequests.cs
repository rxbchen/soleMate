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
    public class HttpWatchlistRequests
    {
        public HttpClient client;

        public HttpWatchlistRequests(RestClient restClient)
        {
            client = restClient.httpClient;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
