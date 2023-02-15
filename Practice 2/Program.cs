using Practice_2;

namespace Practice2
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] dataMult3 = new double[2, 1] {
            { 33.9 },
            { 71.875 } };
            var mat1 = new Matrix(dataMult3);

            double[,] dataMult4 = new double[1, 3] {
            { 3, 3, 3 } };
            var mat2 = new Matrix(dataMult4);

            var mat3 = mat1 * mat2;

            string ss = "sa";
        }
    }
}