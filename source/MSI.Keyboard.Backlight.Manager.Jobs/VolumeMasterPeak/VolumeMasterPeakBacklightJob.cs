using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using MSI.Keyboard.Backlight.Configuration;
using MSI.Keyboard.Backlight.Service;
using NAudio.CoreAudioApi;

namespace MSI.Keyboard.Backlight.Manager.Jobs.DeviceMasterPeak
{
    public class VolumeMasterPeakBacklightJob : BaseBacklightJob
    {
        public override TimeSpan RefreshInterval => TimeSpan.FromMilliseconds(5);

        public VolumeMasterPeakBacklightJob(
            IKeyboardService keyboardService, 
            IBacklightConfigurationBuilder backlightConfigurationBuilder) 
            : base(keyboardService, backlightConfigurationBuilder)
        {
            
        }

        protected override BacklightConfiguration Configure(IBacklightConfigurationBuilder builder)
        {
            var deviceEnumeration = new MMDeviceEnumerator();
            var devices = deviceEnumeration.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            var device = devices.FirstOrDefault();
            var mpv = device.AudioMeterInformation.MasterPeakValue;

            mpv = NormalizeMpv(mpv);
            var color = GetColor(mpv);
            var intensity = GetIntensity(mpv);

            return builder.ForAllRegions(color, intensity).Build();
        }

        private float NormalizeMpv(float mpv)
        {
            if (mpv <= 0.005f)
                return 0;

            var normalizedMpv = mpv * 100f / 0.95f;

            if (normalizedMpv >= 100f)
                normalizedMpv = 100;

            return normalizedMpv;
        }

        private int GetIntensity(float mpv)
        {
            if (mpv == 0f)
                return Intensity;

            var value = Math.Pow(2.1, mpv / 10f) * (Intensity / 100f);

            if (value >= 100f)
                value = 100f;

            var intValue = (int)Math.Round(value, 0);

            return intValue;
        }

        private Color GetColor(float mpv)
        {
            if(mpv == 0f)
            {
                return Color.Red;
            }

            var colorSumValue = (int)Math.Round(mpv * (255 + 255 + 255) * 1.15 / 100, 0);

            Debug.WriteLine(colorSumValue);

            var rgb = new int[3];

            for(var i = 0; i < rgb.Length; i++)
            {
                if(colorSumValue >= 255)
                {
                    rgb[i] = 255;
                    colorSumValue -= 255;
                }
                else if(colorSumValue > 0)
                {
                    rgb[i] = colorSumValue;
                    colorSumValue = 0;
                }
            }

            return Color.FromArgb(0, rgb[0], rgb[1], rgb[2]);
        }
    }
}
