// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// A chained cluster of TLC59711 devices. 
    /// </summary>
    /// <remarks>
    /// The devices should be connected together by their SDTI/SDTO pins.
    /// </remarks>
    public interface ITlc59711Chain : IEnumerable<ITlc59711Chip>, IPwmDevice
    {
        /// <summary>
        /// Number of TLC59711 devices chained together
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the TLC59711 device at the requested position
        /// </summary>
        /// <param name="index">TLC59711 index</param>
        /// <returns>TLC59711 device</returns>
        ITlc59711Chip this[int index] { get; }

        /// <summary>
        /// Returns the TLC59711 device at the requested position
        /// </summary>
        /// <param name="index">TLC59711 index</param>
        /// <returns>TLC59711 device</returns>
        ITlc59711Chip Get(int index);

        /// <summary>
        /// Set BLANK on/off at all connected devices.
        /// </summary>
        /// <param name="blank">If set to <c>true</c> all outputs are forced off.</param>
        void Blank(bool blank);
    }
}
