using Data.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Resources
{
    public class SaglikcimApiService 
    {
        public HttpClient Client = new HttpClient()
        {
            Timeout = new TimeSpan(0, 0, 360),
            BaseAddress = new Uri("http://localhost:51244/api/")
        };
        public string token = "";
        public SaglikcimApiService()
        {

        }
        public SaglikcimApiService(string token)
        {
            Client.Timeout = new TimeSpan(0, 0, 360);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.BaseAddress = new Uri("http://localhost:51244/api/");
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }


        public string Login(string email,string password)
        {
            Client.Timeout = new TimeSpan(0, 0, 360);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.BaseAddress = new Uri("http://localhost:51244/api/");
            var response = RequestPost<IDictionary<string, string>>("Login/Login", new { email = email, password = password });
            if (response != null && !String.IsNullOrEmpty(response.Last().Value))
            {
              
                token = response.Last().Value;
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                return token;
            }
            return null;
        }
        public T RequestGet<T>(string path) where T : class
        {
            HttpResponseMessage response = Client.GetAsync(path).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(((Newtonsoft.Json.Linq.JContainer)((Newtonsoft.Json.Linq.JContainer)(JsonConvert.DeserializeObject<object>(responseString))).Last).First.ToString());
        }
        public T RequestPost<T>(string path, object values = null) where T : class
        {
            try
            {
                string json = JsonConvert.SerializeObject(values, Formatting.Indented);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = Client.PostAsync(path, byteContent).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(((Newtonsoft.Json.Linq.JContainer)((Newtonsoft.Json.Linq.JContainer)(JsonConvert.DeserializeObject<object>(responseString))).Last).First.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
  
}
