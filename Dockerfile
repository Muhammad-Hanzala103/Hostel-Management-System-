# Stage 1 — Build (uses SDK image)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files first for better layer caching
COPY HostelManagement.sln ./
COPY Hostel.Core/Hostel.Core.csproj Hostel.Core/
COPY Hostel.ConsoleApp/Hostel.ConsoleApp.csproj Hostel.ConsoleApp/
COPY Hostel.Tests/Hostel.Tests.csproj Hostel.Tests/
COPY Hostel.Web/Hostel.Web.csproj Hostel.Web/

# Restore dependencies
RUN dotnet restore Hostel.ConsoleApp/Hostel.ConsoleApp.csproj

# Copy the remaining source code
COPY . .

# Build and publish the application
RUN dotnet publish Hostel.ConsoleApp/Hostel.ConsoleApp.csproj -c Release -o /app --no-restore

# Stage 2 — Runtime (uses much smaller runtime image)
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app .

# Create data directory and set permissions (if needed)
RUN mkdir -p data backups exports logs

ENTRYPOINT ["dotnet", "Hostel.ConsoleApp.dll"]
