using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class MetricsPipelineProcessor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IAnalyticsService _analyticsService;

        public MetricsPipelineProcessor(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (_analyticsService.TrackTimedEvent(request.GetType().Name))
            {
                return await next();
            }
        }
    }
}
