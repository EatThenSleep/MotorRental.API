using MotorRental.Consuming.Models;
using MotorRental.Consuming.Services.IServices;
using MotorRental.Utilities;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using static MotorRental.Utilities.SD;
using Newtonsoft.Json;

namespace MotorRental.Consuming.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this._httpClient = httpClient;

        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                // 1. Process Request
                var client = _httpClient.CreateClient("FlcAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                if (apiRequest.ContentType == ContentType.MultipartFormData)
                {
                    message.Headers.Add("Accept", "*/*");
                }
                else
                {
                    message.Headers.Add("Accept", "application/json");
                }

                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.ContentType == ContentType.MultipartFormData)
                {
                    var content = new MultipartFormDataContent();

                    foreach (var prop in apiRequest.Data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(apiRequest.Data);
                        if (value is FormFile)
                        {
                            var file = (FormFile)value;
                            if (file != null)
                            {
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                            }
                        }
                        else
                        {
                            content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                        }
                    }
                    message.Content = content;
                }
                else
                {
                    // Data will not be null in POST/PUT Http calls
                    if (apiRequest.Data != null)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                                            Encoding.UTF8, "application/json");
                    }
                }


                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // 2. Process Response
                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse = await client.SendAsync(message);

                // extract the API result 
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    // convert to instance of class T
                    // process error message
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (ApiResponse != null
                        && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                         || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception e)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var APIresponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIresponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
