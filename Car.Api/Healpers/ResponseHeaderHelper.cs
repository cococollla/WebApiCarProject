namespace CarWebService.API.Healpers
{
    public class ResponseHeaderHelper
    {
        public static void AddToResponseHeader(HttpContext httpContext, int id)
        {
            if (httpContext == null)
            {
                httpContext.Response.Headers.Add("Location", $"/api/Car/GetUserById/{id}");
            }
        }
    }
}
