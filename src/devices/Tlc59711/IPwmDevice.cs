// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// A pulse-width modulation (PWM) device
    /// </summary>
    public interface IPwmDevice 
    {
        /// <summary>
        /// The PWM channels
        /// </summary>
        IPwmChannels Channels { get; }
    }
}
