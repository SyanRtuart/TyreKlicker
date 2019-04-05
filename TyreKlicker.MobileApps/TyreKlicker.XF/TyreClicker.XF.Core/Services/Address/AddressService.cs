using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Exceptions;
using TyreKlicker.XF.Core.Extensions;
using TyreKlicker.XF.Core.Helpers;
using TyreKlicker.XF.Core.Models.Address;
using TyreKlicker.XF.Core.Services.RequestProvider;

namespace TyreKlicker.XF.Core.Services.Address
{
    public class AddressService : IAddressService
    {
        private readonly IRequestProvider _requestProvider;

        public AddressService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<CreateAddressCommand> CreateAddress(string token, CreateAddressCommand command)
        {
            var result = await _requestProvider.PostAsync(GlobalSetting.Instance.AddressEndPoint, command,
                Settings.AccessToken);

            return result;
        }

        public async Task<Models.Address.Address> GetAddressAsync(string token, Guid addressId)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.AddressEndPoint + $"/{addressId}");

            Models.Address.Address address;

            try
            {
                address = await _requestProvider.GetAsync<Models.Address.Address>(uri, Settings.AccessToken);
            }
            catch (HttpResponseEx ex) when (ex.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                address = null;
            }

            return address;
        }

        public async Task<ObservableCollection<Models.Address.Address>> GetAddressesAsync(string token, Guid userId)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.AddressEndPoint + $"/{userId}" + "/addresses");

            var addresses = await _requestProvider.GetAsync<AddressList>(uri, Settings.AccessToken);

            if (addresses?.Addresses != null)
            {
                return addresses?.Addresses.ToObservableCollection();
            }

            return new ObservableCollection<Models.Address.Address>();
        }
    }
}