using System;
using AutoMapper;
using TyreKlicker.Persistence;
using Xunit;

namespace TyreKlicker.Application.Tests.Infrastructure
{
    public class QueryTestFixture : IDisposable
    {
        public TyreKlickerDbContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = TyreKlickerContextFactory.Create();
            Mapper = AutoMapperFactory.Create();

        }

        public IMapper Mapper { get; }


        public void Dispose()
        {
            TyreKlickerContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}