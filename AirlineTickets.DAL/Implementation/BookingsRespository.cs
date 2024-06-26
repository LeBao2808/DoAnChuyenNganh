﻿using AirlineTickets.DAL.Contract;
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
    public class BookingsRespository : GenericRepository<Bookings, AirlineTicketsDBContext>, IBookingsRespository
    {
        public BookingsRespository(AirlineTicketsDBContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }
        public int CountRecordsByPredicate(Expression<Func< Bookings, bool>> predicate)
        {
            return _context.Bookings.Where(predicate).Count();
        }
        public IQueryable<Bookings> FindByPredicate(Expression<Func<Bookings, bool>> predicate)
        {
            return _context.Bookings.Where(predicate).AsQueryable();
        }
    }
}
