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
    public interface IPartnerRespository : IGenericRepository<Partner, AirlineTicketsDBContext>
    {
        public int CountRecordsByPredicate(Expression<Func<Partner, bool>> predicate);
        public IQueryable<Partner> FindByPredicate(Expression<Func<Partner, bool>> predicate);
    }
}
