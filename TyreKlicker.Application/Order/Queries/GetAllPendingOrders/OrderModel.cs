using System;
using System.Linq.Expressions;

namespace TyreKlicker.Application.Order.Queries.GetAllPendingOrders
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid AcceptedByUserId { get; set; }

        public string Registration { get; set; }

        //public string Description { get; set; }

        public static Expression<Func<Domain.Entities.Order, OrderModel>> Projection
        {
            get
            {
                return order => new OrderModel
                {
                    //Description = order.Description,
                    OrderId = order.OrderId,
                    Registration = order.Registration,
                    CreatedByUserId = order.CreatedByUserId,
                    AcceptedByUserId = Guid.Empty
                };
            }
        }

        public static OrderModel Create(Domain.Entities.Order order)
        {
            return Projection.Compile().Invoke(order);
        }
    }
}