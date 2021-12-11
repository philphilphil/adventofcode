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
                var output = array[i, j].ToString();
                //if (output == "0") output = ".";
                Console.Write("{0} ", output);
            }
            Console.WriteLine();
        }
    }

    public static int[,] Build2DArray(List<string> stringInput)
    {
        int[,] map = new int[stringInput.Count, stringInput[0].Length];

        for (int i = 0; i < stringInput.Count; i++)
        {
            var line = stringInput[i].ToCharArray();
            for (int a = 0; a < line.Length; a++)
            {
                map[i, a] = line[a] - '0';
            }
        }
        return map;
    }
}