using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Tyre;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.Tyre
{
    public class TyreService : ITyreService
    {
        private const string BaseUri = @"https://api.wheel-size.com/v1/";
        private const string Key = "6cbe0b7035ba71026abe2ca8dae889af";
        private readonly IRequestProvider _requestProvider;

        public TyreService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<Make>> GetMakes()
        {
            var values = new NameValueCollection
            {
                {"user_key", Key}
            };

            var uri = UriHelper.BuildUri(BaseUri + "makes/", values);

            var response = await _requestProvider.GetAsync<ObservableCollection<Make>>(uri);

            return response;
        }

        public async Task<ObservableCollection<Model>> GetModels(Make make)
        {
            var values = new NameValueCollection
            {
                {"user_key", Key},
                {"make", make.Name}
            };

            var uri = UriHelper.BuildUri(BaseUri + "models/", values);

            var response = await _requestProvider.GetAsync<ObservableCollection<Model>>(uri);

            return response;
        }

        public async Task<ObservableCollection<Years>> GetYears(Make make)
        {
            var values = new NameValueCollection
            {
                {"user_key", Key},
                {"make", make.Name}
            };

            var uri = UriHelper.BuildUri(BaseUri + "years/", values);

            var response = await _requestProvider.GetAsync<ObservableCollection<Years>>(uri);

            return response;
        }

        public async Task<ObservableCollection<ApiVehicle>> GetVehicles(Make make, Model model, Years year)
        {
            var values = new NameValueCollection
            {
                {"user_key", Key},
                {"make", make.Name},
                {"model", model.Name},
                {"year", year.Name}
            };

            var uri = UriHelper.BuildUri(BaseUri + "search/by_model/", values);

            var response = await _requestProvider.GetAsync<ObservableCollection<ApiVehicle>>(uri);

            return response;
        }

        public async Task<ObservableCollection<WheelPair>> GetWheelPairs(Make make, Model model, Years year)
        {
            var values = new NameValueCollection
            {
                {"user_key", Key},
                {"make", make.Name},
                {"model", model.Name},
                {"year", year.Name}
            };

            var uri = UriHelper.BuildUri(BaseUri + "search/by_model/", values);

            var response = await _requestProvider.GetAsync<ObservableCollection<ApiVehicle>>(uri);

            var myWheelPairs = new ObservableCollection<WheelPair>();

            foreach (var vehicle in response)
            foreach (var wheel in vehicle.Wheels)
                myWheelPairs.Add(wheel);

            return myWheelPairs;
        }
    }
}