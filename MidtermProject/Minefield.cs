using System;
using System.Collections.Generic;
using System.Text;

namespace MidtermProject
{
    class Minefield
    {
        /// Enum to define the states of each cell in the grid.
        public enum Cell
        {
            EMPTY,
            M1,
            M2,
            M3,
            M4,
            M5,
            M6,
            M7,
            M8,
            M9,
            MINE
        };

        // global variables 
        public static int[] mineLocation;
        public static bool winOrLose;
        public static int gridWidth, gridHeight, mineCount;
        public static string[,] gameBoard;

        // main is not giving me errors here// 
        /// The entry point of the program, where the program control starts and ends.
        /// This function will gather input values from the user, 

        static void Main(string[] args)
        {
            Minefield.Cell[,] gridCells; // storing the grid and all the cell info
            
            GetInput();

            gridCells = GenerateMineField(gridWidth, gridHeight, mineCount); // this comes from the userinput
            InitilizeBoard();
            DisplayBoard();

            winOrLose = false;
            while (!winOrLose)
            {
                Console.WriteLine("Enter coordinate X: ");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter coordinate y: ");
                int y = int.Parse(Console.ReadLine());

                Console.WriteLine("Reveal or Flag?:  ");
               string answer = (Console.ReadLine());

                PopulateDisplayBoard(gridCells, x, y, answer);

            }
        }

        /// Gets console input from the user for the width and height of the minefield,
        /// as well as how many mines are to be place in the field.
        static void GetInput()
        {
            string input = "";

            Console.WriteLine("Enter width: ");
            input = Console.ReadLine();

            // Forces user to continue inputting values until a valid number is entered.
            // The number also needs to be within the amount of cells in the grid to be valid.
            while (!Int32.TryParse(input, out gridWidth))
            {
                Console.WriteLine("This is not a valid number. Please enter a valid width: ");
                input = Console.ReadLine();
            }

            Console.WriteLine("Enter height: ");
            input = Console.ReadLine();

            while (!Int32.TryParse(input, out gridHeight))
            {
                Console.WriteLine("This is not a valid number. Please enter a valid height: ");
                Console.ReadLine();
            }

            Console.WriteLine("Enter mine count:");
            input = Console.ReadLine();

            while (!Int32.TryParse(input, out mineCount) || Convert.ToInt32(input) > gridWidth * gridHeight)
            {
               if(!Int32.TryParse(input, out mineCount))
               {
                    Console.WriteLine("This is not a valid number. Please try again. ");

               }
               else if (Convert.ToInt32(input) > gridWidth * gridHeight)
                {
                    Console.WriteLine("That is too many times for this size grid. Please enter a valid mine count: ");

                }
                input = Console.ReadLine();
            }
            
        }

        /// Takes grid size and number of mines and outputs a multidimentional array
        /// containing Cell values describing the contents of each grid cell.
        static Cell [,] GenerateMineField(int width, int height, int count)
        {
            Cell[,] cells = new Cell[width, height];
            gameBoard = new string[width, height]; // initilizing blank board 
            mineLocation = new int[count];
             // assign all elements in minefield to 0 
            for(int x = 0; x < width; x++) 
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = Cell.EMPTY;
                }
            }

            Random rnd = new Random(); 
            
