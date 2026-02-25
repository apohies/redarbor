FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Redarbor.API/Redarbor.API.csproj", "Redarbor.API/"]
COPY ["Redarbor.Application/Redarbor.Application.csproj", "Redarbor.Application/"]
COPY ["Redarbor.Domain/Redarbor.Domain.csproj", "Redarbor.Domain/"]
COPY ["Redarbor.Infrastructure/Redarbor.Infrastructure.csproj", "Redarbor.Infrastructure/"]

RUN dotnet restore "Redarbor.API/Redarbor.API.csproj"

COPY . .

RUN dotnet build "Redarbor.API/Redarbor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Redarbor.API/Redarbor.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Redarbor.API.dll"]
