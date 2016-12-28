using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuEvo
{
    class Organism
    {
        public int type;  // 0 = worker, 1 = explorer
        public int[][] matrix;
        public int error;
        public int age;
        public Organism(int type, int[][] m, int error, int age)
        {
            this.type = type;
            this.matrix = SudokuEvoProgram.DuplicateMatrix(m);
            this.error = error;
            this.age = age;
        }
    }
}
