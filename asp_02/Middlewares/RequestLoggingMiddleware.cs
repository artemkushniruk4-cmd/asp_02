using System.Diagnostics;

namespace asp_02.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            // 1. Логуємо початок запиту
            _logger.LogInformation("Прийшов запит: {Method} {Path}", request.Method, request.Path);

            // Передаємо запит далі по конвеєру (до контролерів тощо)
            await _next(context);

            stopwatch.Stop();
            var response = context.Response;

            // 2. Логуємо завершення відповіді та час виконання в мілісекундах
            _logger.LogInformation("Відправилася відповідь: {Method} {Path} повернув статус {StatusCode}. Зайняло: {ElapsedMs} ms",
                request.Method, request.Path, response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
    }
}
