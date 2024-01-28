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
    public class FeedbackAndReviewsRespository : GenericRepository<FeedbackAndReviews, AirlineTicketsDBContext>, IFeedbackAndReviewsRespository
    {
        public FeedbackAndReviewsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func<FeedbackAndReviews, bool>> predicate)
        {
            return _context.FeedbackAndReviews.Where(predicate).Count();
        }
        public IQueryable<FeedbackAndReviews> FindByPredicate(Expression<Func<FeedbackAndReviews, bool>> predicate)
        {
            return _context.FeedbackAndReviews.Where(predicate).AsQueryable();
        }
    }
}
