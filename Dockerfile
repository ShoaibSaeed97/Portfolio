# Use official ASP.NET Core runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY *.sln .
COPY [PersonalPPortfolio]/*.csproj ./[PersonalPPortfolio]/
RUN dotnet restore

# Copy the rest and build
COPY . .
WORKDIR /src/[PersonalPPortfolio]
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "[PersonalPPortfolio].dll"]
