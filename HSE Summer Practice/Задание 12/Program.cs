/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача 12 (сортировка простыми вставками и сортировка с помощью двоичного дерева):
 * ------
 * Сравнивает два типа сортировки одномерных массивов разной упордяоченности
 * подсчётом сравнений и количества пересылок
 */
using System;
using System.Collections.Generic;

namespace Задача_12
{
    class TreeElement // Класс дерево
    {
        public readonly int Data; // Инф. поле
        public TreeElement Left; // Адрес левого поддерева
        public TreeElement Right; // Адрес правого поддерева

        public TreeElement(int data = 0, TreeElement left = null, TreeElement right = null) //Конструктор
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }

    class MyArray //Класс массива
    {
        private static int[] arr { get; set; } //массив
        private static TreeElement root; //Корень дерева
        private static readonly List<int> result = new List<int>(); // результат сортировки деревом

        #region Конструкторы

        public MyArray(int size = 0) //Конструктор с размером
        {
            arr = new int[size];
        }

        public MyArray(int[] Arr) // Конструктор с массивом
        {
            arr = Arr;
        }

        #endregion

        #region Сортировки

        private void InsertionSort(out int countEqual, out int countSwap) // Сортировка простыми вставками
        {
            countEqual = 0;
            countSwap = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                countEqual++;
                countSwap++;
                int newElement = arr[i];
                int location = i - 1;
                while (location >= 0 && arr[location] > newElement)
                {
                    countEqual++;
                    countSwap++;
                    arr[location + 1] = arr[location];
                    location = location - 1;
                }

                arr[location + 1] = newElement;
            }
        }

        private void BinaryTreeSort(out int countCompare, out int countSwap) // Сортировка бинарным деревом
        {
            countCompare = 0;
            countSwap = 0;
            root = null;
            result.Clear();
            FormTree(arr, out countCompare, out countSwap);
            GetSortedNumRec(root);
            arr = result.ToArray();
        }

        private static void AddToTreeElement(int value, ref TreeElement localRoot, ref int countCompare) //Добавление элемента в дерево
        {
            if (localRoot == null)
            {
                localRoot = new TreeElement(value);
                return;
            }
            countCompare++;
            if (localRoot.Data < value)
            {
                AddToTreeElement(value, ref localRoot.Right, ref countCompare);
            }
            else
            {
                AddToTreeElement(value, ref localRoot.Left, ref countCompare);
            }
        }

        public static void FormTree(int[] arr, out int countCompare, out int countSwap) //Дерево из массива
        {
            countCompare = 0;
            countSwap = 0;
            foreach (int el in arr)
            {
                AddToTreeElement(el, ref root, ref countCompare);
                countSwap++;
            }
        }

        private static void GetSortedNumRec(TreeElement node) // Обход дерева
        {
            if (node != null)
            {
                GetSortedNumRec(node.Left);
                result.Add(node.Data);
                GetSortedNumRec(node.Right);
            }
        }

        #endregion

        #region Создание массивов для сортировки

        private MyArray CreateArrayIncrease() //Создание массива, упорядоченного по возрастанию
        {
            for (int i = 0; i < arr.Length; i++) arr[i] = i + 1;

            return new MyArray(arr);
        }

        private MyArray CreateArrayDecrease() //Создание массива, упорядоченного по убыванию
        {
            for (int i = 0; i < arr.Length; i++) arr[i] = arr.Length - i;

            return new MyArray(arr);
        }

