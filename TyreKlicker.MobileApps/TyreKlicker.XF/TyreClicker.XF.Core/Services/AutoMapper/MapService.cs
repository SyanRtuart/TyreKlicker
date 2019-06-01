using AutoMapper;
using TyreKlicker.XF.Core.Infrastructure.AutoMapper;

namespace TyreKlicker.XF.Core.Services.AutoMapper
{
    public static class MapService
    {
        public static IMapper ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<AutoMapperProfile>(); });
            //config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}