            for (int i = 0; i < count; i++)
            {
                // using random to assign the random mines
                int x = rnd.Next(0, width);
                int y = rnd.Next(0, height);
                // if the cell/spot does not have a mine  after random
                if (cells[x, y] < Cell.MINE)
                {
                    //assigning a mine to it
                    cells[x, y] = Cell.MINE;

                    // For each cell around this mine that isn't also a mine,
                    // increase its value by one.
                    // After all mines are assigned, this will ensure each cell
                    // surrounding the cells are being incremented if next to a mine
                    if (y - 1 >= 0)
                    {
                        if (cells[x, y - 1] != Cell.MINE)
                        {
                            cells[x, y - 1]++;

                        }
                    }

                    if (y + 1 < cells.GetLength(1))
                    {
                        if (cells[x, y + 1] != Cell.MINE)
                        {
                            cells[x, y + 1]++;
                        }
                    }

                    if (x - 1 >= 0)
                    {
                        if (cells[x - 1, y] != Cell.MINE)
                        {
                            cells[x - 1, y]++;
                        }

                        if (y - 1 >= 0)
                        {
                            if (cells[x - 1, y - 1] != Cell.MINE)
                            {
                                cells[x - 1, y - 1]++;
                            }
                        }

                        if (y + 1 < cells.GetLength(1))
                        {
                            if (cells[x - 1, y + 1] != Cell.MINE)
                            {
                                cells[x - 1, y + 1]++;
                            }
                        }
                    }

                    // 2nd long if statment
                    if (x + 1 < cells.GetLength(0))
                    {
                        if (cells[x + 1, y] != Cell.MINE)
                        {
                            cells[x + 1, y]++;
                        }

                        if (y - 1 >= 0)
                        {
                            if (cells[x + 1, y - 1] != Cell.MINE)
                            {
                                cells[x + 1, y - 1]++;
                            }
                        }
                        if (y + 1 < cells.GetLength(1))
                        {
                            if (cells[x + 1, y + 1] != Cell.MINE)
                            {
                                cells[x + 1, y + 1]++;
                            }
                        }
                        
                    }
                    // If the randomly selected cell already contains a mine,
                    // make the for loop that iteration try again.
                }
                else
                {
                    i--;
                }

            }
            return cells;

        }
        //we are going to be using 2 boards, one is the answer and the other is the one that displays after userinput. 
        public static void InitilizeBoard()
        {
            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = ".";
                }
            }
        }

        public static void PopulateDisplayBoard (Cell [,] field, int coordX, int coordY, string answer)
        {
            if (answer == "f") // since F is only used for flagging we are checking for win condition in here after userinput
            {
                gameBoard[coordX, coordY] = "F";
                WinGame(field, coordX, coordY);
                DisplayBoard();
            }
            else
            {
                if (field[coordX, coordY] != Cell.EMPTY && field[coordX, coordY] != Cell.MINE)
                {
                    gameBoard[coordX, coordY] = ((int)(Cell)field[coordX, coordY]).ToString();
                    DisplayBoard();
                }
                else if (field[coordY, coordY] == Cell.EMPTY && answer == "r")
                {
                    gameBoard[coordX, coordY] = "X";
                    DisplayBoard();
                }
                else if (field[coordX, coordY] == Cell.MINE)
                {
                    winOrLose = true;
                    Console.WriteLine("YOU LOSE!!!!!");
                    PrintField(field);
                }
            }          
        }

        public static void WinGame(Cell[,]field, int coordX, int coordY) // if there are no more cells to flag you win
        {
            if(field[coordX,coordY] == Cell.MINE && gameBoard[coordX,coordY] == "F")
            {
                mineCount--; // if you flag a mine we count the mines down to 0.
            }
            if(mineCount == 0)
            {
                Console.WriteLine("YOU WIN!!!!");
                winOrLose = true;
                PrintField(field); 
            }
        }

        public static void DisplayBoard()
        {
            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(0); j++)
                {
                    Console.Write(gameBoard[i,j]);
                }
                Console.WriteLine("");
            }
        }

     
        static void PrintField(Cell [,] field)
        {
            // The array is [height, width], so we are flipping the grid the other way to display it //
        
            for (int x = 0; x < field.GetLength(1); x++)
            {
                for(int y = 0; y < field.GetLength(0); y++)
                {
                    if (field[x, y] == Cell.EMPTY)
                    {
                        Console.Write(".");
                    }
                    else if(field[x,y] == Cell.MINE)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("M");
                        Console.ResetColor();
                        // showing the # around the mines
                    }
                    else
                    {
                        Console.Write(Convert.ToInt32(field[x, y]).ToString());

                    }
                }
                Console.WriteLine("");
            }
        }



    }

}
