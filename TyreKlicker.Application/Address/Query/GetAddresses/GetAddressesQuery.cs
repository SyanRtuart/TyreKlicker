using MediatR;
using System;

namespace TyreKlicker.Application.Address.Query.GetAddresses
{
    public class GetAddressesQuery : IRequest<AddressListViewModel>
    {
        public Guid UserId { get; set; }
    }
}