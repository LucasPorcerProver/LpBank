#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LP.Bank.Api/LP.Bank.Api.csproj", "LP.Bank.Api/"]
COPY ["LP.Bank.Application/LP.Bank.Application.csproj", "LP.Bank.Application/"]
COPY ["LP.Bank.Domain/LP.Bank.Domain.csproj", "LP.Bank.Domain/"]
COPY ["LP.Bank.Infra.Data.Accounts/LP.Bank.Infra.Data.Accounts.csproj", "LP.Bank.Infra.Data.Accounts/"]
COPY ["LP.Bank.Infra.Data.Identity/LP.Bank.Infra.Data.Identity.csproj", "LP.Bank.Infra.Data.Identity/"]
RUN dotnet restore "LP.Bank.Api/LP.Bank.Api.csproj"
COPY . .
WORKDIR "/src/LP.Bank.Api"
RUN dotnet build "LP.Bank.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LP.Bank.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LP.Bank.Api.dll"]