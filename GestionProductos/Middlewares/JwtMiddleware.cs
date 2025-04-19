namespace GestionProductos.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //if (token != null || (!context.User.Identity?.IsAuthenticated ?? false))
            //{
            if (!context.User.Identity?.IsAuthenticated ?? false) { 
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            // Aquí podrías hacer más validaciones, como chequear lista negra.
            await _next(context);
        }
    }
}
