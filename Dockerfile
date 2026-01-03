# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de proyecto y restaura dependencias
COPY ["EscuelaPrimariaAPI.csproj", "./"]
RUN dotnet restore "EscuelaPrimariaAPI.csproj"

# Copia todo el resto del código
COPY . .

# Publica la aplicación
RUN dotnet publish "EscuelaPrimariaAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Imagen final para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Puerto y entorno
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Nombre exacto de tu DLL (según tu proyecto)
ENTRYPOINT ["dotnet", "EscuelaPrimariaAPI.dll"]