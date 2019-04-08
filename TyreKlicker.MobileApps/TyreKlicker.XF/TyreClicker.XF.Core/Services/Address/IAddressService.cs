using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TyreKlicker.XF.Core.Models.Address;

namespace TyreKlicker.XF.Core.Services.Address
{
    public interface IAddressService
    {
        Task<CreateAddressCommand> CreateAddress(string token, CreateAddressCommand command);

        Task<Models.Address.Address> GetAddressAsync(string token, Guid addressId);

        Task<Models.Address.Address> GetPrimaryAddressAsync(string token, Guid userId);

        Task<ObservableCollection<Models.Address.Address>> GetAddressesAsync(string token, Guid userId);
    }
}