using Practice_2;

namespace Practice2
{
    class Program
    {
        static void Main(string[] args)
        {
            string matrixStr1 = "1 1 1 1 1, 1 1 1 1 1, 1 1 1 1 1";

            string[] separators = new string[] { ", ", " " };
            string[] rows = matrixStr1.Split(separators[0]);
            double[,] matrixValues = new double[rows.Length, rows[0].Split(separators[1]).Length];
            Matrix m;
            int prevLength = rows[0].Split(separators[1]).Length;

            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] rowsElements = rows[i].Split(separators[1]);
                    if (i > 0 && prevLength != rowsElements.Length)
                    {
                        throw new FormatException("Неверный формат данных");
                    }
                    for (int j = 0; j < rowsElements.Length; j++)
                    {
                        double.TryParse(rowsElements[j], out matrixValues[i, j]);
                    }
                }
                m = new Matrix(matrixValues);
            }
            catch (Exception)
            {
                throw new FormatException("Неверный формат данных");
            }

            string das = "das";
        }
    }
}