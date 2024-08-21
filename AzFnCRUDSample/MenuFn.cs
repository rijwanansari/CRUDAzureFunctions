using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzFnCRUDSample.Service.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AzFnCRUDSample
{
    public class MenuFn
    {
        private readonly ILogger<MenuFn> _logger;
        private readonly IMenuService _menuService;

        public MenuFn(ILogger<MenuFn> log, IMenuService menuService)
        {
            _logger = log;
            _menuService = menuService;
        }

        [FunctionName("MenuFn")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("MenuFn HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        //azure funtion to get all itemtypes
        [FunctionName("GetItemTypes")]
        public async Task<IActionResult> GetItemTypes(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "itemtypes")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = await _menuService.GetItemTypesAsync();
            return new OkObjectResult(response);
        }


        [FunctionName("GetItems")]
        [OpenApiOperation(operationId: "GetItems", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            // get all items
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var items = await _menuService.GetItemsAsync();
            return new OkObjectResult(items);
        }
    }
}

