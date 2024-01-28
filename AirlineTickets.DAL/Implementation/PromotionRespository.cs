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
    public class PromotionRespository : GenericRepository<Promotion, AirlineTicketsDBContext>, IPromotionRespository
    {
        public PromotionRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Promotion, bool>> predicate)
        {
            return _context.Promotion.Where(predicate).Count();
        }
        public IQueryable<Promotion> FindByPredicate(Expression<Func<Promotion, bool>> predicate)
        {
            return _context.Promotion.Where(predicate).AsQueryable();
        }
    }
}
