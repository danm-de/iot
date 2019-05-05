// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Iot.Device.Tlc59711.Extensions;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// TLC59711 settings
    /// </summary>
    public class Tlc59711Settings : ITlc59711Settings
    {
        private byte _brightnessControlR = 127;
        private byte _brightnessControlG = 127;
        private byte _brightnessControlB = 127;

        /// <inheritdoc/>
        public bool Blank { get; set; } = true;

        /// <inheritdoc/>
        public bool DisplayRepeatMode { get; set; } = true;

        /// <inheritdoc/>
        public bool DisplayTimingResetMode { get; set; } = true;

        /// <inheritdoc/>
        public bool ReferenceClock { get; set; }

        /// <inheritdoc/>
        public bool ReferenceClockEdge { get; set; } = true;

        /// <inheritdoc/>
        public byte BrightnessControlR
        {
            get => _brightnessControlR;
            set
            {
                value.ThrowOnInvalidBrightnessControl();
                _brightnessControlR = value;
            }
        }

        /// <inheritdoc/>
        public byte BrightnessControlG
        {
            get => _brightnessControlG;
            set
            {
                value.ThrowOnInvalidBrightnessControl();
                _brightnessControlG = value;
            }
        }

        /// <inheritdoc/>
        public byte BrightnessControlB
        {
            get => _brightnessControlB;
            set
            {
                value.ThrowOnInvalidBrightnessControl();
                _brightnessControlB = value;
            }
        }
    }
}
