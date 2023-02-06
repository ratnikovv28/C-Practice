using System;
using System.Numerics;
using System.Reflection;

namespace Practice1
{
    public struct MethodInfo
    {
        public int overloads;
        public int minParams;
        public int maxParams;

        public MethodInfo(int overloads, int minParams, int maxParams)
        {
            this.overloads = overloads;
            this.minParams = minParams;
            this.maxParams = maxParams;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Ратников Владимир 3530903/00002 8 Вариант

            ShowMenuText();

            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ShowAllTypeInfo(); break;
                    case '2': SelectType(); break;
                    case '3': ChangeConsoleView(); break;
                    case '0': Environment.Exit(0); break;
                    default: break;
                }
            }
        }

        //Меню
        public static void ShowMenuText()
        {
            Console.Clear(); //очистка консоли
            Console.WriteLine("Информация по типам:\n" +
                "1 – Общая информация по типам\n" +
                "2 – Выбрать тип из списка\n" +
                "3 – Параметры консоли\n" +
                "0 - Выход из программы");
        }

        //1 – общая информация по типам
        public static void ShowAllTypeInfo()
        {
            Console.Clear(); //очистка консоли

            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies(); //все подключенные сборки 

            List<Type> types = new(); //список типов, определенных во всех подключенных сборках
            foreach (Assembly asm in refAssemblies) types.AddRange(asm.GetTypes());

            byte nRefTypes = 0; //кол-во ссылочных типов
            byte nValueTypes = 0; //кол-во значимых типов

            //Доп.вывод для варианта 8
            Type maxConsArgs = null; //Тип, у которого конструктор имеет наибольшее число аргументов
            int maxArgs = 0; //текущее максимальное число аргументов у конструктора

            string longestIntName = ""; //самое длинное название интерфейса

            foreach (var t in types) { 
                if (t.IsClass)
                    nRefTypes++;
                else if (t.IsValueType)
                    nValueTypes++;
                
                if(t.GetConstructors().Length > maxArgs) maxConsArgs = t; //Тип, у которого конструктор имеет наибольшее число аргументов

                var interfaces = t.GetInterfaces(); //список интерфейсов типа
                foreach (var i in interfaces) {
                    if(i.Name.Length > longestIntName.Length) longestIntName = i.Name;
                }
            }

            Console.WriteLine("Общая информация по типам\n" +
                $"Подключенные сборки: {refAssemblies.Length}\n" +
                $"Всего типов по всем подключенным сборкам: {types.Count}\n" +
                $"Ссылочные типы (только классы): {nRefTypes}\n" +
                $"Значимые типы: {nValueTypes}\n");

            ConsoleColor currentBackgroundColor = Console.BackgroundColor; //текущий цвет консоли
            ConsoleColor currentForegroundColor = Console.ForegroundColor; //текущий цвет текста консоли
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Информация в соответствии с вариантом №8");
            Console.BackgroundColor = currentBackgroundColor;
            Console.ForegroundColor = currentForegroundColor;

            Console.WriteLine($"Тип, у которого конструктор имеет наибольшее число аргументов: {maxConsArgs.Name}\n" +
                $"Самое длинное название интерфейса: {longestIntName}");

            Console.WriteLine("\nНажмите любую кнопку для выхода в главное меню");
            Console.ReadKey(true);
            ShowMenuText();
        }

        //2 - выбрать тип из списка
        public static void SelectType()
        {
            Console.Clear(); //очистка консоли

            Console.WriteLine("Информация по типам\n" +
                "Выберите тип:\n" +
                "----------------------------------------\n" +
                "\t1 – uint\n" +
                "\t2 – int\n" +
                "\t3 – long\n" +
                "\t4 – float\n" +
                "\t5 – double\n" +
                "\t6 – char\n" +
                "\t7 - string\n" +
                "\t8 – Vector\n" +
                "\t9 – Matrix\n" +
                "\t0 – Выход в главное меню");

            Type t = null;

            bool flag = true;

            while (flag)
            {
                flag = false;
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': t = typeof(uint); break;
                    case '2': t = typeof(int); break;
                    case '3': t = typeof(long); break;
                    case '4': t = typeof(float); break;
                    case '5': t = typeof(double); break;
                    case '6':  t = typeof(char); break;
                    case '7': t = typeof(string); break;
                    case '8': t = typeof(Vector); break;
                    case '9': t = typeof(Matrix3x2); break;
                    case '0': ShowMenuText(); break;
                    default: flag = true; break;
                }
                if(flag == false && t != null) ShowTypeInfo(t);
            }
        }

        //Показать информацию о типе
        public static void ShowTypeInfo(Type t)
        {
            Console.Clear(); //очистка консоли

            string[] fieldNames = new string[t.GetFields().Length];
            for (int i = 0; i < fieldNames.Length; i++)
            {
                fieldNames[i] = t.GetFields()[i].Name;
            }
            string sFieldNames = String.Join(", ", fieldNames);

            string[] propertyNames = new string[t.GetProperties().Length];
            for (int i = 0; i < propertyNames.Length; i++)
            {
                propertyNames[i] = t.GetProperties()[i].Name;
            }
            string sPropertyNames = String.Join(", ", propertyNames);

            Console.WriteLine($"Информация по типу: {t.FullName}\n" +
                $"\tЗначимый тип: {(t.IsValueType ? "+" : "-")}\n" +
                $"\tПространство имен: {t.Namespace}\n" +
                $"\tСборка: {t.Assembly.GetName().Name}\n" +
                $"\tОбщее число элементов: {t.GetMembers().Length}\n" +
                $"\tЧисло методов: {t.GetMethods().Length}\n" +
                $"\tЧисло свойств: {t.GetProperties().Length}\n" +
                $"\tЧисло полей: {t.GetFields().Length}\n" +
                $"\tСписок полей: {sFieldNames}\n" +
                $"\tСписок свойств: {sPropertyNames}\n\n" +
                "Нажмите ‘M’ для вывода дополнительной информации по методам:\n" +
                "Нажмите ‘0’ для выхода в главное меню\n");

            bool flag = true;

            while (flag)
            {
                flag = false;
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case 'm': ShowAdditionalTypeInfo(t); break;
                    case '0': ShowMenuText(); break;
                    default: flag = true; break;
                }
            }
        }

        //Дополнительная информация о типе
        public static void ShowAdditionalTypeInfo(Type t)
        {
            var methodsInfo = new Dictionary<string, MethodInfo>();
            foreach (var m in t.GetMethods())
            {
                if (methodsInfo.ContainsKey(m.Name))  // в словаре уже есть такое имя, обновляем статистику
                {
                    MethodInfo mi = methodsInfo[m.Name];
                    mi.overloads++;
                    if (m.GetParameters().Length > mi.maxParams) mi.maxParams = m.GetParameters().Length;
                    else if (m.GetParameters().Length < mi.minParams) mi.minParams = m.GetParameters().Length;
                    methodsInfo[m.Name] = mi;
                }
                else // в словаре нет такого имени, добавляем элемент
                    methodsInfo.Add(m.Name, new MethodInfo(1, m.GetParameters().Length, m.GetParameters().Length));
            }

            Console.WriteLine($"Методы типа {t.FullName}\n" +
                "Название".PadRight(25, ' ') + "Число перегрузок".PadRight(25, ' ') + "Число параметров");
            foreach (var methodInfo in methodsInfo)
            {
                MethodInfo mi = methodInfo.Value;
                Console.WriteLine(methodInfo.Key.ToString().PadRight(25, ' ') + mi.overloads.ToString().PadRight(25, ' ') +
                    (mi.minParams == mi.maxParams ? mi.maxParams : (mi.minParams + ".." + mi.maxParams)));
            }

            Console.WriteLine("\nНажмите любую кнопку для выхода в главное меню");
            Console.ReadKey(true);
            ShowMenuText();
        }

        //3 - параметры консоли
        public static void ChangeConsoleView()
        {
            Console.Clear(); //очистка консоли

            Console.WriteLine("Изменение параметров консоли\n" +
                "\t1 – Цвет фона\n" +
                "\t2 – Цвет текста\n" +
                "\t0 - Выход в главное меню");

            bool flag = true;

            while (flag)
            {
                flag = false;
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': Console.BackgroundColor = ChooseColor(); break;
                    case '2': Console.ForegroundColor = ChooseColor(); break;
                    case '0': ShowMenuText(); break;
                    default: flag = true; break;
                }
            }

            if(flag == false) ShowMenuText();
        }

        //Выбор цвета
        public static ConsoleColor ChooseColor()
        {
            Console.Clear(); //очистка консоли

            Console.WriteLine("Информация по цветам\n" +
                "Выберите цвет:\n" +
                "----------------------------------------\n" +
                "\t1 – Красный\n" +
                "\t2 – Зеленый\n" +
                "\t3 – Черный\n" +
                "\t4 – Синий\n" +
                "\t5 – Белый\n" +
                "\t6 – Серый");

            ConsoleColor consoleColor = ConsoleColor.Black;

            bool flag = true;

            while (flag)
            {
                flag = false;
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': consoleColor = ConsoleColor.Red; break;
                    case '2': consoleColor = ConsoleColor.Green; break;
                    case '3': consoleColor = ConsoleColor.Black; break;
                    case '4': consoleColor = ConsoleColor.Blue; break;
                    case '5': consoleColor = ConsoleColor.White; break;
                    case '6': consoleColor = ConsoleColor.Gray; break;
                    default: flag = true; break;
                }
            }

            return consoleColor;
        }
    }
}