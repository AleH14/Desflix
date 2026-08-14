# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Desflix.csproj", "."]
RUN dotnet restore "Desflix.csproj"

COPY . .
RUN dotnet build "Desflix.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desflix.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Desflix.dll"]
