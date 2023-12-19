namespace ApiDemoFilms.MiddleWare;
using Films.DAL.InterfacesServices;
public class JwtMiddleWare
{
    private readonly RequestDelegate _next;
    //private readonly AppSettings _appSettings;
    private readonly ITokenService _tokenService;

    public JwtMiddleWare(RequestDelegate next, ITokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await attachUserToContext(context, token);

        await _next(context);
    }

    private async Task attachUserToContext(HttpContext context, string token)
    {
        try
        {
            context.Items["User"] = await _tokenService.ValidateTokenAsync(token);
        }
        catch
        {
            //throw new AuthorizeException("Not authorized");
        }
    }
}