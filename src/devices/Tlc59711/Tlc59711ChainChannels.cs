// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// The PWM channels of a TLC59711 device chain
    /// </summary>
    internal sealed class Tlc59711ChainChannels : IPwmChannels
    {
        private struct Mapping
        {
            public ITlc59711Chip Device { get; }
            public int ChannelIndex { get; }

            public Mapping(ITlc59711Chip device, int channelIndex) {
                Device = device;
                ChannelIndex = channelIndex;
            }
        }

        private readonly Mapping[] _deviceMap;

        /// <summary>
        /// Creates a new instance of the <see cref="Tlc59711ChainChannels"/> class.
        /// </summary>
        /// <param name="devices">TLC59711 devices</param>
        public Tlc59711ChainChannels(IEnumerable<ITlc59711Chip> devices)
        {
            _deviceMap = devices
                .SelectMany(device => Enumerable
                    .Range(0, device.Channels.Count)
                    .Select(idx => new Mapping(device, idx)))
                .ToArray();
        }

        /// <inheritdoc/>
        public ushort this[int index] {
            get => Get(index);
            set => Set(index, value);
        }
        /// <inheritdoc/>
        public int Count => _deviceMap.Length;

        /// <inheritdoc/>
        public ushort Get(int index)
        {
            var mapping = _deviceMap[index];
            return mapping.Device.Channels[mapping.ChannelIndex];
        }

        /// <inheritdoc/>
        public void Set(int index, ushort value) {
            var mapping = _deviceMap[index];
            mapping.Device.Channels[mapping.ChannelIndex] = value;
        }
    }
}
