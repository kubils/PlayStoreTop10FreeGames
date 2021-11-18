#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PlayStoreTopGames/PlayStoreTopGames.csproj", "PlayStoreTopGames/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["BackgroundJob/BackgroundJob.csproj", "BackgroundJob/"]
RUN dotnet restore "PlayStoreTopGames/PlayStoreTopGames.csproj"
COPY . .
WORKDIR "/src/PlayStoreTopGames"
RUN dotnet build "PlayStoreTopGames.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayStoreTopGames.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayStoreTopGames.dll"]