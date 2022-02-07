FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["GringottsBank.Api/GringottsBank.Api.cspro", "GringottsBank.Api/GringottsBank.Api/"]
RUN dotnet restore "GringottsBank.Api/GringottsBank.Api.csproj"
COPY . .
WORKDIR "/src/GringottsBank.Api"
RUN dotnet build "GringottsBank.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GringottsBank.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
##local
##ENTRYPOINT ["dotnet", "GringottsBank.Api.dll"]

## heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GringottsBank.Api.dll

