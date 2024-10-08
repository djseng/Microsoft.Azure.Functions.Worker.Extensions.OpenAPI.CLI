using System;
using System.Threading.Tasks;
using Cocona;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI.Abstractions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;

namespace Microsoft.Azure.Functions.Worker.Extensions.OpenApi.CLI.ConsoleApp
{
    public class GenerateSwaggerConsoleApp
    {
        /// <summary>
        ///     Generates the OpenAPI document.
        /// </summary>
        /// <param name="project">Project path.</param>
        /// <param name="apibaseurl"></param>
        /// <param name="configuration">Compile configuration.</param>
        /// <param name="target"></param>
        /// <param name="version">OpenAPI version.</param>
        /// <param name="format">OpenAPI output format.</param>
        /// <param name="output">Output path.</param>
        public async Task Run(
            [FromService] ICustomApiMockCreator customApiMockCreator,
            [FromService] ICustomOpenApiCreator customOpenApiCreator,
            [FromService] ICustomOpenApiWriter customOpenApiWriter,
            [Option('p', Description = "Api Project path. Default is current directory")]
            string project = ".",
            [Option('a', Description = "ApiBaseUrl. Default is 'localhost'")]
            string apibaseurl = "localhost",
            [Option('c', Description = "Configuration. Default is 'Debug'")]
            string configuration = "Debug",
            [Option('t', Description = "Target framework. Default is 'net8.0'")]
            string target = "net8.0",
            [Option('v', Description = "OpenAPI spec version. Value can be either 'v2' or 'v3'. Default is 'v3'")]
            OpenApiVersionType version = OpenApiVersionType.V3,
            [Option('f', Description = "OpenAPI output format. Value can be either 'json' or 'yaml'. Default is 'json'")]
            OpenApiFormatType format = OpenApiFormatType.Json,
            [Option('o', Description = "Generated OpenAPI output location. Default is 'output'")]
            string output = "output")
        {
            var apiMock = customApiMockCreator.SetupApi(project, configuration, target);

            var openApiDocument = await customOpenApiCreator.CreateOpenApiDocument(
                apibaseurl,
                apiMock.CompiledDllPath,
                apiMock.HttpSettings.RoutePrefix,
                apiMock.OpenApiInfo,
                version,
                format);

            var outputPath = output.GetOutputPath(apiMock.CompiledPath);
            try
            {
                await customOpenApiWriter.WriteOpenApiToFile(openApiDocument, outputPath, format);

            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
            Console.Out.WriteLine($"Swagger succesfully generated at {outputPath}");

        }
    }
}
