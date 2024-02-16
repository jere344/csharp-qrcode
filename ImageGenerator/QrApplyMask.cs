namespace QRGenerator.ImageGenerator;

internal static class QrApplyMask
{

    public static bool?[,] GetBestMatrice(List<bool?[,]> maskedMatrices)
    {
        int bestMask = 0;
        int bestPenalty = int.MaxValue;
        for (int i = 0; i < maskedMatrices.Count; i++)
        {
            int penalty = GetPenalty(maskedMatrices[i]);
            if (penalty < bestPenalty)
            {
                bestPenalty = penalty;
                bestMask = i;
            }
        }
        Console.WriteLine($"Best mask: {bestMask}");
        return maskedMatrices[bestMask];
    }

    private static int GetPenalty(bool?[,] matrix)
    {
        int penalty = 0;
        penalty += GetPenaltyRule1(matrix);
        penalty += GetPenaltyRule2(matrix);
        penalty += GetPenaltyRule3(matrix);
        penalty += GetPenaltyRule4(matrix);
        return penalty;
    }

    private static int GetPenaltyRule1(bool?[,] matrix)
    {
        int penalty = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1) - 4; j++)
            {
                if (matrix[i, j] == matrix[i, j + 1] && matrix[i, j] == matrix[i, j + 2] && matrix[i, j] == matrix[i, j + 3] && matrix[i, j] == matrix[i, j + 4])
                {
                    penalty += 3;
                    j += 4;
                    while (j < matrix.GetLength(1) - 4 && matrix[i, j] == matrix[i, j + 1])
                    {
                        j++;
                        penalty++;
                    }
                }
            }
        }
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0) - 4; i++)
            {
                if (matrix[i, j] == matrix[i + 1, j] && matrix[i, j] == matrix[i + 2, j] && matrix[i, j] == matrix[i + 3, j] && matrix[i, j] == matrix[i + 4, j])
                {
                    penalty += 3;
                    i += 4;
                    while (i < matrix.GetLength(0) - 4 && matrix[i, j] == matrix[i + 1, j])
                    {
                        i++;
                        penalty++;
                    }
                }
            }
        }
        return penalty;
    }

    private static int GetPenaltyRule2(bool?[,] matrix)
    {
        //          For second evaluation condition, look for areas of the same color that are at least 2x2 modules or larger. The QR code specification says that for a solid-color block of size m × n, the penalty score is 3 × (m - 1) × (n - 1). However, the QR code specification does not specify how to calculate the penalty when there are multiple ways of dividing up the solid-color blocks.

        // Therefore, rather than looking for solid-color blocks larger than 2x2, simply add 3 to the penalty score for every 2x2 block of the same color in the QR code, making sure to count overlapping 2x2 blocks. For example, a 3x2 block of the same color should be counted as two 2x2 blocks, one overlapping the other. 
        int penalty = 0;
        for (int i = 0; i < matrix.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < matrix.GetLength(1) - 1; j++)
            {
                if (matrix[i, j] == matrix[i, j + 1] && matrix[i, j] == matrix[i + 1, j] && matrix[i, j] == matrix[i + 1, j + 1])
                {
                    penalty += 3;
                }
            }
        }
        return penalty;
    }

    private static int GetPenaltyRule3(bool?[,] matrix)
    {

        // The third penalty rule looks for patterns of dark-light-dark-dark-dark-light-dark that have four light modules on either side. In other words, it looks for any of the following two patterns:
        // Each time this pattern is found, add 40 to the penalty score. In the example below, there are two such patterns. Therefore, penalty score #3 is 80. 
        int penalty = 0;
        bool[] pattern1 = new bool[11] { true, false, true, true, true, false, true, false, false, false, false };
        bool[] pattern2 = new bool[11] { false, false, false, false, true, false, true, true, true, false, true };
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1) - 10; j++)
            {

                if (
                    (
                        matrix[i, j] == pattern1[0] &&
                        matrix[i, j + 1] == pattern1[1] &&
                        matrix[i, j + 2] == pattern1[2] &&
                        matrix[i, j + 3] == pattern1[3] &&
                        matrix[i, j + 4] == pattern1[4] &&
                        matrix[i, j + 5] == pattern1[5] &&
                        matrix[i, j + 6] == pattern1[6] &&
                        matrix[i, j + 7] == pattern1[7] &&
                        matrix[i, j + 8] == pattern1[8] &&
                        matrix[i, j + 9] == pattern1[9] &&
                        matrix[i, j + 10] == pattern1[10]
                    ) || (
                        matrix[i, j] == pattern2[0] &&
                        matrix[i, j + 1] == pattern2[1] &&
                        matrix[i, j + 2] == pattern2[2] &&
                        matrix[i, j + 3] == pattern2[3] &&
                        matrix[i, j + 4] == pattern2[4] &&
                        matrix[i, j + 5] == pattern2[5] &&
                        matrix[i, j + 6] == pattern2[6] &&
                        matrix[i, j + 7] == pattern2[7] &&
                        matrix[i, j + 8] == pattern2[8] &&
                        matrix[i, j + 9] == pattern2[9] &&
                        matrix[i, j + 10] == pattern2[10]
                    )
                ) {
                    penalty += 40;
                }

            }
        }
        
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0) - 10; i++)
            {
                if (
                    (
                        matrix[i, j] == pattern1[0] &&
                        matrix[i + 1, j] == pattern1[1] &&
                        matrix[i + 2, j] == pattern1[2] &&
                        matrix[i + 3, j] == pattern1[3] &&
                        matrix[i + 4, j] == pattern1[4] &&
                        matrix[i + 5, j] == pattern1[5] &&
                        matrix[i + 6, j] == pattern1[6] &&
                        matrix[i + 7, j] == pattern1[7] &&
                        matrix[i + 8, j] == pattern1[8] &&
                        matrix[i + 9, j] == pattern1[9] &&
                        matrix[i + 10, j] == pattern1[10]
                    ) || (
                        matrix[i, j] == pattern2[0] &&
                        matrix[i + 1, j] == pattern2[1] &&
                        matrix[i + 2, j] == pattern2[2] &&
                        matrix[i + 3, j] == pattern2[3] &&
                        matrix[i + 4, j] == pattern2[4] &&
                        matrix[i + 5, j] == pattern2[5] &&
                        matrix[i + 6, j] == pattern2[6] &&
                        matrix[i + 7, j] == pattern2[7] &&
                        matrix[i + 8, j] == pattern2[8] &&
                        matrix[i + 9, j] == pattern2[9] &&
                        matrix[i + 10, j] == pattern2[10]
                    )
                ) {
                    penalty += 40;
                }
            }
        }
        return penalty;
    }

    private static int GetPenaltyRule4(bool?[,] matrix)
    {
        //      The final evaluation condition is based on the ratio of light modules to dark modules. To calculate this penalty rule, do the following steps:

        //     Count the total number of modules in the matrix.
        //     Count how many dark modules there are in the matrix.
        //     Calculate the percent of modules in the matrix that are dark: (darkmodules / totalmodules) * 100
        //     Determine the previous and next multiple of five of this percent. For example, for 43 percent, the previous multiple of five is 40, and the next multiple of five is 45.
        //     Subtract 50 from each of these multiples of five and take the absolute value of the result. For example, |40 - 50| = |-10| = 10 and |45 - 50| = |-5| = 5.
        //     Divide each of these by five. For example, 10/5 = 2 and 5/5 = 1.
        //     Finally, take the smallest of the two numbers and multiply it by 10. In this example, the lower number is 1, so the result is 10. This is penalty score #4.

        // For another example, in the image below, the total number of modules is 441, and the total number of dark modules is 213. 
        int darkCount = 0;
        int lightCount = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == true)
                {
                    darkCount++;
                }
                else
                {
                    lightCount++;
                }
            }
        }
        int darkPercentage = (int)(100 * darkCount / (darkCount + lightCount));
        int nextMultipleOfFive = (darkPercentage / 5 + 1) * 5;
        int previousMultipleOfFive = (darkPercentage / 5) * 5;
        int penalty = Math.Min(
            Math.Abs(nextMultipleOfFive - 50) / 5, Math.Abs(previousMultipleOfFive - 50) / 5
        ) * 10;
        return penalty;
    }


    public static bool?[,] ApplyMask(bool?[,] matrix, int mask)
    {
        switch (mask)
        {
            case 0:
                return ApplyMask0(matrix);
            case 1:
                return ApplyMask1(matrix);
            case 2:
                return ApplyMask2(matrix);
            case 3:
                return ApplyMask3(matrix);
            case 4:
                return ApplyMask4(matrix);
            case 5:
                return ApplyMask5(matrix);
            case 6:
                return ApplyMask6(matrix);
            case 7:
                return ApplyMask7(matrix);
            default:
                return matrix;
        }
    }

    private static bool?[,] ApplyMask0(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if ((i + j) % 2 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask1(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (i % 2 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask2(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (j % 3 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask3(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if ((i + j) % 3 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask4(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if ((i / 2 + j / 3) % 2 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask5(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if ((i * j) % 2 + (i * j) % 3 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask6(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (((i * j) % 2 + (i * j) % 3) % 2 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

    private static bool?[,] ApplyMask7(bool?[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (((i + j) % 2 + (i * j) % 3) % 2 == 0)
                {
                    matrix[i, j] = matrix[i, j] == true ? false : true;
                }
            }
        }
        return matrix;
    }

}