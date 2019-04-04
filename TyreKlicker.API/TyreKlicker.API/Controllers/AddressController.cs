using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TyreKlicker.API.Exceptions;
using TyreKlicker.Application.Address.Command.CreateAddress;
using TyreKlicker.Application.Address.Query.GetAddress;
using TyreKlicker.Application.Address.Query.GetAddresses;
using AddressDto = TyreKlicker.Application.Address.Query.GetAddress.AddressDto;

namespace TyreKlicker.API.Controllers
{
    [ApiController]
    public class AddressController : BaseController
    {
        [HttpGet]
        public async Task<AddressDto> Address(Guid id)
        {
            return await Mediator.Send(new GetAddressQuery() { Id = id });
        }

        [HttpGet]
        public async Task<AddressListViewModel> Addresses(Guid userId)
        {
            return await Mediator.Send(new GetAddressesQuery() { UserId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> Address(CreateAddressCommand command)
        {
            if (command == null) throw new BadRequestException("BAD_REQUEST", @"Bad Request");

            return Ok(await Mediator.Send(command));
        }
    }
}