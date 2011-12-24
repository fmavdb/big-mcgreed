using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.hardware
{
    public class MessageBuilder
    {
        private string buffer;
           
        public MessageBuilder()
        {
            buffer = "";
        }

        public void Append(string data)
        {
            if (data != null)
            {
                buffer += data;
            }
        }

        public static string AppendCommand(string data)
        {
            string buffer = "";
            if (data != null)
            {
                buffer += ArduinoConstants.COMMAND_START;
                buffer += data;
                buffer += ArduinoConstants.COMMAND_END;
            }
            return buffer;
        }

        public override string ToString()
        {
            int beginIndex = buffer.IndexOf(ArduinoConstants.COMMAND_START);
            if (beginIndex != -1)
            {
                int endIndex = buffer.IndexOf(ArduinoConstants.COMMAND_END, beginIndex);
                if (endIndex != -1)
                {
                    string foundMessage = buffer.Substring(beginIndex, (endIndex - beginIndex) + 1);
                    buffer = buffer.Substring(endIndex + 1);
                    return foundMessage;
                }
            }
            return null;
        }
    }
}
