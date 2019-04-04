using MediatR;
using System;

namespace TyreKlicker.Application.Address.Query.GetAddress
{
    public class GetAddressQuery : IRequest<AddressDto>
    {
        public Guid Id { get; set; }
    }
}