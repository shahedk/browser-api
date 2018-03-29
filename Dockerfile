FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Browser.sln ./
COPY Browser.WebApi/Browser.WebApi.csproj Browser.WebApi/
COPY Browser.Core/Browser.Core.csproj Browser.Core/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Browser.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .


RUN echo 'debconf debconf/frontend select Noninteractive' | debconf-set-selections
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        ca-certificates \
        libfontconfig1-dev

RUN chmod 777 /app/App_Data/linux/phantomjs
RUN ln -sf /app/App_Data/linux/phantomjs /usr/local/share/phantomjs
RUN ln -sf /app/App_Data/linux/phantomjs /usr/local/bin/phantomjs
RUN ln -sf /app/App_Data/linux/phantomjs /usr/bin/phantomjs


ENTRYPOINT ["dotnet", "Browser.WebApi.dll"]
