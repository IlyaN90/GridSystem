using GridSystem.Ants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Grid
{
    public static class Support
    {
        public static int S_CoordinatesToList(int x, int y)
        {
            int spotX = x * 100;
            int theplace = spotX + y;
            return theplace;
        }
        //test this!
        public static int[] S_ListToCoordinates(int position)
        {
            int x= position/100;
            int y= position % 100;

            int[] arr = new int[] 
            { 
                x,
                y
            };
            return arr;
        }

        public static int[] S_FoodNodeLocation()
        {
            Random rnd = new Random();
            int foodLocation = rnd.Next(10000);
            int foodAmmount = rnd.Next(50, 100);
            int[] foodSpot = new int[2];
            foodSpot[0] = foodLocation;
            foodSpot[1] = foodAmmount;
            return foodSpot;
        }

        public static int S_Smell(GridClass Grid, int position)
        {
            int smellLevel = Grid.CellGrid[position].FoodFeromones;
            if (smellLevel > 0)
            {
                return smellLevel;
            }
            return -1;
        }

        public static int S_OtherSmell(GridClass Grid, int position)
        {
            int smellLevel = Grid.CellGrid[position].SearchFeromones;
            if (smellLevel > 0)
            {
                return smellLevel;
            }
            return -1;
        }

        public static void S_MarkCell(GridClass Grid, int position, bool mode)
        {
            if (mode)
            {
                Grid.CellGrid[position].FoodFeromones = +50;
            }
            else
            {
                Grid.CellGrid[position].SearchFeromones = +50;
            }
        }
    }
}
