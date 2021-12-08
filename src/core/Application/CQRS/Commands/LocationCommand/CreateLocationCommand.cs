using DataModel;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.LocationCommand
{
    public class CreateLocationCommand : IRequest<int>
    {
        [Required]
        public string Location { get; set; }

        public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, int>
        {
            private readonly IAppDbContext _context;
            private readonly ILogger<CreateLocationCommandHandler> _logger;

            public CreateLocationCommandHandler(IAppDbContext context, ILogger<CreateLocationCommandHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<int> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
            {
                var location = new LocationModel
                {
                    Location = command.Location
                };

                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Data saved successfully with Id: {location.Id}");

                return location.Id;
            }
        }
    }
}
