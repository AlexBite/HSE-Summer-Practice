/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * Граф задан матрицей смежности.Выяснить, является ли он деревом.
 */
using System;

namespace Задача_8
{
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

        public static void Continue() //Продолжение
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nДля продолжения нажмите клавишу Enter...");
            Console.ReadLine();
        }

        public static void OutputGraph() //Вывод графа
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nМатрица смежности для графа:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        //Ввод целых чисел
        private static int Input(string task)
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

        private static bool Check() //Проверка на связность
        {
            int count = 0;
            for (int i = 0; i < kolVer; i++)
            {
                for (int j = 0; j < kolVer; j++)
                    if (matrix[i, j] == 0)
                        count++;
                if (count == 4)
                    return false;
                else
                    count = 0;
            }
            return true;
        }

        public static int kolVer { get; set; } //Количество вершин
        public static int[,] matrix { get; set; } //Матрица смежностей

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Доброго времени суток!\nДанная программа определяет, является ли граф, заданный таблицей смежности, деревом" +
                "\nПриятного пользования!\n");
            int kolReb;
            while (true)
            {
                var sw = Menu("Доброго времени суток!\nДанная программа определяет, является ли граф, заданный таблицей смежности, деревом" +
                "\nПриятного пользования!\n", "Создать матрицу смежности", "Сгенерировать матрицу смежности", "Выход");
                switch (sw)
                {
                    case 0:
                        {
                            kolReb = 0;
                            kolVer = ReadVGran(0, 101, "Введите количество вершин графа:", "Количество вершин графа");
                            matrix = new int[kolVer, kolVer];
                            for (int i = 0; i < kolVer; i++)
                                for (int j = 0; j < i; j++)
                                {
                                    matrix[i, j] = matrix[j, i] =
                                          ReadVGran(-1, 2, "Введите 1, если вершина " + (j + 1) + " и вершина " + (i + 1) + " связаны, или введите 0, если нет:", "Ответ");
                                    if (matrix[i, j] == 1)
                                        kolReb++;
                                }
                            OutputGraph();
                            if (kolVer - kolReb == 1)
                            {
                                if (Check())
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nДанный граф является деревом");
                                    Continue();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nДанный граф не является деревом");
                                    Continue();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nДанный граф не является деревом");
                                Continue();
                            }
                        }
                        break;
                    case 1:
                        {
                            kolReb = 0;
                            Random rnd = new Random();
                            kolVer = rnd.Next(1, 101);
                            matrix = new int[kolVer, kolVer];
                            for (int i = 0; i < kolVer; i++)
                                for (int j = 0; j < i; j++)
                                {
                                    matrix[i, j] = matrix[j, i] = rnd.Next(0, 2);
                                    if (matrix[i, j] == 1)
                                        kolReb++;
                                }
                            OutputGraph();
                            if (kolVer - kolReb == 1)
                            {
                                if (Check())
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nДанный граф является деревом");
                                    Continue();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nДанный граф не является деревом");
                                    Continue();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nДанный граф не является деревом");
                                Continue();
                            }
                        }
                        break;
                    case 2:
                        return;
                }
            }
        }
    }
}
