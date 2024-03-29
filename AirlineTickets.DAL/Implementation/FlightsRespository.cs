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
    public class FlightsRespository : GenericRepository<Flights, AirlineTicketsDBContext>, IFlightsRespository
    {
        public FlightsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Flights, bool>> predicate)
        {
            return _context.Flights.Where(predicate).Count();
        }
        public IQueryable<Flights> FindByPredicate(Expression<Func<Flights, bool>> predicate)
        {
            return _context.Flights.Where(predicate).AsQueryable();
        }
    }
}
