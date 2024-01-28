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
    public class FlightRespository : GenericRepository<Flight, AirlineTicketsDBContext>, IFlightRespository
    {
        public FlightRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Flight, bool>> predicate)
        {
            return _context.Flight.Where(predicate).Count();
        }
        public IQueryable<Flight> FindByPredicate(Expression<Func<Flight, bool>> predicate)
        {
            return _context.Flight.Where(predicate).AsQueryable();
        }
    }
}
