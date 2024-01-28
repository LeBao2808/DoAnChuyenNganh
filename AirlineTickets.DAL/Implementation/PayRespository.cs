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
    public class PayRespository : GenericRepository<Pay, AirlineTicketsDBContext>, IPayRespository
    {
        public PayRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Pay, bool>> predicate)
        {
            return _context.Pay.Where(predicate).Count();
        }
        public IQueryable<Pay> FindByPredicate(Expression<Func<Pay, bool>> predicate)
        {
            return _context.Pay.Where(predicate).AsQueryable();
        }
    }
}
