#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Impact.Basket.Api/Impact.Basket.Api.csproj", "Impact.Basket.Api/"]
RUN dotnet restore "Impact.Basket.Api/Impact.Basket.Api.csproj"
COPY . .
WORKDIR "/src/Impact.Basket.Api"
RUN dotnet build "Impact.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Impact.Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Impact.Basket.Api.dll"]