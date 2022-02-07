FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ./src src
RUN dotnet restore "src/GringottsBank.Api/GringottsBank.Api.csproj"
COPY . .
WORKDIR "/src/src/GringottsBank.Api"
RUN dotnet build "GringottsBank.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GringottsBank.Api.csproj" -c Release -o /app/publish

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GringottsBank.Api.dll"]