#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AWS.Services.ElasticContainerService/AWS.Services.ElasticContainerService.csproj", "AWS.Services.ElasticContainerService/"]
RUN dotnet restore "AWS.Services.ElasticContainerService/AWS.Services.ElasticContainerService.csproj"
COPY . .
WORKDIR "/src/AWS.Services.ElasticContainerService"
RUN dotnet build "AWS.Services.ElasticContainerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AWS.Services.ElasticContainerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AWS.Services.ElasticContainerService.dll"]