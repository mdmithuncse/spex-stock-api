using AutoMapper;
using DataModel;
using DomainModel;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.CQRS.Commands.StockCommand
{
    public class CreateStockCommand : IRequest<bool>
    {
        public IList<StockRequest> Stocks { get; set; }

        public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, bool>
        {
            private readonly IAppDbContext _context;
            private readonly ILogger<CreateStockCommandHandler> _logger;
            private readonly IMapper _mapper;

            public CreateStockCommandHandler(IAppDbContext context, ILogger<CreateStockCommandHandler> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            private bool IsValidRequest(IList<StockRequest> Stocks)
            {
                return Stocks.Any(x => string.IsNullOrWhiteSpace(x.Sku) || x.LocationId == 0) ? false : true;
            }

            private async Task<bool> IsValidLocationAsync(IList<StockRequest> stocks)
            {
                var locations = await _context.Locations.Where(x => true).OrderBy(x => x.Id).AsQueryable().AsNoTracking().ToListAsync();

                return !stocks.Select(x => x.LocationId).Except(locations.Select(x => x.Id)).Any();
            }
            
            private async Task<(IList<StockRequest> newStocks, IList<StockRequest> oldStocks)> GetStocksAsync(IList<StockRequest> stocks)
            {
                var allStocks = await _context.Stocks.Where(x => true).OrderBy(x => x.Id).AsQueryable().AsNoTracking().ToListAsync();
                var newStocks = stocks.Where(x => !allStocks.Any(c => c.Sku == x.Sku)).ToList();
                var oldStocks = stocks.Where(x => allStocks.Any(c => c.Sku == x.Sku)).ToList();

                return (newStocks: newStocks, oldStocks: oldStocks);
            }

            private async Task<bool> CreateStocksAsync(IList<StockRequest> stocks)
            {
                await _context.Stocks.AddRangeAsync(_mapper.Map<IList<StockModel>>(stocks));
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Data saved successfully");

                return true;
            }

            private async Task<bool> UpdateStocksAsync(IList<StockRequest> stocks)
            {
                var oldStocks = new List<StockModel>();

                foreach (var item in stocks)
                {
                    var oldStock = await _context.Stocks.Where(x => x.Sku == item.Sku && x.LocationId == item.LocationId).AsNoTracking().FirstOrDefaultAsync();
                    
                    if (oldStock != null)
                    {
                        oldStocks.Add(new StockModel
                        {
                            Id = oldStock.Id,
                            Sku = oldStock.Sku,
                            LocationId = oldStock.LocationId,
                            Quantity = item.Quantity,
                            Created = oldStock.Created,
                            Updated = DateTime.UtcNow
                        });
                    }
                }

                _context.Stocks.UpdateRange(oldStocks);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Data updated successfully");

                return true;
            }

            public async Task<bool> Handle(CreateStockCommand command, CancellationToken cancellationToken)
            {
                bool result = false;

                if (!IsValidRequest(command.Stocks))
                {
                    _logger.LogInformation($"Invalid input request");

                    return result;
                }

                if (!await IsValidLocationAsync(command.Stocks))
                {
                    _logger.LogInformation($"Location not found");
                    
                    return result;
                }

                var stocks = await GetStocksAsync(command.Stocks);

                if (stocks.newStocks.Any())
                {
                    result = await CreateStocksAsync(stocks.newStocks);
                }

                if (stocks.oldStocks.Any())
                {
                    result = await UpdateStocksAsync(stocks.oldStocks);
                }

                return result;
            }
        }
    }
}
