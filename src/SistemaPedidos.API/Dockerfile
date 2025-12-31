# ESTÁGIO 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia todos os arquivos .csproj primeiro (isso agiliza o build)
COPY ["src/SistemaPedidos.API/SistemaPedidos.API.csproj", "src/SistemaPedidos.API/"]
COPY ["src/SistemaPedidos.Infrastructure/SistemaPedidos.Infrastructure.csproj", "src/SistemaPedidos.Infrastructure/"]
COPY ["src/SistemaPedidos.Domain/SistemaPedidos.Domain.csproj", "src/SistemaPedidos.Domain/"]

# Restaura as dependências
RUN dotnet restore "src/SistemaPedidos.API/SistemaPedidos.API.csproj"

# Copia o restante dos arquivos de todos os projetos
COPY . .

# Compila e publica
WORKDIR "/src/src/SistemaPedidos.API"
RUN dotnet publish "SistemaPedidos.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ESTÁGIO 2: Final (Execução)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta que o Render usa (geralmente 80 ou 10000, o Render detecta)
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "SistemaPedidos.API.dll"]