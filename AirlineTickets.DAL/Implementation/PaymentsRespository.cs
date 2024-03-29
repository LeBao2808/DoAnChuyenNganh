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
    public class PaymentsRespository : GenericRepository<Payments, AirlineTicketsDBContext>, IPaymentsRespository
    {
        public PaymentsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Payments, bool>> predicate)
        {
            return _context.Payments.Where(predicate).Count();
        }
        public IQueryable<Payments> FindByPredicate(Expression<Func<Payments, bool>> predicate)
        {
            return _context.Payments.Where(predicate).AsQueryable();
        }
    }
}
