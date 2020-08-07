using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Grid
{
    public partial class GridClass
    {
        private int foodLocation;
        private int anthillLocation;

        public int[] RevealLocations()
        {
            int[] allLocations = new int[]
            {
                foodLocation,
                anthillLocation
            };
            return allLocations;
        }
    }
}
