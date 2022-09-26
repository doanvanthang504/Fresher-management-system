using Microsoft.AspNetCore.Http;

namespace Global.Shared.Helpers
{
    public class ResponseValidator
    {
        public static bool IsSuccessfulResponse(int statusCode)
        {
            return statusCode >= StatusCodes.Status200OK && statusCode < StatusCodes.Status300MultipleChoices;
        }
    }
}
