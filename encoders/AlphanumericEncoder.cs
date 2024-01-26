namespace QRGenerator.encoders
{
    public static class AlphanumericEncoder
    {
        
    public static Dictionary<char, int> AlphanumericTable = new()
    {
        {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4},
        {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
        {'A', 10}, {'B', 11}, {'C', 12}, {'D', 13}, {'E', 14},
        {'F', 15}, {'G', 16}, {'H', 17}, {'I', 18}, {'J', 19},
        {'K', 20}, {'L', 21}, {'M', 22}, {'N', 23}, {'O', 24},
        {'P', 25}, {'Q', 26}, {'R', 27}, {'S', 28}, {'T', 29},
        {'U', 30}, {'V', 31}, {'W', 32}, {'X', 33}, {'Y', 34},
        {'Z', 35}, {' ', 36}, {'$', 37}, {'%', 38}, {'*', 39},
        {'+', 40}, {'-', 41}, {'.', 42}, {'/', 43}, {':', 44}
    };

        public static string[] AlphanumericEncode(string text)
        {
            // Step 1: Break String Up Into Groups of Two
            string[] groups = new string[text.Length / 2 + 1];
            int groupIndex = 0;
            for (int i = 0; i < text.Length; i += 2)
            {
                if (i + 2 <= text.Length)
                {
                    groups[groupIndex] = text.Substring(i, 2);
                }
                else
                {
                    groups[groupIndex] = text.Substring(i);
                }
                groupIndex++;
            }

            // Step 2: Convert Each Group Into Binary
            string[] binaryGroups = new string[groups.Length];
            for (int i = 0; i < groups.Length; i++)
            {
                if (groups[i].Length == 2)
                {
                    // For each pair of characters, get the number representation (from the alphanumeric table) of the first character and multiply it by 45. Then add that number to the number representation of the second character. 
                    // convert to 11 binary bits (padded with 0s)
                    binaryGroups[i] = Convert.ToString(AlphanumericTable[groups[i][0]] * 45 + AlphanumericTable[groups[i][1]], 2).PadLeft(11, '0');
                }
                else if (groups[i].Length == 1)
                {
                    // convert to 6 binary bits (padded with 0s)
                    binaryGroups[i] = Convert.ToString(AlphanumericTable[groups[i][0]], 2).PadLeft(6, '0');
                }
            }

            return binaryGroups;
        }
    }
}