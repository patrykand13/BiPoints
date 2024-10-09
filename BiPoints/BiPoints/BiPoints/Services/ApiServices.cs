using BiPoints.Helpers;
using BiPoints.Interfaces.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BiPoints.Services
{
    public class ApiServices
    {
        private readonly IAlertNotificationServices _notificationServices;
        private string _url = "http://192.168.100.10:45455/api/";
        public ApiServices()
        {
            _notificationServices = DependencyService.Get<IAlertNotificationServices>();
        }
        public async Task<HttpResponseMessage> GetResponseAsync(string path, string requestType, object model = null, bool isAuthenticated = true)
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
        public async Task<string> CheckApiResponseAsync(HttpResponseMessage response, bool sendNotification = true)
        {
            try
            {

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (sendNotification)
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.NoContent:
                            await _notificationServices.AlertNotificationPopup("No content");
                            break;
                        case System.Net.HttpStatusCode.BadRequest:
                            var jsonString = await response.Content.ReadAsStringAsync();
                            JObject jsonObject = JObject.Parse(jsonString);
                            await _notificationServices.AlertNotificationPopup(jsonObject["message"].ToString());
                            break;
                        case System.Net.HttpStatusCode.Unauthorized:
                            await _notificationServices.AlertNotificationPopup("Unauthorized access");
                            break;
                        default:
                            await _notificationServices.AlertNotificationPopup("No connection to the server");
                            break;
                    }
                }
                return null;
            }
            catch
            {
                await _notificationServices.AlertNotificationPopup("No connection to the server");
                return null;
            }
        }
    }
}
