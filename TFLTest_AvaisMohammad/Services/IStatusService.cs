using System.Threading.Tasks;
using TFLTest_AvaisMohammad.Models;

namespace TFLTest_AvaisMohammad.Services
{
    public interface IStatusService
    {
        /// <summary>
        /// Return Status of the specified entity (Road, Line, etc)
        /// </summary>
        /// <param name="serviceName">name of the service, roadname, tubename, etc</param>
        /// <returns><see cref="Status"/>Concrete object extending Status</returns>
        Task<Status> GetStatus(string serviceName);
    }
}
