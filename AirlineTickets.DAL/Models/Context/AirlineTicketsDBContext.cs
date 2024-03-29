using MayNghien.Common.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AirlineTickets.DAL.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.DAL.Models.Context
{
    public class AirlineTicketsDBContext : BaseContext
    {
        public AirlineTicketsDBContext()
        {

        }
        public AirlineTicketsDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Tickets> Tickets{ get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<FeedbackAndReviews> FeedbackAndReviews { get; set; }
        public DbSet<Flights> Flights { get; set; }
        public DbSet<AirplaneSeats> AirplaneSeats { get; set; }
        public DbSet<Luggages> Luggages { get; set; }
        public DbSet<Airlines> Airlines { get; set; }
        public DbSet<Payments> Payments { get; set; } 
        public DbSet<Promotions> Promotions { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSetting = JsonConvert.DeserializeObject<AppSetting>(File.ReadAllText("appsettings.json"));
                optionsBuilder.UseSqlServer(appSetting.ConnectionString);
            }


        }
    }
}
