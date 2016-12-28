using System;
using System.Collections.Generic;

namespace SudokuEvo
{
    class SudokuEvoProgram
    {
        static Random rnd = new Random(0);
        static void Main(string[] args)
        {
            Console.WriteLine("Begin solving Sudoku");
            Console.WriteLine("The problem is: ");
            int[][] problem = new int[9][];
            problem[0] = new int[] { 0, 4, 5, 0, 0, 0, 0, 0, 1 };
            problem[1] = new int[] { 3, 0, 0, 1, 9, 0, 0, 0, 5 };
            problem[2] = new int[] { 0, 8, 0, 0, 0, 0, 0, 0, 0 };
            problem[3] = new int[] { 0, 0, 0, 0, 0, 0, 3, 0, 0 };
            problem[4] = new int[] { 0, 0, 0, 0, 8, 0, 0, 2, 0 };
            problem[5] = new int[] { 5, 6, 0, 0, 2, 0, 7, 0, 0 };
            problem[6] = new int[] { 9, 0, 0, 0, 0, 6, 0, 0, 2 };
            problem[7] = new int[] { 0, 5, 3, 0, 0, 4, 0, 0, 6 };
            problem[8] = new int[] { 0, 0, 0, 0, 0, 7, 0, 8, 0 };

            //problem[0] = new int[] { 0, 8, 0, 0, 0, 7, 0, 1, 0 };
            //problem[1] = new int[] { 9, 0, 0, 0, 0, 0, 0, 5, 0 };
            //problem[2] = new int[] { 0, 0, 0, 2, 0, 0, 0, 0, 3 };
            //problem[3] = new int[] { 6, 0, 0, 0, 0, 3, 0, 0, 9 };
            //problem[4] = new int[] { 0, 0, 0, 0, 5, 0, 0, 0, 0 };
            //problem[5] = new int[] { 2, 0, 0, 8, 0, 0, 0, 0, 4 };
            //problem[6] = new int[] { 4, 0, 0, 0, 0, 6, 0, 0, 0 };
            //problem[7] = new int[] { 0, 7, 0, 0, 0, 0, 0, 0, 5 };  
            //problem[8] = new int[] { 0, 1, 0, 4, 0, 0, 0, 8, 0 };

            //problem[0] = new int[] { 8, 0, 0, 0, 0, 0, 0, 0, 0 };
            //problem[1] = new int[] { 0, 0, 3, 6, 0, 0, 0, 0, 0 };
            //problem[2] = new int[] { 0, 7, 0, 0, 9, 0, 2, 0, 0 };
            //problem[3] = new int[] { 0, 5, 0, 0, 0, 7, 0, 0, 0 };
            //problem[4] = new int[] { 0, 0, 0, 0, 4, 5, 7, 0, 0 };
            //problem[5] = new int[] { 0, 0, 0, 1, 0, 0, 0, 3, 0 };
            //problem[6] = new int[] { 0, 0, 1, 0, 0, 0, 0, 6, 8 };
            //problem[7] = new int[] { 0, 0, 8, 5, 0, 0, 0, 1, 0 };
            //problem[8] = new int[] { 0, 9, 0, 0, 0, 0, 4, 0, 0 };

            //problem[0] = new int[] { 0, 0, 6, 2, 0, 0, 0, 8, 0 };
            //problem[1] = new int[] { 0, 0, 8, 9, 7, 0, 0, 0, 0 };
            //problem[2] = new int[] { 0, 0, 4, 8, 1, 0, 5, 0, 0 };
            //problem[3] = new int[] { 0, 0, 0, 0, 6, 0, 0, 0, 2 };
            //problem[4] = new int[] { 0, 7, 0, 0, 0, 0, 0, 3, 0 };
            //problem[5] = new int[] { 6, 0, 0, 0, 5, 0, 0, 0, 0 };
            //problem[6] = new int[] { 0, 0, 2, 0, 4, 7, 1, 0, 0 };
            //problem[7] = new int[] { 0, 0, 3, 0, 2, 8, 4, 0, 0 };
            //problem[8] = new int[] { 0, 5, 0, 0, 0, 1, 2, 0, 0 };
            DisplayMatrix(problem);
            int numOrganisms = 200;
            int maxEpochs = 5000;
            int maxRestarts = 20000;
            int[][] soln = Solve(problem, numOrganisms,
              maxEpochs, maxRestarts);
            Console.WriteLine("Best solution found: ");
            DisplayMatrix(soln);
            int err = Error(soln);
            if (err == 0)
                Console.WriteLine("Success \n");
            else
                Console.WriteLine("Did not find optimal solution \n");
            Console.WriteLine("End Sudoku demo");
            Console.ReadLine();
        }


