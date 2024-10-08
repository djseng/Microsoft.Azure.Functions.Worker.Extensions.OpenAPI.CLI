# Azure Functions OpenAPI CLI Extension

This extension adds the functionality to run the OpenAPI as CLI project. In this way you can create a OpenAPI definition on build time of your functions project.

## How do I use this ?

Add the nuget package to your function project

<PackageReference Include="Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI" Version="1.0.7" />

rebuild your project.

you can then create a OpenAPI output by running the following command in your root of your function project.

`dotnet ./bin/debug/net8.0/azfuncopenapi.dll`

Make sure you swap out net8.0 for the version you are using within your project

## Options

| Option               | Description                                                                               |
| -------------------- | ----------------------------------------------------------------------------------------- |
| -p (--project)       | specify the path to your project (Default is current directory)                           |
| -o (--output)        | specify the output path for swagger definition file                                       |
| -a (--apibaseurl)    | specify the API base url used within the swagger definition (default localhost)           |
| -c (--configuration) | Specify the project Configuration. Default is 'Debug                                      |
| -t (--target)        | Specifty the project target framework. Default is 'net8.0'                                |
| -v (--version)       | Specify the OpenAPI version. Default is V3                                                |
| -f (--format)        | Specify the OpenAPI output format. Value can be either 'json' or 'yaml'. Default is 'json |

## Can I integrate this in my build?

add the following to your csproj. file

*** This is not published to a public registry, you will need to publish this nuget to your own registry.

```
dotnet add package Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI --version 1.0.7
```

see the [props](src/Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI/build/Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI.props) available for customization.
