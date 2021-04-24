using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebStore.Infrastructure.Middleware
{
    public class ErrorHadlingMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ErrorHadlingMiddleware> _Logger;

        public ErrorHadlingMiddleware(RequestDelegate Next, ILogger<ErrorHadlingMiddleware> Logger)
        {
            _Next = Next;
            _Logger = Logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next(context);
            }
            catch (Exception error)
            {
                HandleException(error, context);
                throw;
            }
        }

        private void HandleException(Exception error, HttpContext context)
        {
            _Logger.LogError(error, "Ошибка при обработке запроса к {0}", context.Request.Path);
        }
    }
}
