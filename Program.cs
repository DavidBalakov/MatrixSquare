namespace MatrixTask
{
    public class Matrix
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Напишете положително число: ");
            string input = Console.ReadLine();
            int number;
            while (!int.TryParse(input, out number) || number <= 0 || number > 100)
            {
                Console.WriteLine("Не сте въвели правилно положително число!");
                input = Console.ReadLine();
            }

            int[,] matrix = GenerateMatrix(number);
            PrintMatrix(matrix);
        }

        private static void Change(ref int dX, ref int dY)
        {
            int[] directionsX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] directionsY = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int cd = 0;
            for (int count = 0; count < 8; count++)
            {
                if (directionsX[count] == dX && directionsY[count] == dY)
                {
                    cd = count;
                    break;
                }
            }

            if (cd == 7)
            {
                dX = directionsX[0];
                dY = directionsY[0];
                return;
            }

            dX = directionsX[cd + 1];
            dY = directionsY[cd + 1];
        }

        public static bool CheckCell(int[,] arr, int x, int y)
        {
            int[] dirX = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dirY = { 1, 0, -1, -1, -1, 0, 1, 1 };
            for (int i = 0; i < 8; i++)
            {
                if (x + dirX[i] >= arr.GetLength(0) || x + dirX[i] < 0)
                {
                    dirX[i] = 0;
                }

                if (y + dirY[i] >= arr.GetLength(0) || y + dirY[i] < 0)
                {
                    dirY[i] = 0;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (arr[x + dirX[i], y + dirY[i]] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static void FindCell(int[,] arr, out int x, out int y)
        {
            x = 0;
            y = 0;
            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(0); col++)
                {
                    if (arr[row, col] == 0)
                    {
                        x = row;
                        y = col;
                        return;
                    }
                }
            }
        }

        public static int[,] GenerateMatrix(int number)
        {
            int[,] matrix = new int[number, number];
            int value = 1;
            int row = 0;
            int col = 0;
            int directionX = 1;
            int directionY = 1;

            while (true)
            {
                matrix[row, col] = value;

                if (!CheckCell(matrix, row, col))
                {
                    value++;
                    break;
                }

                bool outOfBoundaries =
                    row + directionX >= number ||  // извън от дясната страна
                    row + directionX < 0 ||        // извън от лявата страна
                    col + directionY >= number ||  // извън от горната страна
                    col + directionY < 0 ||        // извън от долната страна
                    matrix[row + directionX, col + directionY] != 0;

                while (row + directionX >= number || row + directionX < 0 || col + directionY >= number
                    || col + directionY < 0 || matrix[row + directionX, col + directionY] != 0)
                {
                    Change(ref directionX, ref directionY);
                }

                row += directionX;
                col += directionY;
                value++;
            }

            FindCell(matrix, out row, out col);
            if (row != 0 && col != 0)
            {
                directionX = 1;
                directionY = 1;

                while (true)
                {
                    matrix[row, col] = value;
                    if (!CheckCell(matrix, row, col))
                    {
                        break;
                    }

                    if (row + directionX >= number || row + directionX < 0 || col + directionY >= number 
                        || col + directionY < 0 || matrix[row + directionX, col + directionY] != 0)
                    {
                        while (row + directionX >= number || row + directionX < 0 || col + directionY >= number 
                            || col + directionY < 0 || matrix[row + directionX, col + directionY] != 0)
                        {
                            Change(ref directionX, ref directionY);
                        }
                    }

                    row += directionX;
                    col += directionY;
                    value++;
                }
            }

            return matrix;
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int p = 0; p < matrix.GetLength(0); p++)
            {
                for (int q = 0; q < matrix.GetLength(0); q++)
                {
                    Console.Write("{0,3}", matrix[p, q]);
                }

                Console.WriteLine();
            }
        }
    }
}