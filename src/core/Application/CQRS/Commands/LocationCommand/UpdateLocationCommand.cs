using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.LocationCommand
{
    public class UpdateLocationCommand : IRequest<int>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        public class UpdateLocationCommandHandler:  IRequestHandler<UpdateLocationCommand, int>
        {
            private readonly IAppDbContext _context;
            private readonly ILogger<UpdateLocationCommandHandler> _logger;

            public UpdateLocationCommandHandler(IAppDbContext context, ILogger<UpdateLocationCommandHandler> logger)
            { 
                _context = context;
                _logger = logger;
            }

            public async Task<int> Handle(UpdateLocationCommand command, CancellationToken cancellationToken)
            {
                var location = await _context.Locations.Where(x => x.Id == command.Id).FirstOrDefaultAsync();

                if (location == null)
                {
                    return default;
                }

                location.Location = command.Location;
                location.Updated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Data updated successfully with Id: {location.Id}");

                return location.Id;
            }
        }
    }
}
