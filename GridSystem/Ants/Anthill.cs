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

        public List<Ants> ants;

        public int X { get { return this.x; } set { this.x = value; } }
        public int Y { get { return this.y; } set { this.y = value; } }

        public Anthill(int x, int y)
        {
            ants = new List<Ants> { };
            this.x = x;
            this.y = y;
            stashedFood = 110;
            PopulateAnthill();
        }
        //create Ants in bulk
        private void PopulateAnthill()
        {
            //for(int i = 0; i<10; i++)
            //{
                //System.Threading.Thread.Sleep(1500);
                CreateNewAnt();
            //}
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
        //returns false if at least one Ant is alive
        public bool AntsAlive()
        {
            bool gameOver = true;
            foreach (Ants ant in ants)
            {
                if (ant.timer < 50)
                {
                    gameOver = false;
                    break;
                }
            }
            return gameOver;
        }
    }
}
