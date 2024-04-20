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
            builder.Services.AddScoped<IBookingsService, BookingsService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddScoped<IFeedbackAndReviewsService, FeedbackAndReviewsService>();
            builder.Services.AddScoped<IFlightsService, FlightsService>();
            builder.Services.AddScoped<IAirlinesService, AirlinesService>();
            builder.Services.AddScoped<IPaymentsService, PaymentsService>();
            builder.Services.AddScoped<IPromotionsService, PromotionsService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITicketsService, TicketsService>();
            builder.Services.AddScoped<ILuggagesService, LuggagesService>();

            //builder.Services.AddScoped(typeof(IPromotionRespository),typeof(PromotionRespository));

            builder.Services.AddScoped(typeof(SearchRequest));

            #endregion Service Mapping
            #region Repository Mapping
            builder.Services.AddScoped<IBookingsRespository, BookingsRespository>();
            builder.Services.AddScoped<ICustomersRespository, CustomersRespositorycs>();
            builder.Services.AddScoped<IFeedbackAndReviewsRespository, FeedbackAndReviewsRespository>();
            builder.Services.AddScoped<IFlightsRespository, FlightsRespository>();
            builder.Services.AddScoped<IAirlinesRespository, AirlinesRespository>();
            builder.Services.AddScoped<IPaymentsRespository, PaymentsRespository>();
            builder.Services.AddScoped<IPromotionsRespository, PromotionsRespository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITicketsRespository, TicketsRespository>();
            builder.Services.AddScoped<ILuggagesRespository, LuggagesRespository>();
            builder.Services.AddScoped<IAirplaneSeatsRespository, AirplaneSeatsRespository>();

            #endregion Repository Mapping
        }
    }
}
