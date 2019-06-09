using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using TyreKlicker.Application.Address.Command.CreateAddress;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Address.Command.CreateAddress
{
    [Collection("CommandCollection")]
    public class CreateAddressCommandHandlerTests
    {
        public CreateAddressCommandHandlerTests(CommandTestFixture fixture)
        {
            _context = fixture.Context;
        }

        private readonly TyreKlickerDbContext _context;

        [Fact]
        public async Task CreateAddress_UserAlreadyHasAPrimaryAddress_ShouldSetCurrentPrimaryAddressToFalse()
        {
            var sut = new CreateAddressCommandHandler(_context);
            var currentUser = _context.User.First();
            var addressCountBeforeAct = _context.Address.ToList().Count;
            var oldPrimaryAddress =
                _context.Address.First(x => x.UserId == currentUser.Id && x.PrimaryAddress);

            var command = new CreateAddressCommand {City = "Glasgow", UserId = currentUser.Id, PrimaryAddress = true};
            await sut.Handle(command, CancellationToken.None);

            oldPrimaryAddress.PrimaryAddress.ShouldBe(false);
            _context.Address.First(x => x.UserId == currentUser.Id && x.PrimaryAddress).City.ShouldBe("Glasgow");
            _context.Address.Count().ShouldBe(addressCountBeforeAct + 1);
        }

        [Fact]
        public async Task CreateAddress_UserDoesNotExist_ShouldThrowException()
        {
            var sut = new CreateAddressCommandHandler(_context);

            await sut.Handle(new CreateAddressCommand {UserId = Guid.NewGuid()}, CancellationToken.None)
                .ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateAddress_WhenCalled_ShouldCreateAddress()
        {
            var sut = new CreateAddressCommandHandler(_context);
            var addressCountBeforeAct = _context.Address.ToList().Count;
            var currentUser = _context.User.First();

            await sut.Handle(
                new CreateAddressCommand
                {
                    UserId = currentUser.Id, PrimaryAddress = false, City = "Glasgow", PhoneNumber = "0141 000 0000",
                    Postcode = "G55 5RR", Street = "Main Street"
                }, CancellationToken.None);
            var addressCountAfterAct = _context.Address.ToList().Count;

            addressCountAfterAct.ShouldBe(addressCountBeforeAct + 1);
        }
    }
}