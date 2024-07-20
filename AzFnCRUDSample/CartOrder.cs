using System.IO;
using System.Net;
using System.Threading.Tasks;
using AzFnCRUDSample.Service.Cart;
using AzFnCRUDSample.Service.Cart.Dto;
using AzFnCRUDSample.Service.Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AzFnCRUDSample
{
    public class CartOrder
    {
        private readonly ILogger<CartOrder> _logger;
        private readonly ICartService _cartService;

        public CartOrder(ILogger<CartOrder> log, ICartService cartService)
        {
            _logger = log;
            _cartService = cartService;
        }

        [FunctionName("CreateCartOrder")]
        [OpenApiOperation(operationId: "CreateCartOrder", tags: new[] { "CartOrder" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CartOrderRequest), Required = true, Description = "CartOrderRequest object")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResponseModel<CartOrderRequest>), Description = "The OK response")]
        public async Task<IActionResult> CreateCartOrder(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "cartorder")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CartOrderRequest>(requestBody);

            var response = await _cartService.CreateCartOrder(data);


            return new OkObjectResult(response);
        }
    }
}

