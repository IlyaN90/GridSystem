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
        public static bool S_CoordinatesToList(int x, int y, out int thePlace)
        {
            thePlace = -1;
            if (x < 0 && y < 0)
            {
                return false;
            }
            else if (x > 100 && y > 100)
            {
                return false;
            }
            int spotX = x * 100;
            thePlace = spotX + y;
            return true;
        }

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

        public static int LastFromTheSortedList(List<Tactic> tactics)
        {
            List<Tactic> biggestRaitoList = new List<Tactic>();
            double biggest = Double.MinValue;
            foreach (Tactic tactic in tactics)
            {
                if (tactic.raito.Equals(biggest))
                {
                    biggestRaitoList.Add(tactic);
                }
                else if (Support.CompareDouble(biggest, tactic.raito, out double result))
                {
                    biggest = result;
                    biggestRaitoList.Add(tactic);
                }
            }
            return biggestRaitoList[biggestRaitoList.Count - 1].number;
        }

        public static bool CompareDouble(double a, double b, out double biggest)
        {
            biggest = a;
            long aLong = (long)a;
            double aRest = a - aLong;

            long bLong = (long)b;
            double bRest = b - aLong;

            if (a == b)
            {
                double rest = bRest - aRest;
                if (rest >= 0)
                {
                    biggest = b;
                    return true;
                }
            }
            else
            {
                double bigger = b-a;
                biggest = b;
                return true;
            }
            return false;
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
