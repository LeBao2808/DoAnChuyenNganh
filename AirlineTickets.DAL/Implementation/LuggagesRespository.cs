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
    public class LuggagesRespository : GenericRepository<Luggages, AirlineTicketsDBContext>, ILuggagesRespository
    {
        public LuggagesRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Luggages, bool>> predicate)
        {
            return _context.Luggages.Where(predicate).Count();
        }
        public IQueryable<Luggages> FindByPredicate(Expression<Func<Luggages, bool>> predicate)
        {
            return _context.Luggages.Where(predicate).AsQueryable();
        }
    }
}