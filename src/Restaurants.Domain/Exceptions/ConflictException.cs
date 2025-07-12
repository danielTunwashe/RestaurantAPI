namespace Restaurants.Domain.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string resourceType, string resourceIdentifier) : base($"{resourceType} with name: {resourceIdentifier} already exist")
        {
            
        }
    }
}
