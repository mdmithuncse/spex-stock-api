using AutoMapper;
using DomainModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.LocationQuery
{
    public class GetLocationByIdQuery : IRequest<LocationResponse>
    {
        public int Id { get; set; }

        public class GetLocationByIdQueryHandler: IRequestHandler<GetLocationByIdQuery, LocationResponse>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetLocationByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LocationResponse> Handle(GetLocationByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _context.Locations.Where(x => x.Id == query.Id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return default;
                }

                return _mapper.Map<LocationResponse>(item);
            }
        }
    }
}
