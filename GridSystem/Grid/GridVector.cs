using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GridSystem.Grid
{
    public class GridVector
    {
        public readonly int x;
        public readonly int y;
        public readonly int z;
        public bool search;
        public bool food;
        public bool isVector;
        public int weight;

        public GridVector(int x, int y, int z, bool search, bool food)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            int[] arr = new int[] {x,y,z };
            isVector = CheckIfVector(arr);
            if (isVector && search)
            {
                this.search = search;
                this.food = false;
            }
            if (isVector && food)
            {
                this.food = food;
                this.search = false;
            }
            this.food = food;
            weight= CalculateWeight(x,y,z);
        }

        private bool CheckIfVector(int[] args)
        {
            bool isVector = true;
            foreach (int i in args)
            {
                if (i <= 0)
                {
                    isVector = false;
                }
            }
            return isVector;
        }

        private int CalculateWeight(int x, int y, int z)
        {
            weight = (x + y + z) / 3 + (x + y + z) % 3;
            if (x > z)
            {
                weight *= (-1);
            }
            return weight;
        }
    }
}
