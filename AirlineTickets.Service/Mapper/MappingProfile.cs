using AirlineTickets.DAL.Models.Entity;
using AirlineTickets.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTickets.Service.Mapper
{
   public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap();

        }

        public void CreateMap()
        {
            CreateMap<IdentityUser, UserModel>().ReverseMap();
            CreateMap<Bookings, BookingsDto>().ReverseMap();
            CreateMap<Customers, CustomersDto>().ReverseMap();
            CreateMap<FeedbackAndReviews, FeedbackAndReviewsDto>().ReverseMap();
            CreateMap<Flights, FlightsDto>().ReverseMap();
            CreateMap<Airlines, AirlinesDto>().ReverseMap();
            CreateMap<Payments, PaymentsDto>().ReverseMap();
            CreateMap<Promotions, PromotionsDto>().ReverseMap();
            CreateMap<Tickets, TicketsDto>().ReverseMap();
            CreateMap<Luggages, LuggagesDto>().ReverseMap();
            CreateMap<AirplaneSeats, AirplaneSeatsDto>().ReverseMap();
            

        }
    }
}
