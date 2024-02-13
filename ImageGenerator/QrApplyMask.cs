namespace QRGenerator.ImageGenerator;

internal static class QrApplyMask {
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