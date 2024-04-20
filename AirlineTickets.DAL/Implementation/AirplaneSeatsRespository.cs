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
    public class AirplaneSeatsRespository : GenericRepository<AirplaneSeats, AirlineTicketsDBContext>, IAirplaneSeatsRespository
    {
        public AirplaneSeatsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<AirplaneSeats, bool>> predicate)
        {
            return _context.AirplaneSeats.Where(predicate).Count();
        }
        public IQueryable<AirplaneSeats> FindByPredicate(Expression<Func<AirplaneSeats, bool>> predicate)
        {
            return _context.AirplaneSeats.Where(predicate).AsQueryable();
        }
    }
}