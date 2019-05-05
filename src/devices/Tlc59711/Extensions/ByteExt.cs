using System;

namespace Iot.Device.Tlc59711.Extensions
{
    internal static class ByteExt
    {
     
        public const byte BrightnessControlMax = 127;
     
        public static void ThrowOnInvalidBrightnessControl(this byte value)
        {
            if (value <= BrightnessControlMax) return;
            
            var message = string.Format(
                "The maximum value for brightness control is {0}. You set a value of {1}.", 
                BrightnessControlMax, 
                value);

            throw new ArgumentException(message, nameof(value));
        }
    }
}
