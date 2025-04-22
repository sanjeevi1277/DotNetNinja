using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public CustomAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request path matches a protected route
        var path = context.Request.Path.Value;

        // For demonstration, we only check for the token on routes that start with '/api/protected'
        if (path.StartsWith("/api/protected", StringComparison.OrdinalIgnoreCase))
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]); // Your secret key

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // Validate issuer
                        ValidateAudience = true, // Validate audience
                        ValidateLifetime = true, // Validate expiration
                        ValidateIssuerSigningKey = true, // Validate secret key

                        ValidIssuer = _configuration["Jwt:Issuer"], // Issuer to validate against
                        ValidAudience = _configuration["Jwt:Audience"], // Audience to validate against
                        IssuerSigningKey = new SymmetricSecurityKey(key) // Secret key to validate the token signature
                    };

                    // Validating the token
                    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                    // If it's a valid JWT, extract claims
                    if (validatedToken is JwtSecurityToken jwtSecurityToken)
                    {
                        var userId = jwtSecurityToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        if (userId != null)
                        {
                            // Attach the authenticated user to the context
                            context.User = principal;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized - Invalid Token");
                        return;
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized - Error Processing Token");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Bad Request - Token Not Found");
                return;
            }
        }

        // Proceed to the next middleware
        await _next(context);
    }
}
