using Application.CQRS.Commands.StockCommand;
using Application.CQRS.Queries.StockQuery;
using DomainModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Spex.Stock.Api.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class StockController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LocationController> _logger;

        public StockController(IMediator mediator, ILogger<LocationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IList<StockRequest> stocks)
        {
            if (stocks == null || !stocks.Any())
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new CreateStockCommand { Stocks = stocks });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IList<GetStockBySkusResponse>))]
        public async Task<IActionResult> GetStock([FromHeader] IList<string> skus)
        {
            if (!skus.Any())
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetStockBySkusQuery { Skus = skus });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
