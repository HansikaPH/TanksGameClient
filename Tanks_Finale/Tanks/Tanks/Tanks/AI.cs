using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Tanks
{

    class AI
    {
        private static Communicator com;
        private String reply;
   //     private Boolean joined;
        private int count = 0;
        private String playerName;
        private int x, y;
        private int direction;
        private int noOfPlayers;

        private String[,] map = new String[10,10];
        private int[,] players = null;

        public AI(Communicator c)
        {
            com = c;
        }
        
        
        public void process( String s)
        {
            
            
            reply = s;
       
            if( reply.StartsWith("I:")){
                this.init();
            }
            else if( reply.StartsWith("S:")){
                this.setUp();
            }

            else if (reply.StartsWith("G:"))
            {
                this.update();
            }


            else if (reply.StartsWith("C:"))
            {
                this.coin();
            }

            else if (reply.StartsWith("L:"))
            {
                this.lifePack();
            }
      

        }

      

        private void init()
        {
            String[] data = (reply.Substring(0,reply.Length-1)).Split(':');
            Console.Write(data[4] + "\n");
            playerName = data[1];
            String[] bricks = data[2].Split(';');
            String[] stones = data[3].Split(';'); 
            String[] waters = data[4].Split(';');
            Console.Write(bricks[0] + "\n");
            Console.Write(waters[0] + "\n");
            
            foreach(String brick in bricks){
               
               map[(int)Char.GetNumericValue(brick[0]), (int)Char.GetNumericValue(brick[2])] = "B";
               
            }
            foreach (String stone in stones)
            {
                
                map[(int)Char.GetNumericValue(stone[0]), (int)Char.GetNumericValue(stone[2])] = "S";

            }
            foreach (String water in waters)
            {
                
                map[(int)Char.GetNumericValue(water[0]),(int)Char.GetNumericValue(water[2]) ] = "W";

            }
            
        }

        private void setUp()
        {
            x = reply[5];
            y = reply[7];
            direction = reply[9];

            go();

        }

        private void update()
        {
            count++;
            String[] data = (reply.Substring(0, reply.Length - 1)).Split(':');
            String[] damages = data[data.Length - 1].Split(';');

            if (count == 1)
            {
                noOfPlayers = data.Length - 2; //???
                players = new int[noOfPlayers, 7]; //??

            }

            for (int i = 1; i <= noOfPlayers; i++)
            {
                map[(int)Char.GetNumericValue((data[i])[3]), (int)Char.GetNumericValue((data[i])[5])] = (data[i]).Substring(0, 2);
                players[i-1,0] = (int)Char.GetNumericValue((data[i])[3]);
                players[i - 1, 1] = (int)Char.GetNumericValue((data[i])[5]);
                players[i - 1, 2] = (int)Char.GetNumericValue((data[i])[7]);
                players[i - 1, 3] = (int)Char.GetNumericValue((data[i])[9]);
                players[i - 1, 4] = (int)Char.GetNumericValue((data[i])[11]);
                players[i - 1, 5] = (int)Char.GetNumericValue((data[i])[13]);
                players[i - 1, 6] = (int)Char.GetNumericValue((data[i])[15]);
                if ((data[i]).Substring(0, 2) == playerName)
                {
                    x = (int)Char.GetNumericValue((data[i])[3]);
                    y = (int)Char.GetNumericValue((data[i])[5]);
                    direction = (int)Char.GetNumericValue((data[i])[7]);
                }
            }

            foreach (String damage in damages)
            {
                
                map[(int)Char.GetNumericValue(damage[0]), (int)Char.GetNumericValue(damage[2])] = "B" + damage[4];
                                            
            }

            go();
            


        }

        private void coin()
        {
           // reply[2]
           //     reply[4]

            go();
        }

        public void setTimer()
        {
            System.Timers.Timer timer1 = new System.Timers.Timer(2000);
            
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Interval = 1000;
            timer1.Enabled = true;

            Console.WriteLine("timer strated");
            //double counter = timer1.Interval;
            
            
        }

        static void timer1_Elapsed(object sender, EventArgs e)
        {
            //counter--;
            //if(counter==0){
            Console.WriteLine("event");
        }

        private void lifePack()
        {
        }

        private void go(){
            int tempX = x, tempY = y;
            switch (direction)
            {
                case 0:
                    if (y > 0)
                    {
                        tempY = y - 1;
                        if (map[tempX, tempY] == null) com.sendData("UP#");
                        else if ((map[tempX, tempY].StartsWith("B") && !map[tempX, tempY].Contains("4")) || map[tempX, tempY].StartsWith("P")) com.sendData("SHOOT#");
                        else com.sendData("RIGHT#");
                        //    else if 
                       // else com.sendData("UP#");
                    }
                    break;
                case 1:
                    if (x < 9)
                    {
                        tempX = x + 1;
                        if (map[tempX, y] == null) com.sendData("RIGHT#");
                        else if ((map[tempX, tempY].StartsWith("B") && !map[tempX, tempY].Contains("4")) || map[tempX, tempY].StartsWith("P")) com.sendData("SHOOT#");
                        else com.sendData("DOWN#");
                    }
                    break;
                case 2:
                    if (y < 9)
                    {
                        tempY = y + 1;
                        if (map[tempX, tempY] == null) com.sendData("DOWN#");
                        else if ((map[tempX, tempY].StartsWith("B") && !map[tempX, tempY].Contains("4")) || map[tempX, tempY].StartsWith("P")) com.sendData("SHOOT#");
                        else com.sendData("LEFT#");
                    }
                    break;
                case 3:
                    if (x > 0)
                    {
                        tempX = x - 1;
                        if (map[tempX, y] == null) com.sendData("LEFT#");
                        else if ((map[tempX, tempY].StartsWith("B") && !map[tempX, tempY].Contains("4")) || map[tempX, tempY].StartsWith("P")) com.sendData("SHOOT#");
                        else com.sendData("UP#");
                    }
                    break;
            }
        }

       

    }

}

public class Timer1
{
   // private static System.Timers.Timer aTimer;

    public static void setTimer(System.Timers.Timer timer, int LT)
    {
        timer = new System.Timers.Timer(LT);
        timer.AutoReset = false;

        timer.Elapsed += OnTimedEvent;
        timer.Start();
        //Console.WriteLine("Timer started");
    }

    // Specify what you want to happen when the Elapsed event is  
    // raised. 
    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        //remove the coin****** 

        Timer bTimer = (Timer)source;
        bTimer.Enabled = false;
        //Console.WriteLine("Timer stoped");
    }
}