using GridSystem.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Output
{
    public static class PrintGrid
    {
        public static void OutputLoop(int duration, GridClass Grid)
        {
            /*while (duration > 0)
            {
                DrawGrid(Grid);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }*/
            DrawGrid(Grid);
        }
        static void DrawGrid(GridClass Grid)
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
