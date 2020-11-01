FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 11111
EXPOSE 30000
 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ImprovTimeConsole/ImprovTimeConsole.csproj", "ImprovTimeConsole/"]
RUN dotnet restore "ImprovTimeConsole/ImprovTimeConsole.csproj"
COPY . .
WORKDIR "/src/ImprovTimeConsole"
RUN dotnet build "ImprovTimeConsole.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "ImprovTimeConsole.csproj" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImprovTimeConsole.dll"]