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

        public static bool winOrLose;
        public static int gridWidth, gridHeight, mineCount;
        public static string[,] blankBoard;

        // main is not giving me errors here// 

        static void Main(string[] args)
        {
            Minefield.Cell[,] gridCells; // storing the grid 
            
            GetInput();

            gridCells = GenerateMineField(gridWidth, gridHeight, mineCount);
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
                DisplayBoard();

            }

            PrintField(gridCells);

        }

        static void GetInput()
        {
            string input = "";

            Console.WriteLine("Enter width: ");
            input = Console.ReadLine();

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
            

        static Cell [,] GenerateMineField(int width, int height, int count)
        {
            Cell[,] cells = new Cell[width, height];
            blankBoard = new string[width, height]; // initilizing blank board 
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
                    cells[x, y] = Cell.MINE;

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
                        //  loop for try again if the cell h
                    }
                    else
                    {
                        i--;
                    }
                }
                
            }
            return cells;

        }

        public static void InitilizeBoard()
        {
            for (int i = 0; i < blankBoard.GetLength(1); i++)
            {
                for (int j = 0; j < blankBoard.GetLength(1); j++)
                {
                    blankBoard[i, j] = ".";
                }
            }
        }

        public static void PopulateDisplayBoard (Cell [,] field, int coordX, int coordY, string answer)
        {
            if (answer == "f")
            {
                blankBoard[coordX, coordY] = "F";
            }
            else
            {
                if (field[coordX, coordY] != Cell.EMPTY && field[coordX, coordY] != Cell.MINE)
                {
                    blankBoard[coordX, coordY] = ((int)(Cell)field[coordX, coordY]).ToString();
                }
                else if (field[coordY, coordY] == Cell.EMPTY && answer == "r")
                {
                    blankBoard[coordX, coordY] = "X";
                }
                else if (field[coordX, coordY] == Cell.MINE)
                {
                    PrintField(field);
                }
            }
        }

        public static void DisplayBoard()
        {
            for (int i = 0; i < blankBoard.GetLength(1); i++)
            {
                for (int j = 0; j < blankBoard.GetLength(0); j++)
                {
                    Console.Write(blankBoard[i,j]);
                }
                Console.WriteLine("");
            }
        }

     
        static void PrintField(Cell [,] field)
        {
            winOrLose = true;
            Console.WriteLine("YOU LOSE!!!!!");
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for(int x = 0; x < field.GetLength(0); x++)
                {
                    if(field [x,y] == Cell.EMPTY)
                    {
                        Console.Write(".");
                    }
                    else if (field[x,y] == Cell.MINE)
                    {
                        Console.Write("M");
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
