using GridSystem.Grid;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public class Ants
    {
        private int food;
        private int loot;
        private int timer;
        private int age;
        private int x;
        private int y;
        private List<Tactic> tactics;

        private readonly int meal = 15; 

        public Ants(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.food = 0;
            this.loot = 0;
            this.timer = 0;
            this.age = 0;
            Tactic tactic = new Tactic();
            //read from file or create file and genreate tacticsList
            tactics = tactic.CreateTacticsList();
        }


        public void NewTurn(GridClass Grid)
        {
            if(timer < 50) 
            { 
                age += 1;
                food -= 1;
                if (food <= 0)
                {
                    food = 0;
                    timer += 1;
                }
                int curretnPosition = Support.S_CoordinatesToList(x, y);
                Actions(Grid, curretnPosition);
                CheckForFood(Grid, curretnPosition);
                Navigate(Grid, curretnPosition);
            }
        }

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
                if (carringLoot)
                {
                    Grid.CellGrid[curretnPosition].Food += loot;
                    loot = 0;
                }
            }
            if (foodGround&&!carringLoot)
            {
                Grid.CellGrid[curretnPosition].Food=Loot(Grid.CellGrid[curretnPosition].Food);
            }
            if (food == 0)
            {
                int foodEaten = Eat(Grid.CellGrid[curretnPosition].Food);
                Grid.CellGrid[curretnPosition].Food -= foodEaten;
            }
        }

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
            int cell = Decision.MakeTheDecision(returnMode, arrFoodSmell, arrCluesSmell, tactics);


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
        }

        public bool CheckForFood(GridClass Grid, int curretnPosition)
        {
            int foodSource = Grid.CellGrid[curretnPosition].Food;
            if (foodSource == 0 && Grid.CellGrid[curretnPosition].FoodPosition)
            {
                Grid.CellGrid[curretnPosition].FoodPosition = false;
            }
            return Grid.CellGrid[curretnPosition].FoodPosition;
        }

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

        public int[] SmellForClues(GridClass Grid, int curretnPosition)
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
    }
}
