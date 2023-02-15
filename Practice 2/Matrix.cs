using System.Text;

namespace Practice_2
{
    public class Matrix
    {
        //Поля
        private int _rows;
        private int _cols;
        private double[][] _data;
        private bool _isEmpty;
        private bool _isUnity;
        private bool _isDiagonal;

        //Конструкторы
        public Matrix(int nRows, int nCols)
        {
            if (nRows < 1 || nCols < 1) throw new Exception("Неверный размер матрицы: количество строк и столбцов должно быть больше или равно 1");

            Rows = nRows;
            Cols = nCols;
            Data = new double[Rows][];
            IsDiagonal = IsSquared ? true : false;
            IsUnity = IsSquared ? true : false;
            IsEmpty = true;

            //Иницилизация матрицы
            for (int i = 0; i < Rows; i++)
            {
                Data[i] = new double[Cols];
            }

            Random rnd = new Random();
            //Генерация случайной матрицы со значениями от 0 до 9
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    //Data[i][j] = rnd.NextDouble() * (9 - 0) + 0; 
                    Data[i][j] = 1;
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Data[i][j] != 0) IsEmpty = false; //Проверка на нулевую матрицу

                    if (IsSquared == true && i == j && Data[i][j] != 1) IsUnity = false; //Проверка на единичную матрицу
                    else if (IsSquared == true && i != j && Data[i][j] != 0) { IsUnity = false; IsDiagonal = false; } //Проверка на диагональную и единичную матрицу
                }
            }
        }
        public Matrix(double[,] initData)
        {
            Rows = initData.GetLength(0);
            Cols = initData.GetLength(1);
            Data = new double[Rows][];
            IsDiagonal = IsSquared ? true : false;
            IsUnity = IsSquared ? true : false;
            IsEmpty = true;

            //Иницилизация матрицы
            for (int i = 0; i < Rows; i++)
            {
                Data[i] = new double[Cols];
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (initData[i,j] != 0) 
                        IsEmpty = false; //Проверка на нулевую матрицу

                    if (IsSquared == true && i == j && initData[i, j] != 1) IsUnity = false; //Проверка на единичную матрицу
                    else if (IsSquared == true && i != j && initData[i, j] != 0) { IsUnity = false; IsDiagonal = false; } //Проверка на диагональную и единичную матрицу

                    Data[i][j] = initData[i, j];
                }
            }
        }


        public double[][] Data { get => _data; private set => _data = value; } //Матрица
        public double this[int i, int j] { get => Data[i][j]; set => Data[i][j] = value; } //Размеры матрицы только для чтения через свойства
        public int Rows { get => _rows; private set => _rows = value;  } //Количество строк
        public int Cols { get => _cols; private set => _cols = value;  } //Количество столбцов
        public int? Size { get => IsSquared ? Rows * Cols : null; }  //Размер квадратной матрицы


        //Булевые свойства
        public bool IsSquared { get => Rows == Cols ? true : false; } //Является ли матрица квадратной
        public bool IsEmpty { get => _isEmpty;  private set => _isEmpty = value; } //Является ли матрица нулевой
        public bool IsUnity { get => _isUnity; private set => _isUnity = value; } //Является ли матрица единичной
        public bool IsDiagonal { get => _isDiagonal; private set => _isDiagonal = value; } //Является ли матрица диагональной(Вариант 3)


        //Функции
        public static Matrix operator *(Matrix m1, double d) => MultN(m1, d); //Переопределение умножение матрицы на число
        private static Matrix MultN(Matrix m1, double d)
        {
            Matrix arr = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    arr[i, j] = m1[i, j] * d;
                }
            }

            return arr;
        }

        //Дополнительная задача - перемножение матриц
        public static Matrix operator *(Matrix m1, Matrix m2) => MultTwoMatrix(m1, m2); //Переопределение умножение матрицы на число
        private static Matrix MultTwoMatrix(Matrix m1, Matrix m2)
        {
            try
            {
                //Операция умножения двух матриц выполнима только в том случае,
                //если число столбцов в первом сомножителе равно числу строк во втором
                if (m1.Cols != m2.Rows) throw new Exception("Ошибка");
                int newRows = m1.Rows;
                int newCols = m2.Cols;

                double[,] newArr = new double[newRows, newCols];
                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m2.Cols; j++)
                    {
                        for (int k = 0; k < m2.Rows; k++)
                        {
                            newArr[i, j] += m1.Data[i][k] * m2.Data[k][j];
                        }
                    }
                }

                var newMatrix = new Matrix(newArr);

                return newMatrix;
            }
            catch (Exception)
            {
                throw new Exception("Число столбцов матрицы1 не равно числу строк матрицы2");
            }
        }

        public static explicit operator Matrix(double[,] arr) => new Matrix(arr); //Оператор преобразования типов 
        public Matrix Transpose() //Транспонирование матрицы
        {
            var transposed = new Matrix(Cols, Rows);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    transposed[j, i] = Data[i][j];
                }
            }

            return transposed;
        }
        public override string ToString() //Преобразование матрицы в строку
        {
            var str = new StringBuilder("");

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    string nmb = Data[i][j].ToString();

                    if (j != Cols - 1) str.Append($"{nmb} ");
                    else str.Append($"{nmb}");
                }
                if(i != Rows - 1)str.Append(", ");
            }

            return str.ToString();
        }
        public static Matrix GetUnity(int size) //Порождение единичной матрицы
        {
            var unityMatrix = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    unityMatrix[i, j] = i == j ? 1 : 0;
                }
            }

            return unityMatrix;
        }
        public static Matrix GetEmpty(int size) //Порождение нулевой матрицы
        {
            var emptyMatrix = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    emptyMatrix[i, j] = 0;
                }
            }

            return emptyMatrix;
        }
        public static bool TryParse(string s, out Matrix m) //Создание матрицы по строчке
        {
            string[] separators = new string[] { ", ", " " };
            string[] rows = s.Split(separators[0]);
            double[,] matrixValues = new double[rows.Length, rows[0].Split(separators[1]).Length];
            int prevLength = rows[0].Split(separators[1]).Length;

            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] rowsElements = rows[i].Split(separators[1]);
                    if (i > 0 && prevLength != rowsElements.Length)
                    {
                        throw new FormatException("Неверный формат данных");
                        return false;
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
                return false;
            }

            return true;
        } 
    }
}
