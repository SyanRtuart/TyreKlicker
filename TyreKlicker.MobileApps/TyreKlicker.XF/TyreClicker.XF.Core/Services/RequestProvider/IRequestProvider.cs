using System.Threading.Tasks;

namespace TyreKlicker.XF.Core.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task<TResult> PostAsync<TResult, T1>(string uri, T1 data, string header = "");

        Task DeleteAsync(string uri, string token = "");
    }
}