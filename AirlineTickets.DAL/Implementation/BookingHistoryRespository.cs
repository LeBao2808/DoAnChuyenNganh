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
    public class BookingHistoryRespository : GenericRepository<BookingHistory, AirlineTicketsDBContext>, IBookingHistoryRespository
    {
        public BookingHistoryRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<BookingHistory, bool>> predicate)
        {
            return _context.BookingsHistory.Where(predicate).Count();
        }
        public IQueryable<BookingHistory> FindByPredicate(Expression<Func<BookingHistory, bool>> predicate)
        {
            return _context.BookingsHistory.Where(predicate).AsQueryable();
        }
    }
}
