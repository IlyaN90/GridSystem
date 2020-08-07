using GridSystem.Ants;
using GridSystem.Grid;
using GridSystem.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            GridClass Grid = new GridClass(100);
            List<Tactic> tactics = Grid.GetTacticsList();
            Grid.CellGrid = Grid.PopulateGrid();
            Anthill anthill = new Anthill(50, 50);
            int antsPlace=Grid.MarkAnthill(anthill.X, anthill.Y);
            Grid.PlaceFood(antsPlace);
            PrintGrid.OutputLoop(100, Grid, anthill);
            ReadWriteData.Write(tactics);
            Console.ReadKey();
        }
    }
}
