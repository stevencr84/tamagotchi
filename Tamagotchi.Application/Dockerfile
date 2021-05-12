#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Tamagotchi.Application/Tamagotchi.Application.csproj", "Tamagotchi.Application/"]
COPY ["Tamagotchi.Infrastructure/Tamagotchi.Infrastructure.csproj", "Tamagotchi.Infrastructure/"]
COPY ["Tamagotchi.Domain/Tamagotchi.Domain.csproj", "Tamagotchi.Domain/"]
RUN dotnet restore "Tamagotchi.Application/Tamagotchi.Application.csproj"
COPY . .
WORKDIR "/src/Tamagotchi.Application"
RUN dotnet build "Tamagotchi.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tamagotchi.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tamagotchi.Application.dll"]