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
    public class BookTicketsRespository : GenericRepository<BookTickets, AirlineTicketsDBContext>, IBookTicketsRespository
    {
        public BookTicketsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<BookTickets, bool>> predicate)
        {
            return _context.BookTickets.Where(predicate).Count();
        }
        public IQueryable<BookTickets> FindByPredicate(Expression<Func<BookTickets, bool>> predicate)
        {
            return _context.BookTickets.Where(predicate).AsQueryable();
        }
    }
}
