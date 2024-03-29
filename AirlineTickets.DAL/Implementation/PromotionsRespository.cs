using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Models.Context;
using AirlineTickets.DAL.Models.Entity;
using Maynghien.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Implementation
{
    public class PromotionsRespository : GenericRepository<Promotions, AirlineTicketsDBContext>, IPromotionsRespository
    {
        public PromotionsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Promotions, bool>> predicate)
        {
            return _context.Promotions.Where(predicate).Count();
        }
        public IQueryable<Promotions> FindByPredicate(Expression<Func<Promotions, bool>> predicate)
        {
            return _context.Promotions.Where(predicate).AsQueryable();
        }
    }
}
