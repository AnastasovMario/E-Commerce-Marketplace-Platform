#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce Marketplace Platform/E-CommerceMarketplace.csproj", "E-Commerce Marketplace Platform/"]
COPY ["E-CommerMarketplace.Core/E-CommerceMarketplace.Core.csproj", "E-CommerMarketplace.Core/"]
COPY ["E-CommerceMarketplace.Infrastructure/E-CommerceMarketplace.Infrastructure.csproj", "E-CommerceMarketplace.Infrastructure/"]
RUN dotnet restore "E-Commerce Marketplace Platform/E-CommerceMarketplace.csproj"
COPY . .
WORKDIR "/src/E-Commerce Marketplace Platform"
RUN dotnet build "E-CommerceMarketplace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-CommerceMarketplace.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-CommerceMarketplace.dll"]