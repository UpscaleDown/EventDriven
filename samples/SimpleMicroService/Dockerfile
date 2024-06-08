
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build
COPY . .
WORKDIR /build/samples/SimpleMicroService
RUN dotnet restore "SimpleMicroService.csproj"
RUN dotnet build "SimpleMicroService.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleMicroService.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleMicroService.dll"]