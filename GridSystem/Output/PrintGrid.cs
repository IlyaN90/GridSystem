using GridSystem.Ants;
using GridSystem.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Output
{
    //rename class
    public static class PrintGrid
    {
        //starts iterations while at least one ant is alive
        public static void OutputLoop(int duration, GridClass Grid, Anthill anthill)
        {
            bool gameOver = anthill.AntsAlive();
            int iterationNumber = 0;
            DrawGrid(Grid, anthill);
            DoIterations(Grid, anthill, iterationNumber);
            /*while (!gameOver)
            {
                iterationNumber+=1;
                DoIterations(Grid, anthill, iterationNumber);
                DrawGrid(Grid);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }*/
        }

        //iterates throu each ant and handles new food spawns
        static void DoIterations(GridClass Grid, Anthill anthill, int iterationNumber)
        {
            //add new food sources here every 100th iteration
            /*
            if(iterationNumber%100==0)
            {
                int anthillLocation=Support.S_CoordinatesToList(anthill.X, anthill.Y);
                Grid.PlaceFood(anthillLocation);
            }
            */

            foreach (Ants.Ants ant in anthill.ants)
            {
                ant.NewTurn(Grid);
            }
        }

        //Prints grid for fun and debug purposes
        static void DrawGrid(GridClass Grid, Anthill anthill)
        {
            for (int i = 0;i<Grid.CellGrid.Count; i++)
            {
                bool dont = false;
                if (Grid.CellGrid[i].FoodPosition)
                {
                    Console.Write(" f");
                    dont = true;
                }
                if (Grid.CellGrid[i].AnthillPosition == true)
                {
                    Console.Write(" X");
                }
                else if (Grid.CellGrid[i].AntPostition == true)
                {
                    Console.Write(" a");
                }
                else if(i>0&&(i+1)%100 == 0)
                {
                    if (!dont)
                    {
                        Console.Write(" .");
                    }
                    Console.WriteLine(" " + i);
                }
                else
                {
                    if (!dont)
                    {
                        Console.Write(" .");
                    }
                }
            }
        }
    }
}
