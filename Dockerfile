FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy everything else and build
COPY ./src ./src
COPY ./test ./test
COPY Browser.sln Browser.sln
RUN dotnet restore Browser.sln


RUN dotnet publish src/Browser.WebApi/Browser.WebApi.csproj --output published-app

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app

COPY --from=build-env app/src/Browser.WebApi/published-app .

# Configure container for phantomJS dependencies
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
ENTRYPOINT ["dotnet", "Browser.WebApi.dll"]
