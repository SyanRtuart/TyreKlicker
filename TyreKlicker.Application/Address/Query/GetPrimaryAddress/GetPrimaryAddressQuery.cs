using MediatR;
using System;

namespace TyreKlicker.Application.Address.Query.GetPrimaryAddress
{
    public class GetPrimaryAddressQuery : IRequest<AddressDto>
    {
        public Guid UserId { get; set; }
    }
}