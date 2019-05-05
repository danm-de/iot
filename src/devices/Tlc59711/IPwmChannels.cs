// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// The Pulse-width modulation (PWM) channels.
    /// </summary>
    public interface IPwmChannels
    {
        /// <summary>
        /// Indexer, which will allow client code to use [] notation on the class instance itself to modify PWM channel values.
        /// </summary>
        /// <param name="index">channel index</param>
        /// <returns>The current PWM value from <paramref name="index"/></returns>
        ushort this[int index] { get; set; }

        /// <summary>
        /// Returns the number of channels.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the PWM value at the specified channel <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Channel index</param>
        /// <returns>The PWM value at the specified channel <paramref name="index"/></returns>
        ushort Get(int index);

        /// <summary>
        /// Sets the PWM value at channel <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Channel index</param>
        /// <param name="value">The PWM value</param>
        void Set(int index, ushort value);
    }
}
