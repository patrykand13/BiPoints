using BiPoints.Helpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services
{
    public class ApiServices
    {
        private string _url = "http://localhost:24654/api/"; //dev ios url
        public ApiServices()
        {
            // localhost for UWP/iOS or special IP for Android
            if (Device.RuntimePlatform == Device.Android)
            {

                _url = "http://10.0.2.2:24654/api/";

            }
        }

        public async Task<HttpResponseMessage> GetResponse(string path, object model, bool isAuthenticated, string requestType)
        {
            using (HttpClient client = new HttpClient())
            {
                if (isAuthenticated)
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ProfileHelper.Token);
                }

                try
                {
                    string json = "";
                    if (model != null) json = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage ret = new HttpResponseMessage();
                    switch (requestType)
                    {
                        case "post":
                            ret = await client.PostAsync(_url + path, content);
                            break;
                        case "get":
                            ret = await client.GetAsync(_url + path);
                            break;
                        case "put":
                            ret = await client.PutAsync(_url + path, content);
                            break;
                        case "delete":
                            ret = await client.DeleteAsync(_url + path);
                            break;
                    }
                    return ret;
                }
                catch (Exception)
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage();
                    httpResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    return httpResponse;
                }
            }
        }
    }
}
