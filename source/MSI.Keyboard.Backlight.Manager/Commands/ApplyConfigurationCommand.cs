using MediatR;
using MSI.Keyboard.Backlight.Manager.Jobs.Models;

namespace MSI.Keyboard.Backlight.Manager.Commands
{
    public class ApplyConfigurationCommand : IRequest
    {
        public JobsConfiguration Configuration { get; }

        public ApplyConfigurationCommand(JobsConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}