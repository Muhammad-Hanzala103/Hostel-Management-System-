FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files
COPY Hostel.Core/Hostel.Core.csproj Hostel.Core/
COPY Hostel.ConsoleApp/Hostel.ConsoleApp.csproj Hostel.ConsoleApp/
COPY Hostel.Tests/Hostel.Tests.csproj Hostel.Tests/
RUN dotnet restore Hostel.ConsoleApp/Hostel.ConsoleApp.csproj

# Copy source & build
COPY . .
RUN dotnet publish Hostel.ConsoleApp/Hostel.ConsoleApp.csproj -c Release -o /app

# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Hostel.ConsoleApp.dll"]
