FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservice-cart.csproj", "./"]
RUN dotnet restore "./microservice-cart.csproj"
COPY . .
RUN dotnet build "microservice-cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "microservice-cart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microservice-cart.dll"]
