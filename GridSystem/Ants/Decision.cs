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
        public static int ChooseTactic(int x, int y, bool returnMode, List<Cell> CellsList, List<Tactic> tactics, int currentPosition)
        {
            //choose new tactic
            int decision = -1;
            bool edge = CheckForEdges(x, y);
            List<Cell> tilesAround = TilesAround(CellsList, currentPosition);

            //change to bool func (out vector)
            foodVectorsNearby(tilesAround, currentPosition);
            searchVectorsNearby(tilesAround, currentPosition);

            List<Tactic> toChoseFrom = tactics.Where(r => (r.returnMode == returnMode) && (r.edge==edge)).ToList();

            if (needMoreTraining(toChoseFrom, out List < Tactic > needsMoreData))
            {
                decision = Support.S_PickRandomTactic(needsMoreData);
            }
            else
            {
                decision = Support.S_LastFromTheSortedList(toChoseFrom);
            }
            return decision;
        }
        //decide where to next
        //todo fuction that compares vector.weight values
        //for choosing a direction
        //pick random vector for random direction
        //follow vector or the pheromones vs go into opposite direciton
        public static int NextStep(int x, int y, int currentPosition, List<Cell> CellsList, Tactic chosenTactic)
        {
            bool returnMode = chosenTactic.returnMode;
            //int number, bool returnMode, bool allTheSame, bool towards, bool edge, bool picThisDirection, int plusPoints, int minusPoints, int totalTimes, double raito
            int nextStep = -1;
            bool edge = chosenTactic.edge;

            //If there is an AntHill and Ant is returning go there
            List<Cell> tilesAround = TilesAround(CellsList, currentPosition);
            if (returnMode && AnthillNearby(tilesAround, out nextStep))
            {
                return nextStep;
            }
            //If there is Food and Ant is searching go there
            else if (!returnMode && FoodNearby(tilesAround, out nextStep))
            {
                return nextStep;
            }
            //if searching
            if(returnMode)
            {
                if (edge)
                {

                }
                else if (!edge)
                {

                }
            }
            else if(!returnMode) 
            {
                if (edge)
                {

                }
                else if (!edge)
                {

                }
            }
            
            //if pick random, cant go back and has to choose a random vector

            //special case if ant stands on the anthill or food

            return nextStep;
        }
        //splits nearby tiles into vectors and checks if there are any search pheromones on them
        public static void searchVectorsNearby(List<Cell> tilesAround, int currentPosition)
        {
            int[] arrSeachSmell = SmellForClues(tilesAround, currentPosition);
            GridVector v1 = new GridVector(arrSeachSmell[0], arrSeachSmell[1], arrSeachSmell[2], true, false);  
            GridVector v2 = new GridVector(arrSeachSmell[3], arrSeachSmell[4], arrSeachSmell[5], true, false);
            GridVector v3 = new GridVector(arrSeachSmell[6], arrSeachSmell[7], arrSeachSmell[8], true, false);
            GridVector v4 = new GridVector(arrSeachSmell[6], arrSeachSmell[3], arrSeachSmell[0], true, false);
            GridVector v5 = new GridVector(arrSeachSmell[7], arrSeachSmell[4], arrSeachSmell[1], true, false);
            GridVector v6 = new GridVector(arrSeachSmell[8], arrSeachSmell[5], arrSeachSmell[2], true, false);
            GridVector v7 = new GridVector(arrSeachSmell[6], arrSeachSmell[4], arrSeachSmell[2], true, false);
            GridVector v8 = new GridVector(arrSeachSmell[0], arrSeachSmell[4], arrSeachSmell[8], true, false);

        }
        //todo bool func(out vector) get the highest weight if there is a food vector nearby
        public static void foodVectorsNearby(List<Cell> tilesAround, int currentPosition) 
        {
            int[] arrFoodSmell = SmellForFood(tilesAround, currentPosition);
            GridVector v1 = new GridVector(arrFoodSmell[0], arrFoodSmell[1], arrFoodSmell[2], false, true);
            GridVector v2 = new GridVector(arrFoodSmell[3], arrFoodSmell[4], arrFoodSmell[5], false, true);
            GridVector v3 = new GridVector(arrFoodSmell[6], arrFoodSmell[7], arrFoodSmell[8], false, true);
            GridVector v4 = new GridVector(arrFoodSmell[6], arrFoodSmell[3], arrFoodSmell[0], false, true);
            GridVector v5 = new GridVector(arrFoodSmell[7], arrFoodSmell[4], arrFoodSmell[1], false, true);
            GridVector v6 = new GridVector(arrFoodSmell[8], arrFoodSmell[5], arrFoodSmell[2], false, true);
            GridVector v7 = new GridVector(arrFoodSmell[6], arrFoodSmell[4], arrFoodSmell[2], false, true);
            GridVector v8 = new GridVector(arrFoodSmell[0], arrFoodSmell[4], arrFoodSmell[8], false, true);

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
        //return celles around
        public static List<Cell> TilesAround(List<Cell> CellsList, int currentPosition)
        {
            int[] tilesAround = new int[]
                {
                    currentPosition + 1 - 100,  //-x  +y    top right
                    currentPosition + 1,        // x, +y    right
                    currentPosition + 1 + 100,  //+x, +y    bottom right
                    currentPosition - 100,      //-x,  y    top
                    currentPosition,            // x,  y    initial
                    currentPosition + 100,      //+x,  y    bottom     
                    currentPosition - 1 + 100,  //-x, -y    top left
                    currentPosition - 1,        // x, -y    left
                    currentPosition - 1 - 100   //+x, -y    bottom left
                };
            List<Cell> CellsAround = new List<Cell>();
            foreach (int i in tilesAround)
            {
                if(i>0&&i<10001)
                {
                    CellsAround.Add(CellsList[i]);
                }
            }
            return CellsAround;
        }     
        //return location if Anthouse nearby
        private static bool AnthillNearby(List<Cell> tilesAround, out int position)
        {
            position = 0;
            foreach(Cell cell in tilesAround)
            {
                if (cell.AnthillPosition)
                {
                    if (Support.S_CoordinatesToList(cell.X, cell.Y, out int result)) 
                    {
                        position = result;
                        return true;
                    }
                }
            }
            return false;
        }
        //return location if there is Food nearby
        private static bool FoodNearby(List<Cell> tilesAround, out int position)
        {
            {
                position = 0;
                foreach (Cell cell in tilesAround)
                {
                    if (cell.FoodPosition)
                    {
                        if (Support.S_CoordinatesToList(cell.X, cell.Y, out int result))
                        {
                            position = result;
                            return true;
                        }
                    }
                }
                return false;
            }
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
        //get arr with values of food feromones
        public static int[] SmellForFood(List<Cell> CellsList, int currentPosition)
        {
            int[] arr = new int[CellsList.Count];
            for (int i=0; i < CellsList.Count; i++)
            {
                arr[i] = CellsList[i].FoodFeromones;
            }
               /* {
                    Support.S_Smell(Grid, currentPosition + 1 - 100),       //  +y-x  
                    Support.S_Smell(Grid, currentPosition + 1),             //  +y
                    Support.S_Smell(Grid, currentPosition + 1 + 100),       //  +x+y
                    Support.S_Smell(Grid, currentPosition - 100),           //  -x
                    Support.S_Smell(Grid, currentPosition),                 //current position
                    Support.S_Smell(Grid, currentPosition + 100),           //  +x
                    Support.S_Smell(Grid, currentPosition - 1 - 100),       //  -x-y
                    Support.S_Smell(Grid, currentPosition - 1),             //  -y
                    Support.S_Smell(Grid, currentPosition - 1 + 100)        //  -y+x
                };*/
            return arr;
        }
        //get arr with values of search feromones
        public static int[] SmellForClues(List<Cell> CellsList, int currentPosition)
        {
            int[] arr = new int[CellsList.Count];
            for (int i = 0; i < CellsList.Count; i++)
            {
                arr[i] = CellsList[i].SearchFeromones;
            }
           /* int[] arr = new int[]
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
                };*/
            return arr;
        }
    }
}
