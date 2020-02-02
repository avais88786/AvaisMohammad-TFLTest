using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TFLTest_AvaisMohammad.Models;

namespace TFLTest_AvaisMohammad.Services
{
    public class RoadStatusService : StatusService, IStatusService
    {
        private readonly IConfiguration _configuration;
        public RoadStatusService(IConfiguration configuration, HttpClient httpClient) : base(httpClient)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Return Status of the specified Road
        /// </summary>
        /// <param name="roadName">Road Name</param>
        /// <returns><see cref="RoadStatus"/>Concrete object</returns>
        public async Task<Status> GetStatus(string roadName)
        {
            string uri = $"{_configuration["apiEndpointBase"]}/Road/{roadName}?app_id={_configuration["appId"]}&app_key={_configuration["appKey"]}";

            var response = await Get(uri);

            if (response.IsSuccessStatusCode)
            {
                var status = JsonConvert.DeserializeObject<List<RoadStatus>>(response.Content.ReadAsStringAsync().Result)
                                    .First();
                status.Found = true;
                return status;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var notFoundObj = response.Content != null ? JsonConvert.DeserializeObject<TflException>(await response.Content.ReadAsStringAsync())
                                                                : new TflException();
                notFoundObj.Message = $"{roadName} is not a valid road";
                // log the exception -> ILogger..
                return new RoadStatus() { Found = false, FriendlyMessage = notFoundObj.Message };
            }

            return new RoadStatus() { Found = false, FriendlyMessage = $"Unable to get status of Road {roadName}. Error Code : {response.StatusCode}"};
        }
    }
}
