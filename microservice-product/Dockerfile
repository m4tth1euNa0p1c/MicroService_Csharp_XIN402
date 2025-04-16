FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["microservice-product.csproj", "./"]
RUN dotnet restore "microservice-product.csproj"

COPY . .

RUN dotnet publish "microservice-product.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "microservice-product.dll"]
