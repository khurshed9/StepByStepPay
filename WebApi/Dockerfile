# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY WebApi/*.csproj ./WebApi/
RUN dotnet restore ./WebApi/WebApi.csproj

# Копируем весь проект и билдим
COPY . .
RUN dotnet publish ./WebApi/WebApi.csproj -c Release -o /app/publish

# Используем runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
