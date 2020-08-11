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
            for (int x = 1; x < Len; x++) 
            {
                for(int y = 1; y < Len; y++)
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
            int antsPlace = 0;
            if (Support.S_CoordinatesToList(x, y, out int position))
            {
                antsPlace = position;
                CellGrid[antsPlace].AnthillPosition = true;
                CellGrid[antsPlace].Food = 110;
                partial_anthillLocation = antsPlace;
            }
            return partial_anthillLocation;
        }

        public int PlaceFood(int antsHillLocation)
        {
            int[] foodSpot = Support.S_FoodNodeLocation();
            bool spotIsBusy = true;
            while (spotIsBusy)
            {
                if(foodSpot[0] == antsHillLocation)
                {
                    foodSpot = Support.S_FoodNodeLocation();
                }
                else
                {
                    spotIsBusy = false;
                }
            }
            CellGrid[foodSpot[0]].FoodPosition = true;
            CellGrid[foodSpot[0]].Food = foodSpot[1];
            partial_foodLocation = foodSpot[0];
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
                //bool returnMode, bool allTheSame, bool towards, bool edge, bool picThisDirection, int plusPoints, int minusPoints, int totalTimes, double raito

                bool returnMode = true;
                bool allTheSame = true;
                bool towards = true;
                bool edge = false;
                bool picThisDirection = false;
                tactics = new List<Tactic>
                {
//searching 
                    new Tactic(1,!returnMode, !allTheSame, towards, !edge, !picThisDirection, 0, 0, 0, 0),
                    new Tactic(2, !returnMode, !allTheSame, !towards, !edge, !picThisDirection, 0, 0, 0, 0),
                    //random searching
                    new Tactic(3, !returnMode, allTheSame, !towards, !edge, !picThisDirection, 0, 0, 0, 0 ),
//going back
                    new Tactic(4, returnMode, !allTheSame, towards, !edge, !picThisDirection, 0, 0, 0, 0 ),
                    new Tactic(5, returnMode, !allTheSame, !towards, !edge, !picThisDirection, 0, 0, 0, 0 ),
                    //random going back
                    new Tactic(6, returnMode, allTheSame, !towards, !edge, !picThisDirection, 0, 0, 0, 0 ),
//edge searching
                    new Tactic(9, !returnMode, !allTheSame, towards, edge, !picThisDirection, 0, 0, 0, 0),
                    new Tactic(10, !returnMode, !allTheSame, !towards, edge, !picThisDirection, 0, 0, 0, 0),
                    //random Edge searching
                    new Tactic(7, !returnMode, allTheSame, towards, edge, picThisDirection, 0, 0, 0, 0),
                    new Tactic(8, !returnMode, allTheSame, towards, edge, !picThisDirection, 0, 0, 0, 0),
//edge going back
                    new Tactic(13, returnMode, !allTheSame, towards, edge, !picThisDirection, 0, 0, 0, 0),
                    new Tactic(14, returnMode, !allTheSame, !towards, edge, !picThisDirection, 0, 0, 0, 0),
                    //random Edge going back
                    new Tactic(11, returnMode, allTheSame, towards, edge, picThisDirection, 0, 0, 0, 0),
                    new Tactic(12, returnMode, allTheSame, towards, edge, !picThisDirection, 0, 0, 0, 0)
                };
                ReadWriteData.Write(tactics);
            }
            return tactics;
        }
    }
}
