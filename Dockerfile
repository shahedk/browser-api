FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything else and build
COPY ./src ./src
COPY ./test ./test
COPY Browser.sln Browser.sln
RUN dotnet restore Browser.sln


RUN dotnet publish src/Browser.WebApp/Browser.WebApp.csproj --output /published-app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /published-app .

# Configure container for phantomJS dependencies
RUN export OPENSSL_CONF=/etc/ssl/
RUN echo 'debconf debconf/frontend select Noninteractive' | debconf-set-selections
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        ca-certificates \
        libfontconfig1-dev

RUN chmod 777 /app/App_Data/linux/phantomjs
RUN ln -sf /app/App_Data/linux/phantomjs /usr/local/share/phantomjs
RUN ln -sf /app/App_Data/linux/phantomjs /usr/local/bin/phantomjs
RUN ln -sf /app/App_Data/linux/phantomjs /usr/bin/phantomjs

EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Browser.WebApp.dll"]
