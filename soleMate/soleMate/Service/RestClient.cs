using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace soleMate.Service
{
    public class RestClient
    {
        public HttpClient httpClient;

        public RestClient(String baseUrl)
        {
            httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000,
                BaseAddress = new Uri(baseUrl)
            };
        }
    }
}
