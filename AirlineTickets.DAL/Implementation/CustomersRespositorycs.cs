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
    public class CustomersRespositorycs : GenericRepository<Customers, AirlineTicketsDBContext>, ICustomersRespository
    {
        public CustomersRespositorycs(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Customers, bool>> predicate)
        {
            return _context.Customers.Where(predicate).Count();
        }
        public IQueryable<Customers> FindByPredicate(Expression<Func<Customers, bool>> predicate)
        {
            return _context.Customers.Where(predicate).AsQueryable();
        }
    }
}
