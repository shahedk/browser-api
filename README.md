# [BrowserApi](https://b.shahedk.net) - Headless Browser as an Web API

BrowserApi is available in docker container. We can also run it locally as a .NET Core web application. Internally, it uses [PhantomJs](https://phantomjs.org) to simulate headless browser environment.

Run BrowserApi on Port:8080
```
docker run --name=browser-api -p 8080:80 shahedk/browser-api
```

## Use Cases

 - **Zero setup Headless web testing**: Fast testing of web page loading without the browser. 
 - **Screenshot**: Take screen shot any web page with specified height & width.
 - **Get the raw HTML**: Access page content of any ajax web page as simple html.
 - **Execute script**: Provide javascript code when calling the API to execute scripts on any web page.
