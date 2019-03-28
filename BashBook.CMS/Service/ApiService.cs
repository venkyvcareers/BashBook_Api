using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BashBook.CMS.Service
{
    public class ApiService
    {
        public async Task<object> Post(string url, object data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var myContent = JsonConvert.SerializeObject(data);
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(myContent, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                }

                return "NA";
            }
        }

        public async Task<object> Get(string url)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"]);

                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage response = await client.GetAsync(url);

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }

                return "NA";
            }
        }
    }
}