using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Big_McGreed.content.hardware
{ 
    /// <summary>
    /// Simple class holding any connection information that will get used in the manager.
    /// </summary>
    public class ArduinoCom
    {
        /// <summary>
        /// Gets the port.
        /// </summary>
        public SerialPort port { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ArduinoCom"/> class.
        /// </summary>
        public ArduinoCom()
        {
            port = new SerialPort();
            port.BaudRate = ArduinoConstants.CONNECTION_SPEED;
            port.PortName = ArduinoConstants.COM_POORT;
        }
    }
}
