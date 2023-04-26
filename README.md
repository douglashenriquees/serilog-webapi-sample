## Used Packages

* ```dotnet add package Serilog```
* ```dotnet add package Serilog.Sinks.Console```
* ```dotnet add package Serilog.Sinks.File```
* ```dotnet add package Serilog.Sinks.Seq```

## Run Seq Container

* ```docker run --name seq --restart unless-stopped -e ACCEPT_EULA=Y -p 80:80 -p 5341:5341 datalust/seq```