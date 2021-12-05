public static class Helpers
{
    public static void Print2DArray(int[,] array)
    {
        int rowLength = array.GetLength(0);
        int colLength = array.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", array[i, j]));
            }
            Console.WriteLine();
        }
    }
}