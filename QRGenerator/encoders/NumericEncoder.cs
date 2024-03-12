namespace QRGenerator.encoders;

public static class NumericEncoder
{
    public static Dictionary<char, int> NumericTable = new()
    {
        {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
        {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9}
    };

    /// <summary>
    /// Encode the numeric text into binary numbers
    /// </summary>
    /// <param name="text"></param>
    /// <returns> A string of binary numbers</returns>
    public static string NumericEncode(string text)
    {
        // Step 1: Break String Up Into Groups of Three
        string[] groups = new string[(text.Length + 2) / 3];
        int groupIndex = 0;
        int i = 0;
        while (i < text.Length)
        {
            if (i + 3 <= text.Length)
            {
                groups[groupIndex] = text.Substring(i, 3);
            }
            else
            {
                groups[groupIndex] = text.Substring(i);
            }
            groupIndex++;
            i += 3;
        }


        string[] binaryGroups = new string[groups.Length];
        for (i = 0; i < groups.Length; i++)
        {
            if (groups[i].Length == 3)
            {
                // convert to 10 binary bits (padded with 0s)
                binaryGroups[i] = Convert.ToString(Convert.ToInt32(groups[i]), 2).PadLeft(10, '0');
            }
            else if (groups[i].Length == 2)
            {
                // convert to 7 binary bits (padded with 0s)
                binaryGroups[i] = Convert.ToString(Convert.ToInt32(groups[i]), 2).PadLeft(7, '0');
            }
            else if (groups[i].Length == 1)
            {
                // convert to 4 binary bits (padded with 0s)
                binaryGroups[i] = Convert.ToString(Convert.ToInt32(groups[i]), 2).PadLeft(4, '0');
            }

        }

        return string.Join("", binaryGroups);
    }
}
