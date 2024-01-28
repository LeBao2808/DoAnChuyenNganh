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
        public DbSet<BookingHistory> BookingsHistory { get; set; }
        public DbSet<BookTickets> BookTickets { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<FeedbackAndReviews> FeedbackAndReviews { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Pay> Pay { get; set; } 
        public DbSet<Promotion> Promotion { get; set; } 
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
