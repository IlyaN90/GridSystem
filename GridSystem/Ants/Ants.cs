using GridSystem.Grid;
using System;
using System.Collections.Generic;
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

        private readonly int meal = 15; 

        public Ants(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.food = 0;
            this.loot = 0;
            this.timer = 0;
            this.age = 0;
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
            int foodSmell = Support.S_Smell(Grid, curretnPosition);
            int otherSmell = Support.S_OtherSmell(Grid, curretnPosition);
            int[] arrFoodSmell = SmellForFood(Grid, curretnPosition);
            int[] arrCluesSmell = SmellForClues(Grid, curretnPosition);
            if (loot > 0)
            {
                //beräkna nästa steg hem
                Support.S_MarkCell(Grid, curretnPosition, true);
            }
            else
            {
                if (foodSmell > 0)
                {
                    //följ where foodSmell>0&&-1
                }
                else
                {
                    //följ food lukten 
                }
                Support.S_MarkCell(Grid, curretnPosition, false);
            }
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
                    Support.S_Smell(Grid, curretnPosition - 1 + 100),       //  -y+x
                    Support.S_Smell(Grid, curretnPosition + 100),           //  +x
                    Support.S_Smell(Grid, curretnPosition + 1 + 100),       //  +x+y
                    Support.S_Smell(Grid, curretnPosition + 1),             //  +y
                    Support.S_Smell(Grid, curretnPosition - 1),             //  -x
                    Support.S_Smell(Grid, curretnPosition + 1 - 100),       //  +y-x  
                    Support.S_Smell(Grid, curretnPosition - 100),           //  -x
                    Support.S_Smell(Grid, curretnPosition - 1 - 100)        //  -x-y
                };
            return arr;
        }

        public int[] SmellForClues(GridClass Grid, int curretnPosition)
        {
            int[] arr = new int[]
                {
                    Support.S_OtherSmell(Grid, curretnPosition - 1 + 100),       //  -y+x
                    Support.S_OtherSmell(Grid, curretnPosition + 100),           //  +x
                    Support.S_OtherSmell(Grid, curretnPosition + 1 + 100),       //  +x+y
                    Support.S_OtherSmell(Grid, curretnPosition + 1),             //  +y
                    Support.S_OtherSmell(Grid, curretnPosition - 1),             //  -x
                    Support.S_OtherSmell(Grid, curretnPosition + 1 - 100),       //  +y-x  
                    Support.S_OtherSmell(Grid, curretnPosition - 100),           //  -x
                    Support.S_OtherSmell(Grid, curretnPosition - 1 - 100)        //  -x-y
                };
            return arr;
        }
    }
}
