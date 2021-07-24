#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Quran.Server.Api/Quran.Server.Api.csproj", "Quran.Server.Api/"]
COPY ["Quran.Server.Application/Quran.Server.Application.csproj", "Quran.Server.Application/"]
COPY ["Quran.Server.Domain/Quran.Server.Domain.csproj", "Quran.Server.Domain/"]
COPY ["Quran.Server.Infrastructure/Quran.Server.Infrastructure.csproj", "Quran.Server.Infrastructure/"]
RUN dotnet restore "Quran.Server.Api/Quran.Server.Api.csproj"
COPY . .
WORKDIR "/src/Quran.Server.Api"
RUN dotnet build "Quran.Server.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Quran.Server.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quran.Server.Api.dll"]

