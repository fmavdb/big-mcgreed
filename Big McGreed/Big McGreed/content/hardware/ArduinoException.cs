using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.hardware
{
    public class ArduinoException : Exception
    {

        /// <summary>
        /// Constructs the exception with a specified message.
        /// </summary>
        /// <param name="message">A brief message to explain the exception.</param>
        public ArduinoException(string message) : base(message) { }

        /// <summary>
        /// Constructs the exception with a specified message and inner exception.
        /// </summary>
        /// <param name="message">A brief message to explain the exception.</param>
        /// <param name="innerException">Exception data that has been caught.</param>
        public ArduinoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
