#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CleanApp.RestAPI/CleanApp.RestAPI.csproj", "CleanApp.RestAPI/"]
RUN dotnet restore "CleanApp.RestAPI/CleanApp.RestAPI.csproj"
COPY . .
WORKDIR "/src/CleanApp.RestAPI"
RUN dotnet build "CleanApp.RestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanApp.RestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanApp.RestAPI.dll"]