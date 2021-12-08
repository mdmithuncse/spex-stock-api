using AutoMapper;
using DomainModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.StockQuery
{
    public class GetStockBySkusQuery : IRequest<IList<GetStockBySkusResponse>>
    {
        public IList<string> Skus { get; set; }

        public class GetStockBySkusQueryHandler : IRequestHandler<GetStockBySkusQuery, IList<GetStockBySkusResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetStockBySkusQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IList<GetStockBySkusResponse>> Handle(GetStockBySkusQuery query, CancellationToken cancellationToken)
            {
                var stocks = new List<GetStockBySkusResponse>();
                
                foreach (var sku in query.Skus)
                {
                    if (!string.IsNullOrWhiteSpace(sku))
                    {
                        var items = await _context.Stocks.Where(x => x.Sku == sku && x.Quantity > 0).AsQueryable().AsNoTracking().ToListAsync();

                        if (items.Any())
                        {
                            var stockItems = new List<Stock>();

                            foreach(var item in items)
                            {
                                var location = await _context.Locations.Where(x => x.Id == item.LocationId).FirstOrDefaultAsync();

                                stockItems.Add(new Stock
                                {
                                    Location = location.Location,
                                    Quantity = item.Quantity
                                });
                            }

                            stocks.Add(new GetStockBySkusResponse
                            {
                                Sku = sku,
                                Stocks = stockItems
                            });
                        }
                    }
                }

                return stocks;
            }
        }
    }
}
