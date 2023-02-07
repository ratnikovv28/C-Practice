using System.Numerics;
using System.Reflection;

namespace Practice1
{
    public struct MethodInfo
    {
        private int _overloads;
        private int _minParams;
        private int _maxParams;

        public MethodInfo(int overloads, int minParams, int maxParams)
        {
            _overloads = overloads;
            _minParams = minParams;
            _maxParams = maxParams;
        }
        public int Overloads
        {
            get { return _overloads; }
            set { _overloads = value; }
        }
        public int MinParams
        {
            get { return _minParams; }
            set { _minParams = value; }
        }
        public int MaxParams
        {
            get { return _maxParams; }
            set { _maxParams = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Ратников Владимир 3530903/00002 8 Вариант

            while (true)
            {
                Console.Clear(); //очистка консоли
                Console.WriteLine("Информация по типам:\n" +
                    "1 – Общая информация по типам\n" +
                    "2 – Выбрать тип из списка\n" +
                    "3 – Параметры консоли\n" +
                    "0 - Выход из программы");
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ShowAllTypeInfo(); break;
                    case '2': SelectType(); break;
                    case '3': ChangeConsoleView(); break;
                    case '0': return; 
                    default: break;
                }
            }
        }

        //1 – общая информация по типам
        public static void ShowAllTypeInfo()
        {
            Console.Clear(); //очистка консоли

            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies(); //все подключенные сборки 

            List<Type> types = new(); //список типов, определенных во всех подключенных сборках
            foreach (Assembly asm in refAssemblies) types.AddRange(asm.GetTypes());

            int nRefTypes = 0; //кол-во ссылочных типов
            int nValueTypes = 0; //кол-во значимых типов



            //Доп.вывод для варианта 8
            Type maxConsArgs = null; //Тип, у которого конструктор имеет наибольшее число аргументов
            int maxArgs = 0; //текущее максимальное число аргументов у конструктора

            string longestIntName = ""; //самое длинное название интерфейса

            foreach (var t in types) { 
                if (t.IsClass)
                    nRefTypes++;
                else if (t.IsValueType)
                    nValueTypes++;

                ConstructorInfo[] typeConstructors = t.GetConstructors(); //все конструкторы типа

                foreach (var constructor in typeConstructors)
                {
                    if (constructor.GetParameters().Length > maxArgs) //Тип, у которого конструктор имеет наибольшее число аргументов
                    {
                        maxArgs = constructor.GetParameters().Length;
                        maxConsArgs = t;
                    }
                }

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

            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ShowTypeInfo(typeof(uint)); return;
                    case '2': ShowTypeInfo(typeof(int)); return;
                    case '3': ShowTypeInfo(typeof(long)); return;
                    case '4': ShowTypeInfo(typeof(float)); return;
                    case '5': ShowTypeInfo(typeof(double)); return;
                    case '6': ShowTypeInfo(typeof(char)); return;
                    case '7': ShowTypeInfo(typeof(string)); return;
                    case '8': ShowTypeInfo(typeof(Vector)); return;
                    case '9': ShowTypeInfo(typeof(Array)); return;
                    case '0': return; 
                    default: break;
                }
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

            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case 'm': ShowAdditionalTypeInfo(t); break;
                    case '0': return;
                    default: break;
                }
            }
        }

        //Дополнительная информация о типе
        public static void ShowAdditionalTypeInfo(Type t)
        {
            var methodsInfo = new Dictionary<string, MethodInfo>();
            
            foreach(var m in t.GetMethods())
            {
                if (methodsInfo.ContainsKey(m.Name))  // в словаре уже есть такое имя, обновляем статистику
                {
                    MethodInfo mi = methodsInfo[m.Name];
                    mi.Overloads++;
                    if (m.GetParameters().Length > mi.MaxParams) mi.MaxParams = m.GetParameters().Length;
                    else if (m.GetParameters().Length < mi.MinParams) mi.MinParams = m.GetParameters().Length;
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
                Console.WriteLine("{0, -25}{1, -25}{2}", methodInfo.Key, mi.Overloads, 
                    (mi.MinParams == mi.MaxParams ? mi.MaxParams : (mi.MinParams + ".." + mi.MaxParams)));
            }

            Console.WriteLine("\nНажмите ‘0’ для выхода в главное меню");
        }

        //3 - параметры консоли
        public static void ChangeConsoleView()
        {
            Console.Clear(); //очистка консоли

            Console.WriteLine("Изменение параметров консоли\n" +
                "\t1 – Цвет фона\n" +
                "\t2 – Цвет текста\n" +
                "\t0 - Выход в главное меню");

            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ChooseColor("background"); return;
                    case '2': ChooseColor("foreground"); return;
                    case '0': return;
                    default: break;
                }
            }
        }

        //Выбор цвета
        public static void ChooseColor(string type)
        {
            Console.Clear(); //очистка консоли

            Console.WriteLine("Информация по цветам\n" +
                "Выберите цвет:\n" +
                "----------------------------------------");

            string[] colors = typeof(ConsoleColor).GetEnumNames(); //Получаем название всех цветов
            for (int i = 0; i < colors.Length; i++)
            {
                Console.WriteLine($"\t{(char)(i + 'a')} - {colors[i]}"); //сначала 'a' в int, затем int к char
            }

            Console.WriteLine("\t0 - Выход в главное меню");

            while (true)
            {
                char option = char.ToLower(Console.ReadKey(true).KeyChar);
                
                // 0 - символ возврата
                if (option == '0') return;

                // введеный символ должен быть между 'a' и 'a' + color.Length
                int colorIndex = option - 'a';
                if (colorIndex >= 0 && colorIndex < colors.Length)
                {
                    switch (type)
                    {
                        case "background": Console.BackgroundColor = (ConsoleColor)colorIndex; return;
                        case "foreground": Console.ForegroundColor = (ConsoleColor)colorIndex; return;
                    }
                }
            }
        }
    }
}