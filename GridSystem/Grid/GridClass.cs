using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Grid
{
    public class GridClass
    {
        private int len;
        public int ahX;
        public int ahY;
        public List<Cell> CellGrid;

        public GridClass(int cells)
        {
            this.len = cells;
            CellGrid = new List<Cell>() { };
        }

        public int Len 
        { 
            get
            {
                return this.len;
            } 
        }

        public List<Cell> PopulateGrid()
        {
            for (int x = 0; x < Len; x++) 
            {
                for(int y =0; y < Len; y++)
                {
                    Cell c = new Cell();
                    c.X = x;
                    c.Y = y;
                    c.Z = 0;
                    CellGrid.Add(c);
                }
            }
            int[] foodSpot = Support.S_FoodNodeLocation();
            CellGrid[foodSpot[0]].Food = foodSpot[1];
            return CellGrid;
        }

        public void MarkAnthill(int x, int y)
        {
            int theplace= Support.S_CoordinatesToList(x, y);
            CellGrid[theplace].AnthillPosition = true;
            CellGrid[theplace].Food = 110;
        }
    }
}
