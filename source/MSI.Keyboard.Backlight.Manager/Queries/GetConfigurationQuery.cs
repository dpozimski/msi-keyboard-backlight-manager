﻿using MediatR;

namespace MSI.Keyboard.Backlight.Manager.Queries
{
    public class GetConfigurationQuery : IRequest<BacklightConfiguration>
    {
    }
}
