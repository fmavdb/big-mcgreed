using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.hardware
{
    /// <summary>
    /// Simple static class that holds all constant values of the Arduino Hardware.
    /// </summary>
    public static class ArduinoConstants
    {
        //Integers
        public const int CONNECTION_SPEED = 38400;
        public const int LCD_PIN = 5; //Maar een voorbeeld.
        public const int DATA_READING_INTERVAL = 50; //In ms
        public const int DATA_WRITING_INTERVAL = 50; //In ms

        //Strings
        public const string BOTTOM_LED_AAN = "SET_BOTTOM_LED_ON"; //Voorbeeld
        public const string COM_POORT = "COM13";
        public const string COMMAND_START = "#";
        public const string COMMAND_END = "%";
    }
}
