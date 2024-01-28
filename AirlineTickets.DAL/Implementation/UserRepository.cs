using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Models.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly AirlineTicketsDBContext _context;
        public UserRepository(AirlineTicketsDBContext context)
        {
            _context = context;
        }
        public List<IdentityUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public int CountRecordsByPredicate(Expression<Func<IdentityUser, bool>> predicate)
        {
            return _context.Users.Where(predicate).Count();
        }

        public IdentityUser FindById(string id)
        {
            return _context.Users.Where(m => m.Id == id).FirstOrDefault();
        }

        public IQueryable<IdentityUser> FindByPredicate(Expression<Func<IdentityUser, bool>> predicate)
        {
            return _context.Users.Where(predicate).AsQueryable();
        }

        public IdentityUser FindUser(string? Id)
        {
            return _context.Users.FirstOrDefault(m => m.Id == Id);
        }
        public IdentityUser FindByEmail(string? email)
        {

            IdentityUser user = _context.Users.Where(n => n.Email == email).FirstOrDefault();

            return user;
        }
    }
}
