using System;

namespace HostelBooking.Infrastructure.Data
{
    public static class ConnectionStringHelper
    {
        public static string ParseRailwayUrl(string railwayUrl)
        {
            if (string.IsNullOrEmpty(railwayUrl))
                throw new ArgumentException("Railway URL cannot be null or empty", nameof(railwayUrl));

            var uri = new Uri(railwayUrl);

            var host = uri.Host;
            var port = uri.Port;
            var database = uri.AbsolutePath.TrimStart('/');
            var username = uri.UserInfo.Split(':')[0];
            var password = uri.UserInfo.Split(':')[1];

            return $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
        }
    }
}
