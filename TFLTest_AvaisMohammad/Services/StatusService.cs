using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TFLTest_AvaisMohammad.Models;

namespace TFLTest_AvaisMohammad.Services
{
    public abstract class StatusService
    {
        protected readonly HttpClient _httpClient;
        public StatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<T> Get<T>(string uri)
        {
            return JsonConvert.DeserializeObject<T>(await _httpClient.GetStringAsync(uri));
        }

        protected async Task<HttpResponseMessage> Get(string uri)
        {
            HttpResponseMessage result = null;
            try
            {
                result = await _httpClient.GetAsync(uri);
                return result;
            }
            catch (Exception ex) when (ex is ArgumentNullException || ex is InvalidOperationException)
            {
                throw new TflException("An error occurred calling the api", ex);
            }
            //catch (HttpRequestException)
            //{
            //    return result;
            //}
        } 
    }
}
