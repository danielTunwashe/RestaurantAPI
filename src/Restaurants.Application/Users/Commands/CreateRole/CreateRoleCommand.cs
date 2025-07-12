using MediatR;


namespace Restaurants.Application.Users.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest
    {
        public string Name { get; set; } = default!;
    }
}
