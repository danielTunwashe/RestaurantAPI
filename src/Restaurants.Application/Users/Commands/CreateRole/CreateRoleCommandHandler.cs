using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly ILogger<CreateRoleCommandHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleCommandHandler(ILogger<CreateRoleCommandHandler> logger, UserManager<User> userManager, IUserStore<User> userStore, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _userStore = userStore;
            _roleManager = roleManager;
        }
        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating Role with Role name: {request.Name}");

            var role = await _roleManager.FindByNameAsync(request.Name);
            if (role == null)
            {
                var identityRole = new IdentityRole(request.Name);
                await _roleManager.CreateAsync(identityRole);
            }
            else
            {
                throw new ConflictException(nameof(IdentityRole),request.Name);
            }

           

        }
    }
}
