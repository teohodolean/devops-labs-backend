FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevOps-Labs-Backend/DevOps-Labs-Backend.csproj", "DevOps-Labs-Backend/"]
RUN dotnet restore "DevOps-Labs-Backend/DevOps-Labs-Backend.csproj"
COPY . .
WORKDIR "/src/DevOps-Labs-Backend"
RUN dotnet build "DevOps-Labs-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOps-Labs-Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOps-Labs-Backend.dll"]