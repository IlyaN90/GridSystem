using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Grid
{
    public partial class GridClass
    {
        private int partial_foodLocation;
        private int partial_anthillLocation;

        public int[] RevealLocations()
        {
            int[] allLocations = new int[]
            {
                partial_foodLocation,
                partial_anthillLocation
            };
            return allLocations;
        }
    }
}
