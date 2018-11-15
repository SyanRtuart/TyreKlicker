using System;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using TyreKlicker.Application.Exceptions;
using TyreKlicker.Application.Tests.Infrastructure;
using TyreKlicker.Application.User.Queries.GetUserByEmail;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.User.Command.CreateUser
{
    [Collection("QueryCollection")]

    public class CreateUserCommandHandlerTests
    {
        private readonly TyreKlickerDbContext _context;


        public GetUserByEmailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

    }
}
