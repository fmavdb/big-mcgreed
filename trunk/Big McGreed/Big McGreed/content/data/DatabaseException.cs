using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.data
{
    public class DatabaseException : Exception
    {
        /// <summary>
        /// Constructs the exception with a specified message.
        /// </summary>
        /// <param name="message">A brief message to explain the exception.</param>
        public DatabaseException(string message) : base(message) { }
        /// <summary>
        /// Constructs the exception with a specified message and inner exception.
        /// </summary>
        /// <param name="message">A brief message to explain the exception.</param>
        /// <param name="innerException">Exception data that has been caught.</param>
        public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
