// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// The PWM channels of a single TLC59711 device
    /// </summary>
    internal sealed class Tlc59711Channels : IPwmChannels
    {
        private const int NumberOfChannels = 12;

        private readonly byte[] _memory;
        private readonly int _offset;

        /// <summary>
        /// Creates a new instance of the <see cref="Tlc59711Channels"/> class.
        /// </summary>
        /// <param name="memory">Memory to work with</param>
        /// <param name="offset">Byte offset to the first channel index</param>
        public Tlc59711Channels(byte[] memory, int offset) {
            _memory = memory;
            _offset = offset;
        }

        /// <inheritdoc/>
        public int Count => NumberOfChannels;

        /// <inheritdoc/>
        public ushort this[int index] {
            get => Get(index);
            set => Set(index, value);
        }

        /// <inheritdoc/>
        public ushort Get(int index) {
            ThrowOnInvalidChannelIndex(index);
            
            var offset = _offset + (index * 2);
            var high = _memory[offset];
            var low = _memory[offset + 1];
            return unchecked((ushort)((high << 8) | low));
        }

        /// <inheritdoc/>
        public void Set(int index, ushort value) {
            ThrowOnInvalidChannelIndex(index);
            
            var position = _offset + (index * 2);
            _memory[position] = unchecked((byte)(value >> 8));
            _memory[position + 1] = unchecked((byte)value);
        }

        private static void ThrowOnInvalidChannelIndex(int index) {
            if (index >= 0 && index < NumberOfChannels) {
                return;
            }

            var message = string.Format("The index must be greater or equal than 0 and lower than {0}.", NumberOfChannels);
            throw new ArgumentOutOfRangeException(nameof(index), index, message);
        }
    }
}
