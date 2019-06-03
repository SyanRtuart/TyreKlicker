using System;

namespace TyreKlicker.XF.Core.Models.Order
{
    public class AvailabilityDto
    {
        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }

        //public void CreateMappings(Profile configuration)
        //{
        //    configuration.CreateMap<Availability, AvailabilityDto>()
        //        .ForMember(aDTO => aDTO.DateTime, opt => opt.MapFrom(p => p.Start));
        //}
    }
}