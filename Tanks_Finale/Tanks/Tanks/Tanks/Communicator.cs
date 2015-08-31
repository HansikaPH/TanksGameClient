using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Tanks
{

    /// <summary>
    /// Handles the communication
    /// </summary>

    class Communicator
    {
        private static Communicator comm = new Communicator();
        private NetworkStream outStream;
        private NetworkStream inStream;
        private StreamWriter writer;
        private TcpClient client;
        private StreamReader reader;
        private TcpListener listener;

        private Communicator()
        {
            listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();
        }

        public static Communicator GetInstance()
        {
            return comm;
        }
        public void sendData(String msg)
        {
           

            client = new TcpClient();

              
            try
            {
                client.Connect("127.0.0.1", 6000); 
                this.outStream = client.GetStream();

               this. writer = new StreamWriter(outStream);
                writer.Write(msg);
                
                writer.Flush();
                writer.Close();
                outStream.Close();
                client.Close();
                Console.Write(msg + " sent to server");
            }

            catch (Exception e)
            {
                Console.Write("Server Communication(sending) Failed for" + msg + " " + e.Message);
            }

        }

        public String recieveData()
        {

            String reply;

                reply = null;
              //  long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                client = listener.AcceptTcpClient();
                inStream = client.GetStream();
                this.reader = new StreamReader(inStream);
                if (inStream.DataAvailable)
                {
                    // Console.WriteLine("\n\nS");
                    reply = reader.ReadLine();
                  //  Console.WriteLine(s); //+ " \n : " + (DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond - time));
                    //      time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    
                }
                reader.Close();
                inStream.Close();
                client.Close();
                return reply;               
            
     

        }


    }
}
