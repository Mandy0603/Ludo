using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    class Program
    {
        public static int[] Maps = new int[100];
        public static int[] PlayerPos = new int[2];
        public static string[] PlayerNames = new string[2];
        public static bool[] PauseFlag = new bool[2];

        static void Main(string[] args)
        {
            GameShow();
            #region Enter Players' names
            Console.WriteLine("Please enter the name of Player A:");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0] == "")
            {
                Console.WriteLine("Player A's name should not be empty, please enter again:");
                PlayerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("Please enter the name of Player B:");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || PlayerNames[0] == PlayerNames[1])
            {
                if (PlayerNames[1] == "")
                {
                    Console.WriteLine("Player B's name should not be empty, please enter again:");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Player B's name should not be the same with Player A's name, please enter again:");
                    PlayerNames[1] = Console.ReadLine();
                }
            }
            #endregion
            Console.Clear();
            GameShow();
            Console.WriteLine("{0}'s soldier is A", PlayerNames[0]);
            Console.WriteLine("{0}'s soldier is B", PlayerNames[1]);
            InitialMap();
            DrawMap();
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (PauseFlag[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    PauseFlag[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("Player{0} has won!!!", PlayerNames[0]);
                    break;
                }
                if (PauseFlag[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    PauseFlag[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("Player{0} has won!!!", PlayerNames[1]);
                    break;
                }

            }//while
            Win();

            Console.ReadKey();
        }
        /// <summary>
        /// Header
        /// </summary>
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("************************************");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**********Ludo Ludo!!!**************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("************************************");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("************************************");
            Console.ForegroundColor = ConsoleColor.Cyan;

        }
        /// <summary>
        /// Initialize map
        /// </summary>
        public static void InitialMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            int[] pause = { 9, 27, 60, 93 };
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }

        }
        /// <summary>
        /// Draw the map
        /// </summary>
        public static void DrawMap()
        {
            Console.WriteLine("Legend: LuckyTurn:◎  LandMine:☆   Pause:▲  TimeTunnel:卐");
            #region First row
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion
            #region First Column
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }
            #endregion
            #region Second Line
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion
            #region Second Column
            for (int i = 65; i < 70; i++)
            {
                Console.Write(DrawStringMap(i));
                for (int j = 0; j < 29; j++)
                {
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
            #endregion
            #region Third Row
            for (int i = 70; i < 100; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion
        }
        /// <summary>
        /// Method of drawing the map
        /// </summary>
        /// <param name="i">position in the map</param>
        /// <returns></returns>
        public static string DrawStringMap(int i)
        {
            string str = "";
            #region Draw
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[0] == i)
            {
                str = "<>";
            }
            else if (PlayerPos[0] == i)
            {
                str = "Ａ";
            }
            else if (PlayerPos[1] == i)
            {
                str = "Ｂ";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str = "卐";
                        break;
                }

            }
            return str;
            #endregion
        }
        /// <summary>
        /// Play game
        /// </summary>
        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);

            Console.WriteLine("{0}: Please press any key to throw the dice", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0} threw a {1}", PlayerNames[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine("{0}: Please press any key to move on", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}'s turn is over", PlayerNames[playerNumber]);
            Console.ReadKey(true);
            if (PlayerPos[playerNumber] == PlayerPos[1 - playerNumber])
            {
                Console.WriteLine("Player{0} stepped on Player {1},Player {2} moves back by 6 steps:", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                PlayerPos[1 - playerNumber] -= 6;
                Console.ReadKey(true);
            }
            else
            {
                switch (Maps[PlayerPos[playerNumber]])
                {
                    case 0:
                        Console.WriteLine("Player {0} stepped on a square, nothing has happend.", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("Player {0} stepped on a LuckyTurn, please choose: 1--exchange positions 2--throw a bomb", PlayerNames[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("Player {0} chose to exchange position with Player {1}", PlayerNames[playerNumber], PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                int temp = PlayerPos[0];
                                PlayerPos[0] = PlayerPos[1];
                                PlayerPos[1] = temp;
                                Console.WriteLine("Position changed successfully. Press any key to continue:");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("Player {0} chose to throw a bomb at Player {1}, Player {2} will step back by 6 steps.", PlayerNames[playerNumber], PlayerNames[1 - playerNumber], PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[1 - playerNumber] -= 6;
                                Console.WriteLine("Player {0} moved back by 6 steps", PlayerNames[1 - playerNumber]);
                                Console.ReadKey(true);
                                break;

                            }
                            else
                            {
                                Console.WriteLine("Please enter 1 or 2. 1--exchange positions 2--throw a bomb ");
                                input = Console.ReadLine();
                            }

                        }
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("Player {0} stepped on a landmine and will step back by 6 steps", PlayerNames[playerNumber]);
                        Console.ReadKey(true);
                        PlayerPos[playerNumber] -= 6;
                        break;
                    case 3:
                        Console.WriteLine("Player {0} stepped on a Pause, he/she will skip next turn.", PlayerNames[playerNumber]);
                        PauseFlag[playerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("Player {0} stepped on a TimeTunnel, he/she will move forward by 10 steps", PlayerNames[playerNumber]);
                        PlayerPos[playerNumber] += 10;
                        Console.ReadKey(true);
                        break;
                }//switch

            }//else
            ChangePos();
            Console.Clear();
            DrawMap();
        }
        /// <summary>
        /// In case players' position are out of the map
        /// </summary>
        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0] > 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] > 99)
            {
                PlayerPos[1] = 99;
            }
        }
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("    ■          ■                       ■ ■ ■              ");
            Console.WriteLine("    ■        ■                      ■          ■          ");
            Console.WriteLine("    ■      ■                       ■            ■         ");
            Console.WriteLine("    ■    ■                         ■            ■         ");
            Console.WriteLine("    ■  ■                           ■            ■         ");
            Console.WriteLine("    ■    ■                         ■            ■         ");
            Console.WriteLine("    ■      ■                       ■            ■         ");
            Console.WriteLine("    ■        ■                      ■          ■          ");
            Console.WriteLine("    ■          ■         ■              ■ ■ ■       ■     ");
            
            Console.ResetColor();
        }
    }
}
