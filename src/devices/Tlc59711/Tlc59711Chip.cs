// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Iot.Device.Tlc59711.Extensions;

namespace Iot.Device.Tlc59711
{
    /// <summary>
    /// A single TLC59711 device
    /// </summary>
    internal sealed class Tlc59711Chip : ITlc59711Chip
    {
        public const int CommandSize = 28;
        private const byte MagicWord = 0x25 << 2;
        private const byte OUTTMG = 1 << 1;
        private const byte EXTGCK = 1;
        private const byte TMGRST = 1 << 7;
        private const byte DSPRPT = 1 << 6;
        private const byte BLANK = 1 << 5;
        private const byte OFF = 0;
        private const int DataOffset = 4;

        private readonly byte[] _memory;
        private readonly int _memoryOffset;
        private readonly ITlc59711Settings _initSettings;

        private byte _referenceClockEdge = OUTTMG;
        private byte _referenceClock = OFF;
        private byte _displayTimingResetMode = TMGRST;
        private byte _displayRepeatMode = DSPRPT;
        private byte _blank = BLANK;
        private byte _bcb = 127;
        private byte _bcg = 127;
        private byte _bcr = 127;

        /// <summary>
        /// Creates a new instance of the <see cref="Tlc59711Chip"/> class.
        /// </summary>
        /// <param name="initialSettings">Initial TLC59711 settings</param>
        /// <param name="memory">Memory used to write chip state</param>
        /// <param name="memoryOffset"></param>
        public Tlc59711Chip(byte[] memory, int memoryOffset, ITlc59711Settings initialSettings = null) {
            _initSettings = initialSettings;
            _memory = memory;
            _memoryOffset = memoryOffset;
            Channels = new Tlc59711Channels(memory, memoryOffset + DataOffset);

            Reset();
        }

        /// <inheritdoc/>
        public bool Blank {
            get => _blank == BLANK;
            set {
                _blank = (value) ? BLANK : OFF;
                WriteSecondByte();
            }
        }

        /// <inheritdoc/>
        public bool DisplayRepeatMode {
            get => _displayRepeatMode == DSPRPT;
            set {
                _displayRepeatMode = (value) ? DSPRPT : OFF;
                WriteSecondByte();
            }
        }

        /// <inheritdoc/>
        public bool DisplayTimingResetMode {
            get => _displayTimingResetMode == TMGRST;
            set {
                _displayTimingResetMode = (value) ? TMGRST : OFF;
                WriteSecondByte();
            }
        }

        /// <inheritdoc/>
        public bool ReferenceClock {
            get => _referenceClock == EXTGCK;
            set {
                _referenceClock = (value) ? EXTGCK : OFF;
                WriteFirstByte();
            }
        }

        /// <inheritdoc/>
        public bool ReferenceClockEdge {
            get => _referenceClockEdge == OUTTMG;
            set {
                _referenceClockEdge = (value) ? OUTTMG : OFF;
                WriteFirstByte();
            }
        }

        /// <inheritdoc/>
        public byte BrightnessControlR {
            get => _bcr;
            set {
                value.ThrowOnInvalidBrightnessControl();
                
                _bcr = value;
                WriteFourthByte();
            }
        }

        /// <inheritdoc/>
        public byte BrightnessControlG {
            get => _bcg;
            set { 
                value.ThrowOnInvalidBrightnessControl();
                
                _bcg = value;
                WriteThirdByte();
                WriteFourthByte();
            }
        }

        /// <inheritdoc/>
        public byte BrightnessControlB {
            get => _bcb;
            set {
                value.ThrowOnInvalidBrightnessControl();

                _bcb = value;
                WriteSecondByte();
                WriteThirdByte();
            }
        }

        /// <inheritdoc/>
        public IPwmChannels Channels { get; }

        /// <inheritdoc/>
        public void Reset()
        {
            Initialize(_initSettings ?? new Tlc59711Settings());
        }

        private void Initialize(ITlc59711Settings settings) {
            _referenceClockEdge = settings.ReferenceClockEdge ? OUTTMG : OFF;
            _referenceClock = settings.ReferenceClock ? EXTGCK : OFF;
            _displayTimingResetMode = settings.DisplayTimingResetMode ? TMGRST : OFF;
            _displayRepeatMode = settings.DisplayRepeatMode ? DSPRPT : OFF;
            _blank = settings.Blank ? BLANK : OFF;
            _bcb = settings.BrightnessControlB;
            _bcg = settings.BrightnessControlG;
            _bcr = settings.BrightnessControlR;

            WriteFirstByte();
            WriteSecondByte();
            WriteThirdByte();
            WriteFourthByte();
        }

        private void WriteFirstByte() {
            _memory[_memoryOffset + 0] = (byte) (MagicWord | _referenceClockEdge | _referenceClock);
        }

        private void WriteSecondByte() {
            _memory[_memoryOffset + 1]= (byte) (_displayTimingResetMode | _displayRepeatMode | _blank | (_bcb >> 2));
        }

        private void WriteThirdByte() {
            _memory[_memoryOffset + 2] = (byte) ((_bcb << 6) | (_bcg >> 1));
        }

        private void WriteFourthByte() {
            _memory[_memoryOffset + 3] = (byte) ((_bcg << 7) | _bcr);
        }
    }
}
