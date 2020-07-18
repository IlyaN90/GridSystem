using GridSystem.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public class Anthill
    {
        private int x;
        private int y;
        int stashedFood;

        List<Ants> ants;

        public Anthill(int x, int y)
        {
            ants = new List<Ants> { };
            this.x = x;
            this.y = y;
            stashedFood = 110;
            PopulateAnthill();
        }

        private void PopulateAnthill()
        {
            for(int i = 0; i<10; i++)
            {
                //System.Threading.Thread.Sleep(1500);
                CreateNewAnt();
            }
        }
        //should be called when balance is under food:steps raito, but stashedfood is >= 60
        private void CreateNewAnt()
        {
            if (stashedFood >= 10) 
            {
                stashedFood -= 10;
                Ants Ant = new Ants(x, y);
                ants.Add(Ant);
            }
        }

        public int X { get { return this.x; } set { this.x = value; } }
        public int Y { get { return this.y; } set { this.y = value; } }
    }
}
