using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public static class Decision
    {
        //decide where to
        public static int MakeTheDecision(bool returnMode, int[] arrFoodSmell, int[] arrSeachSmell, List<Tactic> tactics)
        {
            //ToDo: special case if location food or anthill, probably no vectors available
            //Get tactics List and compare current situation with which tactic to use

            int[] vectorF1 = new int[]{arrFoodSmell[0],arrFoodSmell[1],arrFoodSmell[2]};
            int[] vectorF2 = new int[]{arrFoodSmell[3],arrFoodSmell[4],arrFoodSmell[5]};
            int[] vectorF3 = new int[]{arrFoodSmell[6],arrFoodSmell[7],arrFoodSmell[8]};
            int[] vectorF4 = new int[]{arrFoodSmell[6],arrFoodSmell[3],arrFoodSmell[0]};
            int[] vectorF5 = new int[]{arrFoodSmell[7],arrFoodSmell[4],arrFoodSmell[1]};
            int[] vectorF6 = new int[]{arrFoodSmell[8],arrFoodSmell[5],arrFoodSmell[2]};
            int[] vectorF7 = new int[]{arrFoodSmell[6],arrFoodSmell[4],arrFoodSmell[2]};
            int[] vectorF8 = new int[]{arrFoodSmell[0],arrFoodSmell[4],arrFoodSmell[8]};

            int[] vectorS1 = new int[] {arrSeachSmell[0],arrSeachSmell[1],arrSeachSmell[2]};
            int[] vectorS2 = new int[]{arrSeachSmell[3],arrSeachSmell[4],arrSeachSmell[5]};
            int[] vectorS3 = new int[]{arrSeachSmell[6],arrSeachSmell[7],arrSeachSmell[8]};
            int[] vectorS4 = new int[]{arrSeachSmell[6],arrSeachSmell[3],arrSeachSmell[0]};
            int[] vectorS5 = new int[]{arrSeachSmell[7],arrSeachSmell[4],arrSeachSmell[1]};
            int[] vectorS6 = new int[]{arrSeachSmell[8],arrSeachSmell[5],arrSeachSmell[2]};
            int[] vectorS7 = new int[]{arrSeachSmell[6],arrSeachSmell[4],arrSeachSmell[2]};
            int[] vectorS8 = new int[]{arrSeachSmell[0],arrSeachSmell[4],arrSeachSmell[8]};

            double f1 = countVector(vectorF1);
            double f2 = countVector(vectorF2);
            double f3 = countVector(vectorF3);
            double f4 = countVector(vectorF4);
            double f5 = countVector(vectorF5);
            double f6 = countVector(vectorF6);
            double f7 = countVector(vectorF7);
            double f8 = countVector(vectorF8);

            double s1 = countVector(vectorS1);
            double s2 = countVector(vectorS2);
            double s3 = countVector(vectorS3);
            double s4 = countVector(vectorS4);
            double s5 = countVector(vectorS5);
            double s6 = countVector(vectorS6);
            double s7 = countVector(vectorS7);
            double s8 = countVector(vectorS8);

            //archive mode, highestValues, lowestValue, allTheSame?, direction, moveTaken.

            return -1;
        }
        //decide which vector is more worth to follow
        private static double countVector(int[] vector)
        {
            double weight = 0;
            if (IsVector(vector))
            {
                weight = (vector[0] + vector[1] + vector[2])/3 + (vector[0] + vector[1] + vector[2]) % 3;
                if(vector[0]> vector[2])
                {
                    weight *= (-1);
                }
            }
            return weight;
        }
        //are three value a vector?
        private static bool IsVector(int[] vector)
        {
            bool isVector = true;
            foreach(int i in vector)
            {
                if (i <= 0)
                {
                    isVector = false;
                }
            }
            return isVector;
        }
    }
}
