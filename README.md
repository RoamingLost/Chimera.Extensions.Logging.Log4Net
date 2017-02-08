# .NET Core logging extension for log4net

Package: [Chimera.Extensions.Logging.Log4Net](http://www.nuget.org/packages/Chimera.Extensions.Logging.Log4Net)
[![NuGet](http://img.shields.io/nuget/v/Chimera.Extensions.Logging.Log4Net.svg)](http://www.nuget.org/packages/Chimera.Extensions.Logging.Log4Net)

[log4net](https://github.com/apache/log4net) logger provider for [Microsoft.Extensions.Logging](https://github.com/aspnet/Logging). Compatible with ASP.NET Core and .NET Core official release (dotnet).

Routes .NET Core log messages to log4net. Refer to the [wiki](https://github.com/RoamingLost/Chimera.Extensions.Logging.Log4Net/wiki) for more information.

## How to use

1. Add dependency in project.json
    ```json
     "dependencies": {
        "Chimera.Extensions.Logging.Log4Net": "1.0.0-*"
      }
    ```

2. Create log4net.config in root of your project file, see [log4net.config example](https://logging.apache.org/log4net/release/manual/configuration.html)
3. In Startup.cs add in `Configure`

    ```c#
    using Chimera.Extensions.Logging.Log4Net;
    
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        //add log4net to .NET Core
        loggerFactory.AddLog4Net();
    ```  
