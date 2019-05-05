// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// Driver for the 12-channel 16bit TLC59711 PWM/LED
    /// </summary>
    public interface ITlc59711 
    {
        /// <summary>
        /// A chained cluster of TLC59711 devices. 
        /// </summary>
        ITlc59711Chain Devices { get; }

        /// <summary>
        /// Creates a TLC59711 command and sends it to the first device using the SPI bus.
        /// </summary>
        void Update();
    }
}
