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
    public class PartnerRespository : GenericRepository<Partner, AirlineTicketsDBContext>, IPartnerRespository
    {
        public PartnerRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<Partner, bool>> predicate)
        {
            return _context.Partner.Where(predicate).Count();
        }
        public IQueryable<Partner> FindByPredicate(Expression<Func<Partner, bool>> predicate)
        {
            return _context.Partner.Where(predicate).AsQueryable();
        }
    }
}
