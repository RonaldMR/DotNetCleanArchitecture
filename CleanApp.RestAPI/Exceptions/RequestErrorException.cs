namespace CleanApp.RestAPI.Exceptions
{
    public class RequestErrorException: Exception
    {
        public RequestErrorException(string[] errors): base(string.Join(",", errors)) 
        { 

        }
    }
}
