using AppCenter.Analytics.Metrics;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MSI.Keyboard.Backlight.Manager.Analytics
{
    public class MetricsPipelineProcessor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (AnalyticsMetrics.TrackTimedEvent(request.GetType().Name))
            {
                return await next();
            }
        }
    }
}
