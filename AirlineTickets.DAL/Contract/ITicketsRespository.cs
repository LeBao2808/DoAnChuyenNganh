using AirlineTickets.DAL.Models.Context;
using AirlineTickets.DAL.Models.Entity;
using Maynghien.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Contract
{
    public interface ITicketsRespository : IGenericRepository<Tickets, AirlineTicketsDBContext>
    {

        public int CountRecordsByPredicate(Expression<Func<Tickets, bool>> predicate);
        public IQueryable<Tickets> FindByPredicate(Expression<Func<Tickets, bool>> predicate);
    }
}
