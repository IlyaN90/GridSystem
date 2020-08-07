using GridSystem.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.Ants
{
    public partial class Ants
    {
        private int testAnthillCoordinates;
        private int testFoodCoordinates;

        public void TestNavigate(GridClass Grid, int curretnPosition)
        {
            int[] locations = Grid.RevealLocations();
            testAnthillCoordinates = locations[0];
            testFoodCoordinates = locations[1];

        }
    }
}
