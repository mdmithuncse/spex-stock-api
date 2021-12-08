using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.LocationCommand
{
    public class DeleteLocationByIdCommand : IRequest<int>
    {
        [Required]
        public int Id { get; set; }

        public class DeleteLocationByIdCommandHandler : IRequestHandler<DeleteLocationByIdCommand, int>
        {
            private readonly IAppDbContext _context;
            private readonly ILogger<DeleteLocationByIdCommandHandler> _logger;

            public DeleteLocationByIdCommandHandler(IAppDbContext context, ILogger<DeleteLocationByIdCommandHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<int> Handle(DeleteLocationByIdCommand command, CancellationToken cancellationToken)
            {
                var location = await _context.Locations.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (location == null)
                {
                    return default;
                }

                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Data deleted successfully with Id: {location.Id}");

                return location.Id;
            }
        }
    }
}
