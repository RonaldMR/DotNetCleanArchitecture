using CleanApp.Application.Constants;

namespace CleanApp.Application.Exceptions
{
    public class RelationDoesNotExists: Exception
    {
        public RelationDoesNotExists(string entity): base(string.Format(ExceptionConstants.NOT_EXISTS, entity)) 
        { 
        
        }
    }
}
