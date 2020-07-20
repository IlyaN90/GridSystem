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

        public List<Tactic> tactics;

        public Tactic()
        {
            plusPoints = 0;
            minusPoints = 0;
            totalTimes = 0;
            raito = 0;
        }

        public Tactic(int plusPoints, int minusPoints, int totalTimes, double raito)
        {
            this.plusPoints = plusPoints;
            this.minusPoints = minusPoints;
            this.totalTimes = totalTimes;
            this.raito = raito;
        }

        public void addPoint() 
        {
            this.plusPoints += 1;
            this.totalTimes += 1;
        }
        public void removePoint()
        {
            this.minusPoints -= 1;
            this.totalTimes -= 1;
        }

        public void CalculateRaito()
        {
            this.raito = plusPoints/minusPoints + plusPoints%minusPoints;
        }

        public List<Tactic> CreateTacticsList()
        {
            //read from file or create file and genreate tacticsList
            List<Tactic> tactics = new List<Tactic> { };
            return tactics;
        }
    }
}
