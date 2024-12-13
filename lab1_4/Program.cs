namespace lab1_4
{
    public class Program4
    {
        public static void Main()
        {
            int rows = 4;
            int cols = 6;
            int[,] arr2D = GenRnd2DArray(rows, cols);
            int[][] arr2DEven = new int[arr2D.GetLength(0)][];

            Console.WriteLine("Random numbers 2D array:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{arr2D[i, j]}  ");
                }
                Console.WriteLine();
            }

            // Creating subarrays (second dimension) of the required length in arr2DEven
            for (int i = 0; i < arr2D.GetLength(0); i++)
            {
                int evenCount = 0;
                for (int j = 0; j < arr2D.GetLength(1); j++)
                {
                    if (arr2D[i, j] % 2 == 0)
                    {
                        evenCount++;
                    }
                }
                arr2DEven[i] = new int[evenCount];
            }

            // Fill arr2DEven with even nums from the arr2D
            for (int i = 0; i < arr2D.GetLength(0); i++)
            {
                int evenIndex = 0;
                for (int j = 0; j < arr2D.GetLength(1); j++)
                {
                    if (arr2D[i, j] % 2 == 0)
                    {
                        arr2DEven[i][evenIndex++] = arr2D[i, j];
                    }
                }
            }

            Console.WriteLine("Even numbers from random int 2D array:");
            for (int i = 0; i < arr2DEven.GetLength(0); i++)
            {
                for (int j = 0; j < arr2DEven[i].Length; j++)
                {
                    arr2DEven[i][j] = arr2D[i, j];
                    Console.Write($"{arr2DEven[i][j]}\t");
                }
                Console.WriteLine();
            }
        }

        private static int[,] GenRnd2DArray(int rows, int cols)
        {
            Random rnd = new();
            int[,] array2D = new int[rows, cols];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    array2D[i, j] = rnd.Next(1, 100);
                }
            }
            return array2D;
        }
    }
}