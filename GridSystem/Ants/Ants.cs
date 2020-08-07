using GridSystem.Grid;
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

        private readonly int meal = 15; 

        public Ants(int x, int y)
        {
            this.x = x;
            this.y = y;
            lastXpos = 0;
            lastYpos = 0;
            this.food = 0;
            this.totalFoodCollected = 0;
            this.loot = 0;
            this.timer = 0;
            this.age = 0;
        }

        //if Ant is alive, do Actions, checkForFood, Navigate
        public void NewTurn(GridClass Grid)
        {
            if (timer < 50) 
            {
                lastXpos = x;
                lastYpos = y;
                age += 1;
                food -= 1;
                if (food <= 0)
                {
                    food = 0;
                    timer += 1;
                }
                int curretnPosition = Support.S_CoordinatesToList(x, y);
                Grid.CellGrid[curretnPosition].AntPostition = false;
                Actions(Grid, curretnPosition);
                CheckForFood(Grid, curretnPosition);
                //Navigate(Grid, curretnPosition);
                TestNavigate(Grid, curretnPosition);//Ant possesses Home and food coordinates
            }
        }
        //loot|stash|eat
        public void Actions(GridClass Grid, int curretnPosition) 
        {
            bool home = Grid.CellGrid[curretnPosition].AnthillPosition;
            bool foodGround = CheckForFood(Grid, curretnPosition);
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
                    Grid.CellGrid[curretnPosition].Food += loot;
                    loot = 0;
                }
            }
            if (foodGround&&!carringLoot)
            {
                Grid.CellGrid[curretnPosition].Food=Loot(Grid.CellGrid[curretnPosition].Food);
                ResetLastPosition();
            }
            if (food == 0)
            {
                int foodEaten = Eat(Grid.CellGrid[curretnPosition].Food);
                Grid.CellGrid[curretnPosition].Food -= foodEaten;
            }
        }
        //chose where too next
        public void Navigate(GridClass Grid, int curretnPosition)
        {
            int[] arrFoodSmell = SmellForFood(Grid, curretnPosition);
            int[] arrCluesSmell = SmellForClues(Grid, curretnPosition);
            bool returnMode = false;

            //if tactic.totalTimes is <100 force ants to take that decision
            //else take decisions that has raito >1 
            //when does an ant need to make a new decision
            //what are criterias for right or wrong decision?
            //when to document failure or success
            int cell = Decision.MakeTheDecision(returnMode, arrFoodSmell, arrCluesSmell, Grid.tactics);

            if (loot > 0)
            {
                //beräkna nästa steg hem
                returnMode = true;
                Support.S_MarkCell(Grid, curretnPosition, returnMode);
            }
            else
            {
                //leta mat
                returnMode = false;
                Support.S_MarkCell(Grid, curretnPosition, returnMode);
            }

            int nextMove = 0;
            switch (cell)
            {
                case 1:
                    nextMove = curretnPosition + 1 - 100;
                    break;
                case 2:
                    nextMove = curretnPosition + 1;
                    break;
                case 3:
                    nextMove = curretnPosition + 1 + 100;
                    break;
                case 4:
                    nextMove = curretnPosition - 100;
                    break;
                case 6:
                    nextMove = curretnPosition + 100;
                    break;
                case 7:
                    nextMove = curretnPosition - 1 - 100;
                    break;
                case 8:
                    nextMove = curretnPosition - 1;
                    break;
                case 9:
                    nextMove = curretnPosition - 1 + 100;
                    break;
            }
            int[] nextMoveArr = Support.S_ListToCoordinates(nextMove);
            this.x =nextMoveArr[0];
            this.y = nextMoveArr[1];
            Grid.CellGrid[curretnPosition].AntPostition = true;
        }
        //is Ant standing on food?
        public bool CheckForFood(GridClass Grid, int curretnPosition)
        {
            int foodSource = Grid.CellGrid[curretnPosition].Food;
            if (foodSource == 0 && Grid.CellGrid[curretnPosition].FoodPosition)
            {
                Grid.CellGrid[curretnPosition].FoodPosition = false;
            }
            return Grid.CellGrid[curretnPosition].FoodPosition;
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
        public int[] SmellForFood(GridClass Grid, int curretnPosition)
        {
            int[] arr = new int[]
                {
                    Support.S_Smell(Grid, curretnPosition + 1 - 100),       //  +y-x  
                    Support.S_Smell(Grid, curretnPosition + 1),             //  +y
                    Support.S_Smell(Grid, curretnPosition + 1 + 100),       //  +x+y
                    Support.S_Smell(Grid, curretnPosition - 100),           //  -x
                    Support.S_Smell(Grid, curretnPosition),                 //current position
                    Support.S_Smell(Grid, curretnPosition + 100),           //  +x
                    Support.S_Smell(Grid, curretnPosition - 1 - 100),       //  -x-y
                    Support.S_Smell(Grid, curretnPosition - 1),             //  -y
                    Support.S_Smell(Grid, curretnPosition - 1 + 100)        //  -y+x
                };
            return arr;
        }
        //get arr with values of "i am looking for food" feromones
        public int[] SmellForClues(GridClass Grid, int curretnPosition)
        {
            int[] arr = new int[]
                {
                    Support.S_OtherSmell(Grid, curretnPosition + 1 - 100),       //  +y-x  
                    Support.S_OtherSmell(Grid, curretnPosition + 1),             //  +y
                    Support.S_OtherSmell(Grid, curretnPosition + 1 + 100),       //  +x+y
                    Support.S_OtherSmell(Grid, curretnPosition - 100),           //  -x
                    Support.S_OtherSmell(Grid, curretnPosition),                 //current position
                    Support.S_OtherSmell(Grid, curretnPosition + 100),           //  +x
                    Support.S_OtherSmell(Grid, curretnPosition - 1 - 100),       //  -x-y
                    Support.S_OtherSmell(Grid, curretnPosition - 1),             //  -y
                    Support.S_OtherSmell(Grid, curretnPosition - 1 + 100)        //  -y+x
                };
            return arr;
        }
        //if at the antHill or foodPosition, reset last position so Ant can go the way it came
        private void ResetLastPosition()
        {
            lastXpos = 0;
            lastYpos = 0;
        }
    }
}
