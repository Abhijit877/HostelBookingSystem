# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["HostelBooking.Api/HostelBooking.Api.csproj", "HostelBooking.Api/"]
COPY ["HostelBooking.Application/HostelBooking.Application.csproj", "HostelBooking.Application/"]
COPY ["HostelBooking.Domain/HostelBooking.Domain.csproj", "HostelBooking.Domain/"]
COPY ["HostelBooking.Infrastructure/HostelBooking.Infrastructure.csproj", "HostelBooking.Infrastructure/"]
RUN dotnet restore "HostelBooking.Api/HostelBooking.Api.csproj"

# Copy the entire source code
COPY . .

# Build the application
WORKDIR "/src/HostelBooking.Api"
RUN dotnet build "HostelBooking.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "HostelBooking.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port that the application will run on
EXPOSE 8080

# Set the entry point for the application
ENTRYPOINT ["dotnet", "HostelBooking.Api.dll"]
