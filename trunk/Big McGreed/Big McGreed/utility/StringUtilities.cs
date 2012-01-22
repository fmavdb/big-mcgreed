using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.utility
{
    public static class StringUtilities
    {
        /// <summary>
        /// This static array contains valid characters.
        /// </summary>
        private static char[] validChars = {
		    '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 
		    'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 
		    't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', 
		    '3', '4', '5', '6', '7', '8', '9'
	    };

        /// <summary>
        /// Formats a given name for display.
        /// </summary>
        /// <param name="name">The name to format.</param>
        /// <returns>Returns a formatted string.</returns>
        public static string FormatForDisplay(this string name)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name).Replace('_', ' ');
        }
    }
}
