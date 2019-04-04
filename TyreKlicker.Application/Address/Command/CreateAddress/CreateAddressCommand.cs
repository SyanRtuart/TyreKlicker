using MediatR;
using System;

namespace TyreKlicker.Application.Address.Command.CreateAddress
{
    public class CreateAddressCommand : IRequest
    {
        public Guid UserId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string PhoneNumber { get; set; }

        public bool PrimaryAddress { get; set; }
    }
}