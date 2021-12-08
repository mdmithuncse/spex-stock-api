using AutoMapper;
using DomainModel;
using MediatR;
using Pagination;
using Pagination.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.LocationQuery
{
    public class GetAllLocationQuery : IRequest<PagedResult<LocationResponse>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, PagedResult<LocationResponse>>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetAllLocationQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedResult<LocationResponse>> Handle(GetAllLocationQuery query, CancellationToken cancellationToken)
            {
                var result = await _context.Locations.GetPagedItemsAsync(query.Page, query.PageSize);

                if (result == null || !result.Items.Any())
                {
                    return default;
                }

                return new PagedResult<LocationResponse>
                {
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    PageCount = result.PageCount,
                    RowCount = result.RowCount,
                    Items = _mapper.Map<IList<LocationResponse>>(result.Items)
                };
            }
        }
    }
}
