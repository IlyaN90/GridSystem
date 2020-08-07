using GridSystem.Ants;
using GridSystem.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Grid
{
    public partial class GridClass
    {
        private int len;
        public int ahX;
        public int ahY;
        public List<Cell> CellGrid;
        public List<Tactic> tactics;

        public GridClass(int cells)
        {
            this.len = cells;
            CellGrid = new List<Cell>() { };
            this.tactics = GetTacticsList();
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
            return CellGrid;
        }

        public int MarkAnthill(int x, int y)
        {
            int antsPlace = Support.S_CoordinatesToList(x, y);
            CellGrid[antsPlace].AnthillPosition = true;
            CellGrid[antsPlace].Food = 110;
            anthillLocation = antsPlace;
            return antsPlace;
        }

        public int PlaceFood(int antsPlace)
        {
            int[] foodSpot = Support.S_FoodNodeLocation();
            while (foodSpot[0] == antsPlace)
            {
                foodSpot = Support.S_FoodNodeLocation();
            }
            CellGrid[foodSpot[0]].FoodPosition = true;
            CellGrid[foodSpot[0]].Food = foodSpot[1];
            foodLocation = foodSpot[0];
            return foodSpot[0];
        }

        //read from file or create file and genreate tacticsList
        public List<Tactic> GetTacticsList()
        {
            if (ReadWriteData.Read(out List<Tactic> txtTactics))
            {
                tactics = txtTactics;
            }
            else
            {
                tactics = new List<Tactic> { };
                ReadWriteData.Write(tactics);
            }
            return tactics;
        }
    }
}
