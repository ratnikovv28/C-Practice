using Practice_2;

namespace Practice2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Для ручного теста
            double[,] dataMult3 = new double[2, 2] {
            { 34, 34 },
            { 71.875, 1 } };
            var mat1 = new Matrix(dataMult3);

            double[,] dataMult4 = new double[2, 3] {
            { 3, 3, 3 },
            { 3, 3, 3 }};
            var mat2 = new Matrix(dataMult4);

            var mat3 = mat1 * mat2;

            string ss = "sa";
        }
    }
}