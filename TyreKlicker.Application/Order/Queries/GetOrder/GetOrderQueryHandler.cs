using TyreKlicker.Persistence;

namespace TyreKlicker.Application.Order.Queries.GetOrder
{
    public class GetOrderQueryHandler
    {
        public readonly TyreKlickerDbContext _context;

        public GetOrderQueryHandler(TyreKlickerDbContext context)
        {
            _context = context
        }
    }
}