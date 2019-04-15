using System;

namespace SpectoryCoreAPP
{

    namespace Spectory
    {
        // Player info struct - playerName and playerSign
        public struct playerInfo
        {
            public string playerName;
            public string playerSign;
        };

        class Fourinarow
        {
            static void Main(string[] args)
            {
                DefineConsoleAppearance();
                //Initialize game Board
                string[,] board = new string[9, 10];
                bool boardFull;
                Tuple<playerInfo, playerInfo> myPlayers = InstantiatePlayers();
                // Declare and initialize struct objects. 
                Console.WriteLine("\nThe Board is ready - GOOD LUCK:\n ");
                PrintBoard(board); //Display the board
                do
                {
                    string resultPlayer1 = PlayerRound(board, myPlayers.Item1);
                    string resultPlayer2 = PlayerRound(board, myPlayers.Item2);
                    //Check if the board is full that means if we don't have space to drop our choice and game finished with draw
                    boardFull = CheckForFullBoard(board);

                } while (!boardFull);
            }

            private static void DefineConsoleAppearance()
            {
                //Console colors
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            //Player round - starting with Player 1 than Player 2
            private static string PlayerRound(string[,] board, playerInfo player)
            {
                int dropChoice;
                bool playerWin;
                //Player One start to play first
                dropChoice = PlayerDrop(board, player);
                //Put drop choice on the first empty row
                CheckBellow(board, player, dropChoice);
                PrintBoard(board);
                //Connected four check
                playerWin = CheckFour(board, player);
                //If player wins
                if (playerWin)
                {
                    PlayerWin(player);
                    restart(board);
                }
                return null;
            }

            //Instantiate players name and sign
            private static Tuple<playerInfo, playerInfo> InstantiatePlayers()
            {
                playerInfo playerOne = new playerInfo();
                playerInfo playerTwo = new playerInfo();
                Console.WriteLine("Game started");
                Console.WriteLine("Player One please enter your name: ");
                playerOne.playerName = Console.ReadLine();
                playerOne.playerSign = " X ";
                Console.WriteLine("\nPlayer Two please enter your name: ");
                playerTwo.playerName = Console.ReadLine();
                playerTwo.playerSign = " O ";
                return Tuple.Create<playerInfo, playerInfo>(playerOne, playerTwo);
            }

            // This function returns the user choice - refer for both users. 
            static int PlayerDrop(string[,] board, playerInfo activePlayer)
            {
                int dropChoice;
                Console.WriteLine("\n" + activePlayer.playerName + "'s Turn ");
                Console.WriteLine(activePlayer.playerName + "(" + activePlayer.playerSign.Trim() + ")" + ", Please enter a number between 1 and 7: ");

                //Accept only values between 1-7
                while (!int.TryParse(Console.ReadLine(), out dropChoice) || dropChoice < 1 || dropChoice > 7)

                {
                    Console.WriteLine("\nNot a valid number between 1 and 7: " + activePlayer.playerName.Trim() + " please enter valid number again");
                }
                Console.Clear();

                // Check if the column is full.
                while (board[1, dropChoice] == " X " || board[1, dropChoice] == " O ")
                {
                    Console.WriteLine("\nThat column is full, please enter a new column: ");
                    dropChoice = Convert.ToInt32(Console.ReadLine());
                }
                return dropChoice;
            }


            // This function assign user drop choice to the board
            static void CheckBellow(string[,] board, playerInfo activePlayer, int dropChoice)
            {
                int length = 6, turn = 0;
                do
                {
                    if (board[length, dropChoice] != " X " && board[length, dropChoice] != " O ")
                    {
                        board[length, dropChoice] = activePlayer.playerSign;
                        turn = 1;
                    }
                    else
                        --length;
                } while (turn != 1);
            }

            // This function display the board in the console after every user drop choice
            static void PrintBoard(string[,] board)
            {
                int rows = 6, columns = 7, i, ix;
                Console.Write("\n");
                for (i = 1; i <= rows; i++)
                {
                    Console.Write("|");
                    for (ix = 1; ix <= columns; ix++)
                    {
                        if (board[i, ix] != " X " && board[i, ix] != " O ")
                            board[i, ix] = " # ";
                        Console.Write(board[i, ix]);
                    }
                    Console.Write("| \n");
                }
            }

            //// This function check if there is a win according to 5 win searches: Left, Right, Up, Left And Up and Right And Up
            static bool CheckFour(string[,] board, playerInfo activePlayer)
            {
                bool win = SearchLeft(board, activePlayer) || SearchRight(board, activePlayer) || SearchUp(board, activePlayer) || SearchLeftAndUp(board, activePlayer) ||  SearchRightAndUp(board, activePlayer);
                return win;           
            }

            //Search for win - Left
            static bool SearchLeft(string[,] board, playerInfo activePlayer)
            {
                string sign = activePlayer.playerSign;
                bool win = false;
                for (int i = 8; i >= 1; --i)
                {
                    for (int ix = 9; ix >= 1; --ix)
                    {
                        //searchLeft
                        if (board[i, ix] == sign &&
                            board[i, ix - 1] == sign &&
                            board[i, ix - 2] == sign &&
                            board[i, ix - 3] == sign)
                        {
                            win = true;
                        }
                    }
                }
                return win;
            }

            //Search for win - Right
            static bool SearchRight(string[,] board, playerInfo activePlayer)
            {
                string sign = activePlayer.playerSign;
                bool win = false;
                for (int i = 8; i >= 1; --i)
                {
                    for (int ix = 9; ix >= 1; --ix)
                    {
                        //searchRight
                        if (board[i, ix] == sign &&
                            board[i, ix + 1] == sign &&
                            board[i, ix + 2] == sign &&
                            board[i, ix + 3] == sign)
                        {
                            win = true;
                        }
                    }
                }
                return win;
            }

            //Search for win - Up
            static bool SearchUp(string[,] board, playerInfo activePlayer)
            {
                string sign = activePlayer.playerSign;
                bool win = false;
                for (int i = 8; i >= 1; --i)
                {
                    for (int ix = 9; ix >= 1; --ix)
                    {
                        //searchUp
                        if (board[i, ix] == sign &&
                            board[i - 1, ix] == sign &&
                            board[i - 2, ix] == sign &&
                            board[i - 3, ix] == sign)
                        {
                            win = true;
                        }
                    }

                }
                return win;
            }

            //Search for win - Left and Up
            static bool SearchLeftAndUp(string[,] board, playerInfo activePlayer)
            {
                string sign = activePlayer.playerSign;
                bool win = false;
                for (int i = 8; i >= 1; --i)
                {
                    for (int ix = 9; ix >= 1; --ix)
                    {

                        //searchLeft && searchUp
                        if (board[i, ix] == sign &&
                            board[i - 1, ix - 1] == sign &&
                            board[i - 2, ix - 2] == sign &&
                            board[i - 3, ix - 3] == sign)
                        {
                            win = true;
                        }
                    }
                }
                return win;
            }

            //Search for win - Right and Up
            static bool SearchRightAndUp(string[,] board, playerInfo activePlayer)
            { 
                
            string sign = activePlayer.playerSign;
            bool win = false;
            for (int i = 8; i >= 1; --i)
            {
                for (int ix = 9; ix >= 1; --ix)
                {
                    //searchRight && searchUp
                    if (board[i, ix] == sign &&
                        board[i - 1, ix + 1] == sign &&
                        board[i - 2, ix + 2] == sign &&
                        board[i - 3, ix + 3] == sign)
                    {
                        win = true;
                    }
                }
            }
            return win;
        }
    

            // This function checks if the column is full 
            static bool CheckForFullBoard(string[,] board)
            {
                int boardFull = 0;
                for (int i = 1; i <= 7; ++i)
                {
                    if (board[1, i] != " # ")
                        ++boardFull;
                }
                if (boardFull == 7)
                {
                    Console.WriteLine("The board is full, it is a draw");
                    Console.ReadLine();
                    return true;
                }
                else
                    return false;
            }

            // This function Prints the winning player name
            static void PlayerWin(playerInfo activePlayer)
            {
                Console.WriteLine(activePlayer.playerName + " \nWin the game!\n");

            }

            // Restart board
            static void restart(string[,] board)
            {
                int restart;

                Console.WriteLine("Would you like to restart? Yes(1) No(2): ");
                while (!int.TryParse(Console.ReadLine(), out restart) || restart < 1 || restart > 2)
                {
                    Console.WriteLine("\nNot a valid number, please enter 1 OR 2 only!");
                }

                //In case of restart, fill the array again
                if (restart == 1)
                {
                    Console.Clear();
                    for (int i = 1; i <= 6; i++)
                    {
                        for (int ix = 1; ix <= 7; ix++)
                        {
                            board[i, ix] = " # ";
                        }
                    }

                    PrintBoard(board);
                }
                else
                {
                    Console.WriteLine("Press any key to Exit");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }             
    }
}
