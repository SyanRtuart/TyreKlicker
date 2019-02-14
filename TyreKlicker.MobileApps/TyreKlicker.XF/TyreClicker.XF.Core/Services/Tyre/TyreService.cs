using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Tyre;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.Tyre
{
    public class TyreService : ITyreService
    {
        private readonly IRequestProvider _requestProvider;

        private const string BaseUri = @"https://api.wheel-size.com/v1/";
        private const string Key = "6cbe0b7035ba71026abe2ca8dae889af"; 

        public TyreService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IEnumerable<Make>> GetMakes()
        {
            var values = new NameValueCollection
            {
                {"key", Key }
            };

            var uri = UriHelper.BuildUri(BaseUri + "make/" , values);

            var response = await _requestProvider.GetAsync<IEnumerable<Make>>(uri);

            return response;
        }

        public async Task<IEnumerable<Model>> GetModels(Make make)
        {
            var values = new NameValueCollection
            {
                {"key", Key }
            };

            var uri = UriHelper.BuildUri(BaseUri + "models/" , values);

            var response = await _requestProvider.GetAsync<IEnumerable<Model>>(uri);

            return response;
        }

        public async Task<IEnumerable<Years>> GetYears(Make make)
        {
            var values = new NameValueCollection
            {
                {"key", Key }
            };

            var uri = UriHelper.BuildUri(BaseUri + "years/", values);

            var response = await _requestProvider.GetAsync<IEnumerable<Years>>(uri);

            return response;
        }
    }
}
