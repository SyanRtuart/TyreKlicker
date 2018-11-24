using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.Order.Queries.GetAllOrders
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }

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
                    OrderId = o.OrderId,
                    CreatedByUserId = o.CreatedByUserId,
                    AcceptedByUserId = o.AcceptedByUserId.Value,
                    Description = o.Description != null
                        ? o.Description : string.Empty,
                    Registration = o.Registration != null
                        ? o.Registration : string.Empty
                };
            }
        }

        public static OrderDto Create(Domain.Entities.Order order)
        {
            return Projection.Compile().Invoke(order);
        }
    }
}