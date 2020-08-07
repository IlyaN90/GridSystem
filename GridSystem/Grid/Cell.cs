using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Grid
{
    public class Cell
    {
        private int x;
        private int y;
        private int z;
        private int food;
        private int foodFeromones;
        private int searchFeromones;
        private bool antPostition;
        private bool anthillPosition;
        private bool foodPosition;

        public Cell()
        {
            z = 0;
            food = 0;
            foodFeromones = 0;
            searchFeromones = 0;
            antPostition = false;
            foodPosition = false;
        }

        public int X { get { return this.x; } set { this.x = value; } }
        public int Y { get { return this.y; } set { this.y = value; } }
        public int Z { get { return this.z; } set { this.z = value; } }
        public int Food { get { return this.food; } set { this.food = value; } }
        public int FoodFeromones { get { return this.foodFeromones; } set { this.foodFeromones = value; } }
        public int SearchFeromones { get { return this.searchFeromones; } set { this.searchFeromones = value; } }
        public bool AntPostition { get { return this.antPostition; } set { this.antPostition = value; } }

        public bool AnthillPosition { get { return this.anthillPosition; } set { this.anthillPosition = value; } }
        public bool FoodPosition { get { return this.anthillPosition; } set { this.anthillPosition = value; } }
    }
}
