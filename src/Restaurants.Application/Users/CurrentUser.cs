namespace Restaurants.Application.Users
{
    public record CurrentUser(string Id, string Email, IEnumerable<string> Roles,string? Nationality, DateOnly? DateOfBirth) 
    {
        //To access the roles of a particular user or IdentityUser
        public bool IsInRole(string role) => Roles.Contains(role);  
    }
}
