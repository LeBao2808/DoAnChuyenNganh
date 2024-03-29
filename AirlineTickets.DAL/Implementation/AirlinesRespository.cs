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
    public class AirlinesRespository : GenericRepository<Airlines, AirlineTicketsDBContext>, IAirlinesRespository
    {
        public AirlinesRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Airlines, bool>> predicate)
        {
            return _context.Airlines.Where(predicate).Count();
        }
        public IQueryable<Airlines> FindByPredicate(Expression<Func<Airlines, bool>> predicate)
        {
            return _context.Airlines.Where(predicate).AsQueryable();
        }
    }
}
