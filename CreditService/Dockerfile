# 1. build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build 
WORKDIR /src

COPY ["CreditService.sln", "./"]

COPY ["CredirService.Domain/CredirService.Domain.csproj", "CredirService.Domain/"]
COPY ["CreditService.Application/CreditService.Application.csproj", "CreditService.Application/"]
COPY ["CreditService.Infrastructure/CreditService.Infrastructure.csproj", "CreditService.Infrastructure/"]
COPY ["CreditService.Web/CreditService.Web.csproj", "CreditService.Web/"]


RUN dotnet restore "CreditService.Web/CreditService.Web.csproj"


COPY . .

WORKDIR "/src/CreditService.Web"

RUN dotnet publish "CreditService.Web.csproj" -c Release -o /app/out

# 2. Start
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
USER $APP_UID

ENTRYPOINT ["dotnet", "CreditService.Web.dll"]