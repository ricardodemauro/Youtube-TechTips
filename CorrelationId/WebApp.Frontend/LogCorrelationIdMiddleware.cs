using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Frontend
{
    public class LogCorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICorrelationContextAccessor _correlationContext;

        public LogCorrelationIdMiddleware(RequestDelegate next, ICorrelationContextAccessor correlationContext)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _correlationContext = correlationContext ?? throw new ArgumentNullException(nameof(correlationContext));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = _correlationContext.CorrelationContext.CorrelationId;
            if (!string.IsNullOrEmpty(correlationId))
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<LogCorrelationIdMiddleware>>();
                using (logger.BeginScope("{@CorrelationId}", correlationId))
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
