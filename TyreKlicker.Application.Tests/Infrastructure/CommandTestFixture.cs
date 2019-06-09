using System;
using AutoMapper;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Infrastructure
{
    public class CommandTestFixture : IDisposable
    {
        public CommandTestFixture()
        {
            Context = TyreKlickerContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }

        public TyreKlickerDbContext Context { get; }
        public IMapper Mapper { get; }


        public void Dispose()
        {
            TyreKlickerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("CommandCollection")]
    public class CommandCollection : ICollectionFixture<CommandTestFixture>
    {
    }
}