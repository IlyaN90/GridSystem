using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public class Tactic
    {
        private bool returnMode;
        private bool allTheSame;
        private bool towards;
        private bool from;
        private int plusPoints;
        private int minusPoints;
        private int totalTimes;
        private double raito;

        public Tactic()
        {
            plusPoints = 0;
            minusPoints = 0;
            totalTimes = 0;
            raito = 0;
        }

        public Tactic(bool returnMode, bool allTheSame, bool towards, bool from, int plusPoints, int minusPoints, int totalTimes, double raito)
        {
            this.returnMode = returnMode;
            this.allTheSame = allTheSame;
            this.towards = towards;
            this.from = from;
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
