// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// A single TLC59711 device
    /// </summary>
    public interface ITlc59711Chip : IPwmDevice, ITlc59711Settings
    {
        /// <summary>
        /// Initializes the device with default values.
        /// </summary>
        void Reset();
    }
}
