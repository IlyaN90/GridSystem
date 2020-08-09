using GridSystem.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public static class Decision
    {
        //choses tactic depending on tactic.raito||forces a random tactic for training
        public static int ChooseTactic(int x, int y, bool returnMode, GridClass Grid, int currentPosition)
        {
            //choose new tactic
            int decision = -1;
            bool edge = CheckForEdges(x, y);

            //change to bool func (out vector)
            foodVectorsNearby(Grid, currentPosition);
            searchVectorsNearby(Grid, currentPosition);
            //add vectors
            List<Tactic> toChoseFrom = Grid.tactics.Where(r => (r.returnMode == returnMode) && (r.edge==edge)).ToList();

            if(needMoreTraining(toChoseFrom, out List < Tactic > needsMoreData))
            {
                Random rnd = new Random();
                int pickRandom = rnd.Next(needsMoreData.Count);
                decision = needsMoreData[pickRandom].number;
            }
            else
            {
                decision = Support.LastFromTheSortedList(Grid.tactics);
            }
            return decision;
        }
        //todo bool func(out vector) get the highest weight if there is a search vector nearby
        public static void searchVectorsNearby(GridClass Grid, int currentPosition)
        {
            int weight = 0;
            int[] arrSeachSmell = SmellForClues(Grid, currentPosition);
            int[] vectorS1 = new int[] { arrSeachSmell[0], arrSeachSmell[1], arrSeachSmell[2] };
            int[] vectorS2 = new int[] { arrSeachSmell[3], arrSeachSmell[4], arrSeachSmell[5] };
            int[] vectorS3 = new int[] { arrSeachSmell[6], arrSeachSmell[7], arrSeachSmell[8] };
            int[] vectorS4 = new int[] { arrSeachSmell[6], arrSeachSmell[3], arrSeachSmell[0] };
            int[] vectorS5 = new int[] { arrSeachSmell[7], arrSeachSmell[4], arrSeachSmell[1] };
            int[] vectorS6 = new int[] { arrSeachSmell[8], arrSeachSmell[5], arrSeachSmell[2] };
            int[] vectorS7 = new int[] { arrSeachSmell[6], arrSeachSmell[4], arrSeachSmell[2] };
            int[] vectorS8 = new int[] { arrSeachSmell[0], arrSeachSmell[4], arrSeachSmell[8] };
            int s1 = 0;
            int s2 = 0;
            int s3 = 0;
            int s4 = 0;
            int s5 = 0;
            int s6 = 0;
            int s7 = 0;
            int s8 = 0;
            if (countVector(vectorS1, out weight))
            {
                s1 = weight;
            }
            if (countVector(vectorS2, out weight))
            {
                s2 = weight;
            }
            if (countVector(vectorS3, out weight))
            {
                s3 = weight;
            }
            if (countVector(vectorS4, out weight))
            {
                s4 = weight;
            }
            if (countVector(vectorS5, out weight))
            {
                s5 = weight;
            }
            if (countVector(vectorS6, out weight))
            {
                s6 = weight;
            }
            if (countVector(vectorS7, out weight))
            {
                s7 = weight;
            }
            if (countVector(vectorS8, out weight))
            {
                s8 = weight;
            }
        }
        //todo bool func(out vector) get the highest weight if there is a food vector nearby
        public static void foodVectorsNearby(GridClass Grid, int currentPosition) 
        {
            int[] arrFoodSmell = SmellForFood(Grid, currentPosition);

            //all possible vectors
            int[] vectorF1 = new int[] { arrFoodSmell[0], arrFoodSmell[1], arrFoodSmell[2] };
            int[] vectorF2 = new int[] { arrFoodSmell[3], arrFoodSmell[4], arrFoodSmell[5] };
            int[] vectorF3 = new int[] { arrFoodSmell[6], arrFoodSmell[7], arrFoodSmell[8] };
            int[] vectorF4 = new int[] { arrFoodSmell[6], arrFoodSmell[3], arrFoodSmell[0] };
            int[] vectorF5 = new int[] { arrFoodSmell[7], arrFoodSmell[4], arrFoodSmell[1] };
            int[] vectorF6 = new int[] { arrFoodSmell[8], arrFoodSmell[5], arrFoodSmell[2] };
            int[] vectorF7 = new int[] { arrFoodSmell[6], arrFoodSmell[4], arrFoodSmell[2] };
            int[] vectorF8 = new int[] { arrFoodSmell[0], arrFoodSmell[4], arrFoodSmell[8] };

            //which vector smells the strongest
            int weight = 0;
            int f1 = 0;
            int f2 = 0;
            int f3 = 0;
            int f4 = 0;
            int f5 = 0;
            int f6 = 0;
            int f7 = 0;
            int f8 = 0;

            if (countVector(vectorF1, out weight))
            {
                f1 = weight;
            }
            if (countVector(vectorF2, out weight))
            {
                f2 = weight;
            }
            if (countVector(vectorF3, out weight))
            {
                f3 = weight;
            }
            if (countVector(vectorF4, out weight))
            {
                f4 = weight;
            }
            if (countVector(vectorF5, out weight))
            {
                f5 = weight;
            }
            if (countVector(vectorF6, out weight))
            {
                f6 = weight;
            }
            if (countVector(vectorF7, out weight))
            {
                f7 = weight;
            }
            if (countVector(vectorF8, out weight))
            {
                f8 = weight;
            }
        }

        //decide where to next
        public static int NextStep(int x, int y, int currentPosition, bool returnMode, GridClass Grid, int chosenTactic)
        {
            //int number, bool returnMode, bool allTheSame, bool towards, bool edge, bool picThisDirection, int plusPoints, int minusPoints, int totalTimes, double raito

            int nextStep = -1;

            bool edge = CheckForEdges(x, y);

            if (returnMode && AnthillNearby(currentPosition, out nextStep))
            {
                return nextStep;
            }
            if (!returnMode && FoodNearby(currentPosition, out nextStep))
            {
                return nextStep;
            }

            int[] tilesAround = TilesAround(currentPosition);


            return nextStep;
        }
        //decide which vector is more worth to follow
        private static bool countVector(int[] vector, out int weight)
        {
            weight = 0;
            if (vector.Length == 3) 
            { 
                if (IsVector(vector))
                {
                    weight = (vector[0] + vector[1] + vector[2])/3 + (vector[0] + vector[1] + vector[2]) % 3;
                    if(vector[0]> vector[2])
                    {
                        weight *= (-1);
                    }
                    return true;
                }
            }
            return false;
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
        //check if the data statistics are sufficient
        private static bool needMoreTraining(List<Tactic> toChoseFrom, out List<Tactic> needsMoreData)
        {
            needsMoreData = new List<Tactic>();
            bool unTrained = false;
            foreach(Tactic tactic in toChoseFrom)
            {
                if (tactic.totalTimes < 100)
                {
                    needsMoreData.Add(tactic);
                    unTrained = true;
                }
            }
            return unTrained;
        }
        //return tiles around
        public static int[] TilesAround(int currentPosition)
        {
            int[] tilesAround = new int[]
                {
                    currentPosition + 1 - 100,
                    currentPosition + 1,
                    currentPosition + 1 + 100,
                    currentPosition - 100,
                    currentPosition,
                    currentPosition + 100,
                    currentPosition - 1 - 100,
                    currentPosition - 1,
                    currentPosition - 1 + 100
                };
            return tilesAround;
        }
        
        //todo check if there is an Anthouse nearby
        private static bool AnthillNearby(int currentPosition, out int position)
        {
            position = 0;
            return false;
        }
        //todo check if there is an Anthouse food
        private static bool FoodNearby(int currentPosition, out int position)
        {
            position = 0;
            return false;
        }

        //ckecks if the ant is standing on the edge
        private static bool CheckForEdges(int x, int y)
        {
            bool edge = false;

            if (x == 1 || x == 100)
            {
                edge = true;
            }
            if (y == 1 || y == 100)
            {
                edge = true;
            }
            return edge;
        }

        public static int[] SmellForFood(GridClass Grid, int currentPosition)
        {
            int[] arr = new int[]
                {
                    Support.S_Smell(Grid, currentPosition + 1 - 100),       //  +y-x  
                    Support.S_Smell(Grid, currentPosition + 1),             //  +y
                    Support.S_Smell(Grid, currentPosition + 1 + 100),       //  +x+y
                    Support.S_Smell(Grid, currentPosition - 100),           //  -x
                    Support.S_Smell(Grid, currentPosition),                 //current position
                    Support.S_Smell(Grid, currentPosition + 100),           //  +x
                    Support.S_Smell(Grid, currentPosition - 1 - 100),       //  -x-y
                    Support.S_Smell(Grid, currentPosition - 1),             //  -y
                    Support.S_Smell(Grid, currentPosition - 1 + 100)        //  -y+x
                };
            return arr;
        }
        //get arr with values of "i am looking for food" feromones
        public static int[] SmellForClues(GridClass Grid, int currentPosition)
        {
            int[] arr = new int[]
                {
                    Support.S_OtherSmell(Grid, currentPosition + 1 - 100),       //  +y-x  
                    Support.S_OtherSmell(Grid, currentPosition + 1),             //  +y
                    Support.S_OtherSmell(Grid, currentPosition + 1 + 100),       //  +x+y
                    Support.S_OtherSmell(Grid, currentPosition - 100),           //  -x
                    Support.S_OtherSmell(Grid, currentPosition),                 //current position
                    Support.S_OtherSmell(Grid, currentPosition + 100),           //  +x
                    Support.S_OtherSmell(Grid, currentPosition - 1 - 100),       //  -x-y
                    Support.S_OtherSmell(Grid, currentPosition - 1),             //  -y
                    Support.S_OtherSmell(Grid, currentPosition - 1 + 100)        //  -y+x
                };
            return arr;
        }
    }
}
