using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Big_McGreed.content.hardware
{
    /// <summary>
    /// Main Arduino Communication Manager class. Handles all in/out communication.
    /// </summary>
    public class ArduinoManager
    {
        private ArduinoCom com;
        private Thread receiveMessagesThread;
        private MessageBuilder messageBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArduinoManager"/> class.
        /// </summary>
        public ArduinoManager()
        {
            com = new ArduinoCom();
            messageBuilder = new MessageBuilder();
            receiveMessagesThread = new Thread(new ThreadStart(run));
            receiveMessagesThread.Priority = ThreadPriority.AboveNormal;     
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void stop()
        {
            if (com.port.IsOpen)
            {
                com.port.Close();
            }
        }

        /// <summary>
        /// Create a connection with the Arduino.
        /// </summary>
        /// <returns></returns>
        public bool connect()
        {
            try
            {
                com.port.Open();
                if (com.port.IsOpen)
                {
                    com.port.DiscardInBuffer();
                    com.port.DiscardOutBuffer();
                    Console.WriteLine("Successfully connected to the Arduino.");
                    sendMessage();
                    return true;
                }
                return false;
            } 
            catch (IOException e) 
            {
                Console.WriteLine(new ArduinoException(e.Message)); //Prevent the game from crashing while connecting
                return false;
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        public void sendMessage()
        {
            //Zo verstuur je een bericht NAAR de Arduino!
            if (true) //Iets is true... 
            {
                sendMessage(MessageBuilder.AppendCommand(ArduinoConstants.BOTTOM_LED_AAN));
            }
        }

        /// <summary>
        /// Receives the message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void receiveMessage(string message)
        {
            message = message.Substring(1, message.Length - 1); //Haal command start en end van de string af.
            switch (message)
            {
                case ArduinoConstants.BOTTOM_LED_AAN:
                    //Doe iets...
                    break;
                default:
                    Console.WriteLine(new ArduinoException("Arduino message: " + message + " is unhandled."));
                    break;
            }
        }


        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private bool sendMessage(string message)
        {
            if (com.port.IsOpen)
            {
                try
                {
                    com.port.Write(message);
                    return true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(new ArduinoException(exception.Message)); //Prevent the game from crashing while writing
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Processes the received messages.
        /// </summary>
        public void processReceivedMessages()
        {
            string message = messageBuilder.ToString();
            while (message != null)
            {
                receiveMessage(message);
                message = messageBuilder.ToString();
            }
        }

        /// <summary>
        /// This runs every DATA_READING_INTERVAL milliseconds. 
        /// </summary>
        protected void run()
        {
            while (com.port.IsOpen)
            {
                if (com.port.BytesToRead > 0)
                {
                    try
                    {
                        string dataFromSocket = com.port.ReadExisting();
                        messageBuilder.Append(dataFromSocket);
                        processReceivedMessages();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(new ArduinoException(e.Message));
                    }
                }
                System.Threading.Thread.Sleep(ArduinoConstants.DATA_READING_INTERVAL);
            }
        }
    }
}
