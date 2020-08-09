using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public class Tactic
    {
        public bool returnMode;
        public bool allTheSame;
        public bool towards;
        public bool edge;
        public bool picThisDirection;
        private int plusPoints;
        private int minusPoints;
        public int totalTimes;
        public double raito;
        public int number;

        public Tactic()
        {
            plusPoints = 0;
            minusPoints = 0;
            totalTimes = 0;
            raito = 0;
            number = 0;
        }

        public Tactic(int number, bool returnMode, bool allTheSame, bool towards, bool edge, bool picThisDirection, int plusPoints, int minusPoints, int totalTimes, double raito)
        {
            this.number = number;
            this.returnMode = returnMode;
            this.allTheSame = allTheSame;
            this.towards = towards;
            this.plusPoints = plusPoints;
            this.minusPoints = minusPoints;
            this.totalTimes = totalTimes;
            this.raito = raito;
        }

        //the tactic worked
        public void addPoint() 
        {
            this.plusPoints += 1;
            this.totalTimes += 1;
        }
        //the tactic failed
        public void removePoint()
        {
            this.minusPoints -= 1;
            this.totalTimes -= 1;
        }
        //get the raito plusPointsa/minusPoints
        public void CalculateRaito()
        {
            this.raito = plusPoints/minusPoints + plusPoints%minusPoints;
        }
        //create all possible strategies

    }
}
