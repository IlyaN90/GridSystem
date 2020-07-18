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
                if (food < 0)
                {
                    timer += 1;
                }
                Navigate(Grid);
            }
        }


        public void Navigate(GridClass Grid) 
        {
            int curretnPosition = Support.S_CoordinatesToList(x, y);
            LootAndEat(Grid, curretnPosition);

            int foodSmell = Support.S_Smell(Grid, curretnPosition);
            int otherSmell = Support.S_OtherSmell(Grid, curretnPosition);

                if (loot > 0)
                {
                    //beräkna nästa steg hem
                    Support.Mark(Grid, curretnPosition, true);
                }
                else
                {
                    int[] arr=LookAround(Grid, curretnPosition);
                    if (foodSmell > 0)
                    {
                    //följ where foodSmell>0&&-1
                }
                else
                    { 
                       //följ food lukten 
                    }
                    Support.Mark(Grid, curretnPosition, false);
                }
        }

        public void LootAndEat(GridClass Grid, int curretnPosition)
        {
            int foodSource = Grid.CellGrid[curretnPosition].Food;
            if (foodSource > 0)
            {
                if (Grid.CellGrid[curretnPosition].AnthillPosition)
                {
                    Grid.CellGrid[curretnPosition].Food += loot;
                    int foodEaten = Eat(foodSource);
                    Grid.CellGrid[curretnPosition].Food -= foodEaten;
                }
                else
                {
                    if (food < meal)
                    {
                        int foodEaten = Eat(foodSource);
                        Grid.CellGrid[curretnPosition].Food -= foodEaten;
                    }
                    Loot(foodSource);
                }
            }
            if (loot != 0 && food == 0)
            {
                int foodEaten = Eat(loot);
                loot -= foodEaten;
            }
        }

        public void Loot(int foodSource)
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

        public int[] LookAround(GridClass Grid, int curretnPosition)
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
    }
}
