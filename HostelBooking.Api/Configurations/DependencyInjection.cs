using HostelBooking.Application.Interfaces;
using HostelBooking.Application.Services;
using HostelBooking.Infrastructure.Interfaces;
using HostelBooking.Infrastructure.Repositories;

namespace HostelBooking.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IHostelRepository, HostelRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Register Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IHostelService, HostelService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