        private MyArray CreateArrayRandom() //Создание не упорядоченного массива
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++) arr[i] = rnd.Next(-100, 101);

            return new MyArray(arr);
        }

        #endregion

        #region Сортировка различных массивов

        public void IncreaseSort() //Сортировка возрастающего массива
        {
            var array2 = CreateArrayIncrease();
            Console.WriteLine("Массив до сортировки:");
            Show();

            InsertionSort(out var countSravn, out var countSwap);
            Console.WriteLine("\n\nСортировка простыми вставками");
            Console.WriteLine("Кол-во сравнений: {0}", countSravn);
            Console.WriteLine("Кол-во перестановок: {0}", countSwap);
            Console.WriteLine("Отсортированный массив: ");
            Show();

            array2.BinaryTreeSort(out countSravn, out countSwap);
            Console.WriteLine("\n\nСортировка с помощью бинарного дерева");
            Console.WriteLine("Кол-во сравнений: {0}", countSravn);
            Console.WriteLine("Кол-во перестановок: {0}", countSwap);
            Console.WriteLine("Отсортированный массив: ");
            array2.Show();

            Console.WriteLine("\n\nДля продолжения нажмите на любую клавишу...");
            Console.ReadKey(true);
        }

        public void DecreaseSort() //Сортировка убывающего массива
        {
            var array2 = CreateArrayDecrease();
            Console.WriteLine("Массив до сортировки:");
            Show();

            InsertionSort(out var countSravn, out var countSwap);
            Console.WriteLine("\n\nСортировка простыми вставками");
            Console.WriteLine("Кол-во сравнений: {0}", countSravn);
            Console.WriteLine("Кол-во перестановок: {0}", countSwap);
            Console.WriteLine("Отсортированный массив: ");
            Show();

            array2.BinaryTreeSort(out countSravn, out countSwap);
            Console.WriteLine("\n\nСортировка с помощью бинарного дерева");
            Console.WriteLine("Кол-во сравнений: {0}", countSravn);
            Console.WriteLine("Кол-во перестановок: {0}", countSwap);
            Console.WriteLine("Отсортированный массив: ");
            array2.Show();

            Console.WriteLine("\n\nДля продолжения нажмите на любую клавишу...");
            Console.ReadKey(true);
        }

        public void RandomSort() //Сортировка раномного массива
        {
            var array2 = CreateArrayRandom();
            Console.WriteLine("Массив до сортировки:");
            Show();

            InsertionSort(out var countSravn, out var countSwap);
            Console.WriteLine("\n\nСортировка простыми вставками");
            Console.WriteLine("Кол-во сравнений: {0}", countSravn);
            Console.WriteLine("Кол-во перестановок: {0}", countSwap);
            Console.WriteLine("Отсортированный массив: ");
            Show();

            array2.BinaryTreeSort(out countSravn, out countSwap);
            Console.WriteLine("\n\nСортировка с помощью бинарного дерева");
            Console.WriteLine("Кол-во сравнений: {0}", countSravn);
            Console.WriteLine("Кол-во перестановок: {0}", countSwap);
            Console.WriteLine("Отсортированный массив: ");
            array2.Show();

            Console.WriteLine("\n\nДля продолжения нажмите на любую клавишу...");
            Console.ReadKey(true);
        }

        #endregion

        public void Show() // Вывод массива
        {
            foreach (var element in arr) Console.Write(element + " ");
        }
    }

    class Program
    {
        public static int Menu(string headLine, params string[] paragraphs) // Наикрасивейшее меню
        {
            Console.Clear();
            Console.WriteLine(headLine);
            int paragraph = 0, x = 2, y = 5, oldParagraph = 0;
            Console.CursorVisible = false;
            for (int i = 0; i < paragraphs.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + i);
                Console.Write(paragraphs[i]);
            }
            bool choice = false;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x, y + oldParagraph);
                Console.Write(paragraphs[oldParagraph] + " ");

                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + paragraph);
                Console.Write(paragraphs[paragraph]);

                oldParagraph = paragraph;

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        paragraph++;
                        break;
                    case ConsoleKey.UpArrow:
                        paragraph--;
                        break;
                    case ConsoleKey.Enter:
                        choice = true;
                        break;
                }
                if (paragraph >= paragraphs.Length)
                    paragraph = 0;
                else if (paragraph < 0)
                    paragraph = paragraphs.Length - 1;
                if (choice)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.CursorVisible = true;
                    Console.Clear();
                    break;
                }
            }
            Console.Clear();
            Console.CursorVisible = true;
            return paragraph;
        }

        private static int Input(string task)
        //Ввод целых чисел
        {
            int number;
            bool ok = false;
            do
            {
                Console.WriteLine(task);
                ok = int.TryParse(Console.ReadLine(), out number);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ввод неправильный, нужно ввести целое число. Повторите попытку:\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!ok);
            return number;
        }

        //Ввод числа в гранях
        private static int ReadVGran(int min, int max, string task, string name = null)
        {
            int chislo;
            do
            {
                chislo = Input(task);
                if (chislo <= min || chislo >= max)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка! " + name + " должен(-но) быть больше, чем {0} и меньше, чем {1}. Попробуйте ещё раз:\n", min, max);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (chislo <= min || chislo >= max);
            return chislo;
        }

        private static void CreateArray(ref MyArray array)
        {
            while (true)
            {
                var size = ReadVGran(0, 101, "Введите размер массива:", "Размер массива");
                if (size == 0)
                {
                    Console.WriteLine("Размер массива не может быть равен 0! Повторите ввод...");
                }
                else
                {
                    array = new MyArray(size);
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string[] mainMenu = {
                "Пересоздать массив", "Отсортировать массив, упорядоченный по возрастанию (2-мя методами)",
                "Отсортировать массив, упорядоченный по убыванию (2-мя методами)",
                "Отсортировать неупорядоченный массив (2-мя методами)",
                "Выход"};
            MyArray array = new MyArray();
            CreateArray(ref array);
            while (true)
            {
                var sw = Menu("Доброго времени суток!\nДанная программа сравнивает сортировки одномерного массива.\n" +
                    "Сортировку простыми вставками и сортировку с помощью двоичного дерева\nПриятного пользования!", mainMenu);
                switch (sw)
                {
                    case 0:
                        CreateArray(ref array);
                        break;
                    case 1:
                        array.IncreaseSort();
                        break;
                    case 2:
                        array.DecreaseSort();
                        break;
                    case 3:
                        array.RandomSort();
                        break;
                    case 4:
                        return;
                }
            }
        }
    }
}
