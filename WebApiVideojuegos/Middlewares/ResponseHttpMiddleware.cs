namespace WebApiVideojuegos.Middlewares
{
    public static class MiddlewareExtensions//creamos una extension 
    {
        public static IApplicationBuilder UseResponseHttpMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ResponseHttpMiddleware>();
        }
    }
    public class ResponseHttpMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ResponseHttpMiddleware> logger;

        public ResponseHttpMiddleware(RequestDelegate next,
            ILogger<ResponseHttpMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            using (var ms = new MemoryStream())
            {
                var bodyOriginal = context.Response.Body;
                context.Response.Body = ms;
                await next(context);
                ms.Seek(0, SeekOrigin.Begin);
                string response = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(bodyOriginal);
                context.Response.Body = bodyOriginal;

                logger.LogInformation(response);
            }
        }
    }
}