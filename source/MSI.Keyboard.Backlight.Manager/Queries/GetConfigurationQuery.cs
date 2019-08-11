using MediatR;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;

namespace MSI.Keyboard.Backlight.Manager.Queries
{
    public class GetConfigurationQuery : IRequest<JobsConfiguration>
    {
    }
}
