using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem.VariousErrors
{
    public class ex_OffTheGrid : Exception
    {   
            public ex_OffTheGrid()
            {
            }

            public ex_OffTheGrid(string message)
                : base(message)
            {
            }

            public ex_OffTheGrid(string message, Exception inner)
                : base(message, inner)
            {
            }
    }
}
