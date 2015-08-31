using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using Tanks;

namespace Tanks
{
#if WINDOWS || XBOX
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        private Communicator comm;
        private String reply;
        private AI aiPlayer;
        private GameWorld game;
        private static Program program;

        public static void Main(string[] args)
        {
            program = new Program();
            program.play();
            
        }

        public void play()
        {

            
        //    game = new GameWorld();
        //   comm = Communicator.GetInstance();
        //   aiPlayer = new AI(comm);
        //    comm.sendData("JOIN#");

        //    while (true)
        //    {
        //        reply = comm.recieveData();
        //        if (reply == null) continue;

        //        if (reply.Equals("PLAYERS_FULL#") || reply.Equals("GAME_ALREADY_STARTED#"))
        //        {
        //            comm.sendData("JOIN#");
        //        }

        //        else
        //        {
        //            aiPlayer.process(reply);

        //            Console.Write(reply);
        //        }

                  using (GameWorld game = new GameWorld())
                  {
                      game.Run();
                  }
            //aiPlayer.setTimer();
            }
        }

       
    }
#endif
//}

