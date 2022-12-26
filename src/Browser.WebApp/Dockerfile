FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Browser.WebApp/Browser.WebApp.csproj", "Browser.WebApp/"]
RUN dotnet restore "Browser.WebApp/Browser.WebApp.csproj"
COPY . .
WORKDIR "/src/Browser.WebApp"
RUN dotnet build "Browser.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Browser.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Browser.WebApp.dll"]
