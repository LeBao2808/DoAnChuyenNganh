using AirlineTickets.DAL.Contract;
using AirlineTickets.DAL.Implementation;
using AirlineTickets.Service.Contract;
using AirlineTickets.Service.Implementation;
using MayNghien.Models.Request.Base;

namespace AirlineTickets.API.StartUp
{
    public class ServiceRepoMapping
    {
        public ServiceRepoMapping() { }

        public void Mapping(WebApplicationBuilder builder)
        {
            #region Service Mapping
            builder.Services.AddScoped<IBookingHistoryService, BookingHistoryService>();
            builder.Services.AddScoped<IBookTicketsService, BookTicketsService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddScoped<IFeedbackAndReviewsService, FeedbackAndReviewsService>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<IPartnerService, PartnerService>();
            builder.Services.AddScoped<IPayService, PayService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            //builder.Services.AddScoped(typeof(IPromotionRespository),typeof(PromotionRespository));

            builder.Services.AddScoped(typeof(SearchRequest));

            #endregion Service Mapping
            #region Repository Mapping
            builder.Services.AddScoped<IBookingHistoryRespository, BookingHistoryRespository>();
            builder.Services.AddScoped<IBookTicketsRespository, BookTicketsRespository>();
            builder.Services.AddScoped<ICustomersRespository, CustomersRespositorycs>();
            builder.Services.AddScoped<IFeedbackAndReviewsRespository, FeedbackAndReviewsRespository>();
            builder.Services.AddScoped<IFlightRespository, FlightRespository>();
            builder.Services.AddScoped<IPartnerRespository, PartnerRespository>();
            builder.Services.AddScoped<IPayRespository, PayRespository>();
            builder.Services.AddScoped<IPromotionRespository, PromotionRespository>();

            #endregion Repository Mapping
        }
    }
}
