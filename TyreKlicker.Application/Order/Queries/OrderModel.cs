using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.Order.Queries
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Registration { get; set; }
        public string Description { get; set; }

        public static Expression<Func<Domain.Entities.Order, OrderModel>> Projection
        {
            get
            {
                return order => new OrderModel
                {
                    Description = order.Description,
                    OrderId = order.OrderId,
                    Registration = order.Registration,
                    UserId = order.UserId
                };
            }
        }

        public static OrderModel Create(Domain.Entities.Order order)
        {
            return Projection.Compile().Invoke(order);
        }
    }
}