using GridSystem.Grid;
using GridSystem.VariousErrors;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public partial class Ants
    {
        private int food;
        private int totalFoodCollected; //for stats
        private int loot;
        private int age;    //for stats
        private int lastXpos;
        private int lastYpos;
        private int x;
        private int y;
        public int timer;
        private int tacticNumber;
        private int stepsSinceDecision;

        private readonly int meal = 15; 

        public Ants(int x, int y)
        {
            tacticNumber = 0;
            this.x = x;
            this.y = y;
            lastXpos = 0;
            lastYpos = 0;
            this.food = 0;
            this.totalFoodCollected = 0;
            this.loot = 0;
            this.timer = 0;
            this.age = 0;
            stepsSinceDecision = 0;
        }

        //if Ant is alive, do Actions, checkForFood, Navigate
        public void NewTurn(GridClass Grid)
        {
            if (timer < 50) 
            {
                if (stepsSinceDecision > 51)
                {
                    Failure(Grid.tactics);
                }
                lastXpos = x;
                lastYpos = y;
                age += 1;
                food -= 1;
                if (food <= 0)
                {
                    food = 0;
                    timer += 1;
                }
                int currentPosition = -1;
                try { 
                    if (Support.S_CoordinatesToList(x, y, out int position))
                    {
                        currentPosition = position;
                        Grid.CellGrid[currentPosition].AntPostition = false;
                        Actions(Grid, currentPosition);
                        CheckForFood(Grid, currentPosition);
                        //Navigate(Grid, currentPosition);
                        TestNavigate(Grid, currentPosition);//Ant possesses Home and food coordinates
                    }
                    else
                    {
                        throw new ex_OffTheGrid("Ant has escaped the Grid!");
                    }
                }
                catch(ex_OffTheGrid)
                {
                }
            }
            else
            {
                if (tacticNumber > 0)
                {
                    Failure(Grid.tactics);
                }
            }
        }
        //loot|stash|eat
        public void Actions(GridClass Grid, int currentPosition) 
        {
            bool home = Grid.CellGrid[currentPosition].AnthillPosition;
            bool foodGround = CheckForFood(Grid, currentPosition);
            bool carringLoot = false;
            if (loot > 0)
            {
                carringLoot = true;
            }
            if (home)
            {
                ResetLastPosition();
                if (carringLoot)
                {
                    totalFoodCollected += loot;
                    Grid.CellGrid[currentPosition].Food += loot;
                    loot = 0;
                    carringLoot = false;
                    Success(Grid.tactics);
                }
            }
            if (!home&&foodGround&&!carringLoot)
            {
                ResetLastPosition();
                Grid.CellGrid[currentPosition].Food=Loot(Grid.CellGrid[currentPosition].Food);
                Success(Grid.tactics);
            }
            if (food == 0)
            {
                int foodEaten = Eat(Grid.CellGrid[currentPosition].Food);
                Grid.CellGrid[currentPosition].Food -= foodEaten;
            }
        }
        //chose where too next
        public void Navigate(GridClass Grid, int currentPosition)
        {
            bool returnMode = false;
            //if tactic.totalTimes is <100 force ants to take that decision
            //else take decisions that has raito >1 
            //when does an ant need to make a new decision
            //what are criterias for right or wrong decision?
            //when to document failure or success
            if (loot > 0)
            {
                //beräkna nästa steg hem
                returnMode = true;
                Support.S_MarkCell(Grid, currentPosition, returnMode);
            }
            else
            {
                //leta mat
                returnMode = false;
                Support.S_MarkCell(Grid, currentPosition, returnMode);
            }

            if (tacticNumber == 0)
            {
                tacticNumber = Decision.ChooseTactic(x, y, returnMode, Grid, currentPosition);
            }

            int nextMove = Decision.NextStep(x, y, currentPosition, returnMode, Grid, tacticNumber);

            int[] nextMoveArr = Support.S_ListToCoordinates(nextMove);
            this.x =nextMoveArr[0];
            this.y = nextMoveArr[1];
            try
            {
                if (Support.S_CoordinatesToList(x, y, out int result))
                {
                    currentPosition = result;
                    Grid.CellGrid[currentPosition].AntPostition = true;
                }
                else
                {
                    throw new ex_OffTheGrid("Ant has escaped the Grid!");
                }
            }
            catch (ex_OffTheGrid)
            {
            }
        }

        //is Ant standing on food?
        public bool CheckForFood(GridClass Grid, int currentPosition)
        {
            int foodSource = Grid.CellGrid[currentPosition].Food;
            if (foodSource == 0 && Grid.CellGrid[currentPosition].FoodPosition)
            {
                Grid.CellGrid[currentPosition].FoodPosition = false;
            }
            return Grid.CellGrid[currentPosition].FoodPosition;
        }
        //if there is food pick it up
        public int Loot(int foodSource)
        {
            if (foodSource > 50)
            {
                loot = 50;
                foodSource -= 50;
            }
            else
            {
                loot += foodSource;
                foodSource = 0;
            }
            return foodSource;
        }
        //Eat as much as possible but not more than a meal
        public int Eat(int foodSource)
        {
            int foodEaten = 0;
            if (foodSource >= meal)
            {
                foodEaten = meal - food;
                loot -= 1;
            }
            else
            {
                foodEaten = foodSource;
            }
            food += foodEaten;
            timer = 0;
            return foodEaten;
        }
        //get arr with values of "i found food" feromones
        //if at the antHill or foodPosition, reset last position so Ant can go the way it came
        private void ResetLastPosition()
        {
            lastXpos = 0;
            lastYpos = 0;
        }
        //Adds a point to tactic, updates tactic stats, resets tacticNumber 
        private void Success(List<Tactic> tacticsList)
        {
            tacticsList[tacticNumber].addPoint();
            tacticsList[tacticNumber].totalTimes+=1;
            tacticsList[tacticNumber].CalculateRaito();
            tacticNumber = 0;
        }
        //Removes a point from tactic, updates tactic stats, resets tacticNumber 
        private void Failure(List<Tactic> tacticsList)
        {
            tacticsList[tacticNumber].removePoint();
            tacticsList[tacticNumber].totalTimes -= 1;
            tacticsList[tacticNumber].CalculateRaito();
            tacticNumber = 0;
        }
    }
}
