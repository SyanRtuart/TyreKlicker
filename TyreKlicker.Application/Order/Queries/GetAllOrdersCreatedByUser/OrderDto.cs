using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.Order.Queries.GetAllOrdersCreatedByUser
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid AcceptedByUserId { get; set; }

        public string Registration { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Domain.Entities.Order, OrderDto>> Projection
        {
            get
            {
                return o => new OrderDto
                {
                    Id = o.Id,
                    AcceptedByUserId = o.AcceptedByUserId ?? Guid.Empty,
                    Description = o.Description ?? string.Empty,
                    Registration = o.Registration ?? string.Empty
                };
            }
        }

        public static OrderDto Create(Domain.Entities.Order order)
        {
            return Projection.Compile().Invoke(order);
        }
    }
}