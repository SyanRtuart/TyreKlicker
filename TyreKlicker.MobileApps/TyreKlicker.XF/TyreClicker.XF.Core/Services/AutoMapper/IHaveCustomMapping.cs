using AutoMapper;

namespace TyreKlicker.XF.Core.Services.AutoMapper
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}