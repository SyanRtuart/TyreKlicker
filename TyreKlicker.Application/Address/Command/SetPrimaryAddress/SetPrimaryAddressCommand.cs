using MediatR;
using System;

namespace TyreKlicker.Application.Address.Command.SetPrimaryAddress
{
    public class SetPrimaryAddressCommand : IRequest
    {
        public Guid Id { get; set; }

        public bool IsPrimary { get; set; }
    }
}