// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// A chained cluster of TLC59711 devices
    /// </summary>
    /// <remarks>
    /// The devices should be connected together with their SDTI/SDTO pins.
    /// </remarks>
    internal sealed class Tlc59711Chain : ITlc59711Chain
    {
        private const int CommandSize = Tlc59711Chip.CommandSize;
        private readonly Tlc59711Chip[] _devices;

        /// <summary>
        /// Creates a new instance of the <see cref="Tlc59711Chain"/> class.
        /// </summary>
        /// <param name="sharedMemory">Memory to work with.</param>
        /// <param name="numberOfDevices">Number of <see cref="ITlc59711Chip"/>s connected together.</param>
        /// <param name="initialSettings">Initial chip settings</param>
        public Tlc59711Chain(byte[] sharedMemory, int numberOfDevices, ITlc59711Settings initialSettings = null)
        {
            _devices = CreateDevices(sharedMemory, numberOfDevices, initialSettings);
            Channels = new Tlc59711ChainChannels(_devices);
        }

        /// <inheritdoc/>
        public int Count => _devices.Length;

        /// <inheritdoc/>
        public ITlc59711Chip this[int index] => _devices[index];

        /// <inheritdoc/>
        public IPwmChannels Channels { get; }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc/>
        public IEnumerator<ITlc59711Chip> GetEnumerator() => ((IEnumerable<ITlc59711Chip>)_devices).GetEnumerator();

        /// <inheritdoc/>
        public ITlc59711Chip Get(int index) => _devices[index];

        /// <inheritdoc/>
        public void Blank(bool blank)
        {
            foreach (var device in _devices)
            {
                device.Blank = blank;
            }
        }

        private static Tlc59711Chip[] CreateDevices(byte[] memory, int numberOfDevices, ITlc59711Settings settings) =>
            Enumerable
                .Range(0, numberOfDevices)
                .Select(idx => new Tlc59711Chip(memory, idx * CommandSize, settings))
                .ToArray();
    }
}
