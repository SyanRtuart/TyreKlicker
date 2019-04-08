using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TyreKlicker.API.Exceptions;
using TyreKlicker.Application.Address.Command.CreateAddress;
using TyreKlicker.Application.Address.Query.GetAddress;
using TyreKlicker.Application.Address.Query.GetAddresses;
using TyreKlicker.Application.Address.Query.GetPrimaryAddress;
using AddressDto = TyreKlicker.Application.Address.Query.GetAddress.AddressDto;

namespace TyreKlicker.API.Controllers
{
    [ApiController]
    [Route("~/api/[controller]/")]
    public class AddressController : BaseController
    {
        [HttpGet, Route("{id:guid}")]
        public async Task<AddressDto> Address([FromRoute] Guid id)
        {
            return await Mediator.Send(new GetAddressQuery() { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Address(CreateAddressCommand command)
        {
            if (command == null) throw new BadRequestException("BAD_REQUEST", @"Bad Request");

            return Ok(await Mediator.Send(command));
        }

        [HttpGet, Route("{userId:guid}/[action]")]
        public async Task<AddressListViewModel> Addresses([FromRoute] Guid userId)
        {
            return await Mediator.Send(new GetAddressesQuery() { UserId = userId });
        }

        [HttpGet, Route("{userId:guid}/[action]")]
        public async Task<Application.Address.Query.GetPrimaryAddress.AddressDto> PrimaryAddress([FromRoute] Guid userId)
        {
            return await Mediator.Send(new GetPrimaryAddressQuery() { UserId = userId });
        }
    }
}