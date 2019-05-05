// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Device.Spi;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// Driver for the 12-channel 16bit TLC59711 PWM/LED
    /// </summary>
    public sealed class Tlc59711 : ITlc59711, IDisposable
    {
        private readonly Tlc59711Chain _deviceChain;
        private readonly SpiDevice _spiDevice;
        private readonly bool _isOwner;
        private readonly byte[] _memory;

        /// <summary>
        /// Creates a new instance of the <see cref="Tlc59711"/> class and initializes it.
        /// </summary>
        /// <param name="spiDevice">A open SPI connection</param>
        /// <param name="numberOfDevices">Number of TLC59711 chips connected together.</param>
        /// <param name="initialSettings">Initial TLC59711 chip settings</param>
        /// <param name="initializeSpiDevice">If <c>true</c> the SPI device will be initialized with default transfers settings.</param>
        /// <param name="isOwner">If <c>true</c> the SPI device will be disposed on <see cref="Dispose"/></param>
        public Tlc59711(
            SpiDevice spiDevice,
            int numberOfDevices = 1,
            Tlc59711Settings initialSettings = null,
            bool initializeSpiDevice = true,
            bool isOwner = true)
        {
            if (numberOfDevices <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfDevices), "You need at least one device.");
            }

            _spiDevice = spiDevice ?? throw new ArgumentNullException(nameof(spiDevice));
            _isOwner = isOwner;

            if (initializeSpiDevice)
            {
                spiDevice.ConnectionSettings.Mode = SpiMode.Mode0;
                spiDevice.ConnectionSettings.DataBitLength = 8;
                spiDevice.ConnectionSettings.ClockFrequency = 10000000; // 10Mhz
            }

            var size = numberOfDevices * Tlc59711Chip.CommandSize;
            _memory = new byte[size];
            _deviceChain = new Tlc59711Chain(_memory, numberOfDevices, initialSettings);
        }

        /// <inheritdoc/>
        public ITlc59711Chain Devices => _deviceChain;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_isOwner)
            {
                _spiDevice.Dispose();
            }
        }

        /// <inheritdoc/>
        public void Update() => _spiDevice.Write(_memory);
    }
}
