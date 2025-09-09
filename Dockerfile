FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Biblioteca.API/Biblioteca.API.csproj", "src/Biblioteca.API/"]
COPY ["src/Biblioteca.Domain/Biblioteca.Domain.csproj", "src/Biblioteca.Domain/"]
COPY ["src/Biblioteca.Application/Biblioteca.Application.csproj", "src/Biblioteca.Application/"]
COPY ["src/Biblioteca.Infrastructure/Biblioteca.Infrastructure.csproj", "src/Biblioteca.Infrastructure/"]

RUN dotnet restore "src/Biblioteca.API/Biblioteca.API.csproj" --disable-parallel

COPY . .
WORKDIR "/src/Biblioteca.API"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Biblioteca.API.dll"]