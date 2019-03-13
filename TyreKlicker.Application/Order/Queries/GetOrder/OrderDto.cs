using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? AcceptedByUserId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Registration { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Trim { get; set; }

        public string Tyre { get; set; }

        public static Expression<Func<Domain.Entities.Order, OrderDto>> Projection
        {
            get
            {
                return o => new OrderDto
                {
                    Id = o.Id,
                    CreatedByUserId = o.CreatedByUserId,
                    AcceptedByUserId = o.AcceptedByUserId ?? Guid.Empty,
                    Make = o.Make,
                    Model = o.Model,
                    CreatedDate = o.CreatedDate,
                    Registration = o.Registration ?? string.Empty,
                    Trim = o.Trim,
                    Year = o.Year,
                    Tyre = o.Tyre
                };
            }
        }

        public static OrderDto Create(Domain.Entities.Order order)
        {
            return Projection.Compile().Invoke(order);
        }
    }
}