        public static int[][] Solve(int[][] problem, int numOrganisms, int maxEpochs, int maxRestarts)
        {
            int err = int.MaxValue;
            int seed = 0;
            int[][] best = null;
            int attempt = 0;
            while (err != 0 && attempt < maxRestarts)
            {
                Console.WriteLine("\nseed = " + seed);
                rnd = new Random(seed);
                best = SolveEvo(problem, numOrganisms, maxEpochs);
                err = Error(best);
                ++seed;
                ++attempt;
            }
            return best;
        }
        public static void DisplayMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var cell = matrix[i][j];
                    if (cell == 0)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(cell + " ");
                    }
                    if (j != 0 && j % 3 == 2)
                    {
                        Console.Write("  ");
                    }

                }
                Console.WriteLine();
            }
        }
        public static int[][] SolveEvo(int[][] problem,
          int numOrganisms, int maxEpochs)
        {
            int numWorker = (int)(numOrganisms * 0.90);
            int numExplorer = numOrganisms - numWorker;
            Organism[] hive = new Organism[numOrganisms];

            int bestError = int.MaxValue;
            int[][] bestMatrix = null;

            // Initialize each Organism
            int organismType = -1;
            for (int i = 0; i < numOrganisms; i++)
            {
                if (i < numWorker)
                {
                    organismType = 0;
                }
                else
                {
                    organismType = 1;
                }
                int[][] rndMatrix = RandomMatrix(problem);
                int err = Error(rndMatrix);
                hive[i] = new Organism(organismType, rndMatrix, err, 0);
                if (err < bestError)
                {
                    bestError = err;
                    bestMatrix = DuplicateMatrix(rndMatrix);
                }
            }
            int epoch = 0;
            while (epoch < maxEpochs)
            {
                if (epoch % 1000 == 0)
                {
                    Console.Write("epoch = " + epoch);
                    Console.WriteLine(" best error = " + bestError);
                }

                if (bestError == 0)  // solution found
                    break;
                for (int i = 0; i < numOrganisms; ++i)
                {
                    // Process each Organism
                    if (hive[i].type == 0)//worker
                    {
                        int[][] neighborMatrix = NeighborMatrix(problem, hive[i].matrix);
                        int neighborMatrixErr = Error(neighborMatrix);
                        double p = rnd.NextDouble();
                        if (neighborMatrixErr < hive[i].error || p < 0.001)
                        {
                            hive[i].matrix = DuplicateMatrix(neighborMatrix);
                            hive[i].error = neighborMatrixErr;
                            if (neighborMatrixErr < hive[i].error)
                            {
                                hive[i].age = 0;
                            }
                            if (neighborMatrixErr < bestError)
                            {
                                bestError = neighborMatrixErr;
                                bestMatrix = DuplicateMatrix(neighborMatrix);
                            }
                        }
                        else
                        {
                            ++hive[i].age;
                            if (hive[i].age > 1000)
                            {
                                int[][] m = RandomMatrix(problem);
                                hive[i] = new Organism(0, m, Error(m), 0);
                            }
                        }
                    }
                    else if (hive[i].type == 1) //explorer
                    {
                        int[][] rndMatrix = RandomMatrix(problem);
                        hive[i].matrix = DuplicateMatrix(rndMatrix);
                        hive[i].error = Error(rndMatrix);
                        if (hive[i].error < bestError)
                        {
                            bestError = hive[i].error;
                            bestMatrix = DuplicateMatrix(hive[i].matrix);
                        }
                    }
                }
                // Merge best worker with best explorer, increment epoch
                int bestWorkerIndex = 0;
                int worstWorkerIndex = 0;
                int smallestWorkerError = hive[0].error;
                int largestWorkerError = hive[0].error;
                for (int i = 0; i < numWorker; i++)
                {
                    if (hive[i].error < smallestWorkerError)
                    {
                        smallestWorkerError = hive[i].error;
                        bestWorkerIndex = i;
                    }
                    if (hive[i].error > largestWorkerError)
                    {
                        largestWorkerError = hive[i].error;
                        worstWorkerIndex = i;
                    }
                }
                int bestExplorerIndex = numWorker;
                int smallestExplorerError = hive[numWorker].error;
                for (int i = numWorker; i < numOrganisms; i++)
                {
                    if (hive[i].error < smallestExplorerError)
                    {
                        smallestExplorerError = hive[i].error;
                        bestExplorerIndex = i;
                    }
                }
                int[][] merged = MergeMatrices(hive[bestWorkerIndex].matrix, hive[bestExplorerIndex].matrix);
                hive[worstWorkerIndex] = new Organism(0, merged, Error(merged), 0);
                if (hive[worstWorkerIndex].error < bestError)
                {
                    bestError = hive[worstWorkerIndex].error;
                    bestMatrix = DuplicateMatrix(merged);
                }
                ++epoch;
            }
            return bestMatrix;
        }
        public static int[][] RandomMatrix(int[][] problem)
        {
            int[][] result = DuplicateMatrix(problem);
            for (int block = 0; block < 9; ++block)
            {
                List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int i = 0; i < numbers.Count; i++)
                {
                    int swapIndex = rnd.Next(i, numbers.Count);
                    int tmp = numbers[i];
                    numbers[i] = numbers[swapIndex];
                    numbers[swapIndex] = tmp;
                }
                int[] corner = Corner(block);
                int startRow = corner[0];
                int startColum = corner[1];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int cell = problem[startRow + i][startColum + j];
                        if (cell != 0)
                        {
                            numbers.Remove(cell);
                        }
                    }
                }
                int numbersIndex = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (result[startRow + i][startColum + j] == 0)
                        {
                            result[startRow + i][startColum + j] = numbers[numbersIndex];
                            ++numbersIndex;
                        }
                    }
                }
            }
            return result;
        }
        public static int[] Corner(int block)
        {
            return new int[] { block / 3 * 3, (block - block / 3 * 3) * 3 };
        }
        public static int Block(int r, int c)
        {
            return c / 3 + (r / 3) * 3;
        }
        public static int[][] NeighborMatrix(int[][] problem, int[][] matrix)
        {
            int[][] result = DuplicateMatrix(matrix);
            int block = rnd.Next(0, 9); //[0,8]
            while (block >= 0)
            {
                List<int[]> canSwapped = new List<int[]>();
                int[] corner = Corner(block);
                int startRow = corner[0];
                int startColum = corner[1];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (problem[startRow + i][startColum + j] == 0)
                        {
                            canSwapped.Add(new int[] { startRow + i, startColum + j });
                        }
                    }
                }
                if (canSwapped.Count < 2)
                {
                    block = rnd.Next(0, 9);
                    continue;
                }
                else
                {
                    // pick two. suppose there are 4 possible cells 0,1,2,3
                    int k1 = rnd.Next(canSwapped.Count);  // 0,1,2,3
                    int inc = rnd.Next(1, canSwapped.Count);  // 1,2,3
                    int k2 = (k1 + inc) % canSwapped.Count;
                    //Console.WriteLine("k1 k2 = " + k1 + " " + k2);

                    int r1 = canSwapped[k1][0];
                    int c1 = canSwapped[k1][1];
                    int r2 = canSwapped[k2][0];
                    int c2 = canSwapped[k2][1];
                    int tmp = result[r1][c1];
                    result[r1][c1] = result[r2][c2];
                    result[r2][c2] = tmp;
                    break;
                }
            }
            return result;
        }
        //public static int Error1(int[][] matrix)
        //{
        //    int err = 0;
        //    // assumes blocks are OK (one each 1-9)

        //    // rows error
        //    for (int i = 0; i < 9; ++i)  // each row
        //    {
        //        int[] counts = new int[9];  // [0] = count of 1s, [1] = count of 2s
        //        for (int j = 0; j < 9; ++j)  // walk down column of curr row
        //        {
        //            int v = matrix[i][j];  // 1 to 9
        //            ++counts[v - 1];
        //        }

        //        for (int k = 0; k < 9; ++k)  // number missing 
        //        {
        //            if (counts[k] == 0)
        //                ++err;
        //        }

        //    }  // each row

        //    // columns error
        //    for (int j = 0; j < 9; ++j)  // each column
        //    {
        //        int[] counts = new int[9];  // [0] = count of 1s, [1] = count of 2s

        //        for (int i = 0; i < 9; ++i)  // walk down 
        //        {
        //            int v = matrix[i][j];  // 1 to 9
        //            ++counts[v - 1];  // counts[0-8]
        //        }

        //        for (int k = 0; k < 9; ++k)  // number missing in the colum
        //        {
        //            if (counts[k] == 0)
        //                ++err;
        //        }
        //    } // each column

        //    return err;
        //} // Error
        public static int[][] MergeMatrices(int[][] m1, int[][] m2)
        {
            int[][] result = DuplicateMatrix(m1);
            for (int block = 0; block < 9; ++block)
            {
                double pr = rnd.NextDouble();
                if (pr < 0.50)
                {
                    int[] corner = Corner(block);
                    for (int i = corner[0]; i < corner[0] + 3; ++i)
                        for (int j = corner[1]; j < corner[1] + 3; ++j)
                            result[i][j] = m2[i][j];
                }
            }
            return result;
        }
        public static int Error(int[][] matrix)
        {
            int err = 0;
            for (int i = 0; i < 9; i++) //row
            {
                int[] row = matrix[i];
                for (int index = 0; index < row.Length; index++)
                {
                    for (int compareIndex = index + 1; compareIndex < row.Length; compareIndex++)
                    {
                        if (row[index] == row[compareIndex])
                        {
                            ++err;
                            break;
                        }
                    }
                }
            }
            for (int j = 0; j < 9; j++) //colum
            {
                int[] colum = new int[] { matrix[0][j], matrix[1][j], matrix[2][j], matrix[3][j], matrix[4][j], matrix[5][j], matrix[6][j], matrix[7][j], matrix[8][j] };
                for (int index = 0; index < colum.Length; index++)
                {
                    for (int compareIndex = index + 1; compareIndex < colum.Length; compareIndex++)
                    {
                        if (colum[index] == colum[compareIndex])
                        {
                            ++err;
                            break;
                        }
                    }
                }
            }
            return err;
        }
        public static int[][] DuplicateMatrix(int[][] matrix)
        {
            int[][] result = CreateMatrix(matrix.Length);
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    result[i][j] = matrix[i][j];
                }
            }
            return result;
        }
        public static int[][] CreateMatrix(int n)
        {
            int[][] result = new int[n][];
            for (int i = 0; i < n; ++i)
                result[i] = new int[n];
            return result;
        }
    }
}
