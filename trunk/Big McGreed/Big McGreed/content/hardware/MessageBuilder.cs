using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.hardware
{
    //Definieer het type van de delegate
    //-Double, Integer, void etc.
    public delegate void BufferedCommandHandler(String command);

    public class MessageBuilder
    {

        public event BufferedCommandHandler bufferedCommandHandler;

        //Specificeer de methode...
        //-Return type
        //-Paremeters
        protected void CommandHandler(String command)
        {
            if (bufferedCommandHandler != null) //Als er events zijn geregistreerd, execute deze dan.
            {
                bufferedCommandHandler(command);
            }   
        }

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
                if (buffer.StartsWith(ArduinoConstants.COMMAND_START) && buffer.EndsWith(ArduinoConstants.COMMAND_END))
                {
                    CommandHandler(buffer); //Execute the registered events.
                    buffer = ""; //Reset the buffer.
                }
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

        private string ToString()
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
