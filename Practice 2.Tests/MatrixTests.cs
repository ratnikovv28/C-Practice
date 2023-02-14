namespace Practice_2.Tests
{
    public class Tests
    {
        //Первый конструктор
        private Matrix certainMatrix; //Определенная матрица
        private Matrix squareMatrix; //Квадратная матрица
        private Matrix wrongMatrix; //Неправильная матрица

        //Второй конструктор
        private Matrix certainMatrixByRectangleArray; //Определенная матрица
        private Matrix squareMatrixByRectangleArray; //Квадратная матрица
        private Matrix unityMatrixByRectangleArray; //Единичная матрица
        private Matrix diagonalMatrixByRectangleArray; //Диагональная матрица
        private Matrix emptyMatrixByRectangleArray; //Нулевая матрица

        [SetUp]
        public void Setup()
        {
            certainMatrix = new Matrix(3, 5);
            squareMatrix = new Matrix(8, 8);

            double[,] certainRectangleArray = { { 1.1, 2, 3.4 },
                                                { 4, 5.75, 6 } };
            certainMatrixByRectangleArray = new Matrix(certainRectangleArray);

            double[,] squareRectangleArray = { { 1, 2, 3.2 },
                                               { 4.5, 5, 6 },
                                               { 7, 8.98, 9 } };
            squareMatrixByRectangleArray = new Matrix(squareRectangleArray);

            double[,] unityRectangleArray = { { 1, 0, 0 }, 
                                              { 0, 1, 0 }, 
                                              { 0, 0, 1 } };
            unityMatrixByRectangleArray = new Matrix(unityRectangleArray);

            double[,] diagonalRectangleArray = { { 1.312, 0, 0 },
                                                 { 0, 5.312, 0 },
                                                 { 0, 0, 9.787 } };
            diagonalMatrixByRectangleArray = new Matrix(diagonalRectangleArray);

            double[,] emptyRectangleArray = { { 0, 0, 0 },
                                              { 0, 0, 0 },
                                              { 0, 0, 0 } };
            emptyMatrixByRectangleArray = new Matrix(emptyRectangleArray);
        }

        [Test]
        public void Create_Wrong_Matrix()
        {
            Assert.Throws<Exception>(() => wrongMatrix = new Matrix(0, 5));
            Assert.Throws<Exception>(() => wrongMatrix = new Matrix(3, 0));
        }

        [Test]
        public void Test_Matrix_Properties()
        {
            //Тест размеров матрицы
            Assert.AreEqual(3, certainMatrix.Rows);
            Assert.AreEqual(5, certainMatrix.Cols);
            Assert.AreNotEqual(7, squareMatrix.Rows);
            Assert.AreNotEqual(4, squareMatrix.Cols);

            Assert.AreEqual(2, certainMatrixByRectangleArray.Rows);
            Assert.AreEqual(3, certainMatrixByRectangleArray.Cols);
            Assert.AreNotEqual(4, diagonalMatrixByRectangleArray.Rows);
            Assert.AreNotEqual(2, diagonalMatrixByRectangleArray.Cols);
        }

        [Test]
        public void Test_Size_Of_Matrix()
        {
            //Тест размера матрицы(только квадратной)
            Assert.IsNull(certainMatrix.Size);
            Assert.AreEqual(8 * 8, squareMatrix.Size);

            Assert.IsNull(certainMatrixByRectangleArray.Size);
            Assert.AreEqual(3 * 3, squareMatrixByRectangleArray.Size);
        }

        [Test]
        public void Test_Is_Squared_Property()
        {
            //Тест является ли квадратной
            Assert.IsFalse(certainMatrix.IsSquared);
            Assert.IsTrue(squareMatrix.IsSquared);

            Assert.IsFalse(certainMatrixByRectangleArray.IsSquared);
            Assert.IsTrue(squareMatrixByRectangleArray.IsSquared);
        }

        [Test]
        public void Test_Is_Empty_Property()
        {
            //Тест является ли нулевой
            Assert.IsFalse(certainMatrix.IsEmpty);

            Assert.IsFalse(unityMatrixByRectangleArray.IsEmpty);
            Assert.IsTrue(emptyMatrixByRectangleArray.IsEmpty);
        }

        [Test]
        public void Test_Is_Unity_Property()
        {
            //Тест является ли единичной
            Assert.IsFalse(certainMatrix.IsUnity);

            Assert.IsFalse(emptyMatrixByRectangleArray.IsUnity);
            Assert.IsTrue(unityMatrixByRectangleArray.IsUnity);
        }

        [Test]
        public void Test_Is_Diagonal_Property()
        {
            //Тест является ли диагональной
            Assert.IsFalse(certainMatrix.IsDiagonal);

            Assert.IsFalse(squareMatrixByRectangleArray.IsDiagonal);
            Assert.IsTrue(diagonalMatrixByRectangleArray.IsDiagonal);
        }

        [Test]
        public void Test_Get_Matrix_Data()
        {
            double[][] data = new double[3][];
            data[0] = new double[] { 1, 1, 1, 1, 1 };
            data[1] = new double[] { 1, 1, 1, 1, 1 };
            data[2] = new double[] { 1, 1, 1, 1, 1 };

            double[][] notData = new double[3][];
            notData[0] = new double[] { 1, 1, 1, 1, 1 };
            notData[1] = new double[] { 1, 2, 1, 1, 1 };
            notData[2] = new double[] { 1, 1, 1, 2, 1 };

            //Тест получения всех элементов матрицы
            Assert.AreEqual(data, certainMatrix.Data);
            Assert.AreNotEqual(notData, certainMatrix.Data);
        }

        [Test]
        public void Test_Get_Matrix_Element()
        {
            double[][] data = new double[3][];
            data[0] = new double[] { 1, 1, 1, 1, 1 };
            data[1] = new double[] { 1, 2, 1, 1, 1 };
            data[2] = new double[] { 1, 1, 1, 1, 1 };

            //Тест получения элемента матрицы
            Assert.AreEqual(data[1][4], certainMatrix.Data[1][4]);
            Assert.AreNotEqual(data[1][1], certainMatrix.Data[1][1]);
        }

        //Функция - 3 вариант
        [Test]
        public void Test_Multiplication_Matrix_On_Number()
        {
            double[][] dataMult1 = new double[3][];
            dataMult1[0] = new double[] { 3, 3, 3, 3, 3 };
            dataMult1[1] = new double[] { 3, 3, 3, 3, 3 };
            dataMult1[2] = new double[] { 3, 3, 3, 3, 3 };

            double[][] dataMult2 = new double[3][];
            dataMult2[0] = new double[] { 3, 3, 3, 2, 3 };
            dataMult2[1] = new double[] { 3, 3, 3, 3, 3 };
            dataMult2[2] = new double[] { 2, 3, 3, 3, 3 };

            //Тест матричной операции умножнения на число в соотвествии с вариантом 3
            Assert.AreEqual(dataMult1, (certainMatrix * 3).Data);
            Assert.AreNotEqual(dataMult2, (certainMatrix * 3).Data);
        }

        [Test]
        public void Test_Type_Conversion_Operator()
        {
            double[,] data1 = new double[3, 5];

            //Тест оператора преобразования типов
            Assert.IsTrue(((Matrix)data1).GetType() == certainMatrix.GetType());
            Assert.IsFalse(data1.GetType() == certainMatrix.GetType());
        }

        [Test]
        public void Test_Transpose_Matrix()
        {
            double[][] transposeMatrix = new double[5][];
            transposeMatrix[0] = new double[] { 1, 1, 1 };
            transposeMatrix[1] = new double[] { 1, 1, 1 };
            transposeMatrix[2] = new double[] { 1, 1, 1 };
            transposeMatrix[3] = new double[] { 1, 1, 1 };
            transposeMatrix[4] = new double[] { 1, 1, 1 };

            double[][] notTransposeMatrix = new double[4][];
            transposeMatrix[0] = new double[] { 1, 1, 1 };
            transposeMatrix[1] = new double[] { 1, 1, 1 };
            transposeMatrix[2] = new double[] { 1, 1, 1 };
            transposeMatrix[3] = new double[] { 1, 1, 1 };

            //Тест транспонирование матрицы
            Assert.AreEqual(transposeMatrix, certainMatrix.Transpose().Data);
            Assert.AreNotEqual(notTransposeMatrix, certainMatrix.Transpose().Data);
        }

        [Test]
        public void Test_Redefined_ToString()
        {
            string matrixStr1 = "1 1 1 1 1, 1 1 1 1 1, 1 1 1 1 1";
            string matrixStr2 = "1 1 1 1 1; 1 1 1/ 1 1, 1 1 1 1 1";

            //Тест переопределенного метода ToString
            Assert.AreEqual(matrixStr1, certainMatrix.ToString());
            Assert.AreNotEqual(matrixStr2, certainMatrix.ToString());
        }

        [Test]
        public void Test_Create_Unity_Matrix()
        {
            double[,] unityMatrix = new double[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            double[,] notUnityMatrix = new double[3, 3] { { 1, 1, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

            //Тест порождения единичной матрицы
            Assert.AreEqual(((Matrix)unityMatrix).Data, Matrix.GetUnity(3).Data);
            Assert.AreNotEqual(((Matrix)notUnityMatrix).Data, Matrix.GetUnity(3).Data);
        }

        [Test]
        public void Test_Create_Empty_Matrix()
        {
            double[,] emptyMatrix = new double[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            double[,] notEmptyMatrix = new double[3, 3] { { 0, 1, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            //Тест порождения единичной матрицы
            Assert.AreEqual(((Matrix)emptyMatrix).Data, Matrix.GetEmpty(3).Data);
            Assert.AreNotEqual(((Matrix)notEmptyMatrix).Data, Matrix.GetEmpty(3).Data);
        }

        [Test]
        public void Test_Create_Matrix_By_String()
        {
            string matrixStr1 = "1 1 1 1 1, 1 1 1 1 1, 1 1 1 1 1";
            string matrixStr2 = "1 1 1 1 1; 1 1 1/ 1 1, 1 1 1 1 1";
            Matrix matrixByStr1;

            //Тест создания матрицы по строчке(3 вариант)
            Assert.IsTrue(Matrix.TryParse(matrixStr1, out matrixByStr1));
            Assert.Throws<Exception>(() => Matrix.TryParse(matrixStr2, out matrixByStr1));
        }
    }
}