using AutoMapper;
using TyreKlicker.XF.Core.Services.AutoMapper;
using TyreKlicker.XF.Core.Validators;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class Vehicle : IHaveCustomMapping
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Trim { get; set; }

        public string Tyre { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Vehicle, ValidatableObject<Vehicle>>();
            ;
        }
    }
}