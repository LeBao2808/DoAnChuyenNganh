using AirlineTickets.DAL.Models.Entity;
using AirlineTickets.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuanLyNhanSuBackEnd.Model.Dto;
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
            CreateMap<BookingHistory, BookingHistoryDto>().ReverseMap();
            CreateMap<IdentityUser, UserModel>().ReverseMap();
            CreateMap<BookTickets, BookTicketsDto>().ReverseMap();
            CreateMap<Customers, CustomersDto>().ReverseMap();
            CreateMap<FeedbackAndReviews, FeedbackAndReviewsDto>().ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<Partner, PartnerDto>().ReverseMap();
            CreateMap<Pay, PayDto>().ReverseMap();
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            

        }
    }
}
