/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * Напишите рекурсивный метод создания циклического списка, в информационные
 * поля элементов которого последовательно заносятся номера с 1 до N (N водится с
 * клавиатуры). Первый включенный в список элемент, имеющий номер 1, оказывается в
 * хвосте списка (последним). Разработайте рекурсивные методы поиска и удаления
 * элементов списка.
 */

using System;

namespace Задача_9
{
    class Program
    {
        static CycleList cycleList; // Список
        public static int Menu(int k, string headLine, params string[] pechat) //Меню
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(headLine);
            int tek = 0, x = 2, y = 5, tekold = 0;
            Console.CursorVisible = false;
            var ok = false;
            for (var i = 0; i < pechat.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                if (i % (k + 1) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(pechat[i]);
            }
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(x, y + tekold);
                Console.Write(pechat[tekold] + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(x, y + tek);
                Console.Write(pechat[tek]);
                tekold = tek;
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        tek += k + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        tek -= k + 1;
                        break;
                    case ConsoleKey.Enter:
                        ok = true;
                        break;
                }

                if (tek >= pechat.Length)
                    tek = 0;
                else if (tek < 0)
                    tek = pechat.Length - 1;
            } while (!ok);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            return tek + 1;
        }

        //Ввод целых чисел
        private static int Input(string task)
        {
            int number;
            bool ok = false;
            do
            {
                Console.Write(task);
                ok = int.TryParse(Console.ReadLine(), out number);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nВвод неправильный, нужно ввести целое число. Повторите попытку:\n");
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
                    Console.WriteLine("\nОшибка! " + name + " должен(-но) быть больше, чем {0} и меньше, чем {1}. Попробуйте ещё раз:\n", min, max);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (chislo <= min || chislo >= max);
            return chislo;
        }

        public static void Continue() //Ожидание
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nДля продолжения нажмите любую клавишу...");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        private static void Create() //Создание массива
        {
            int N = ReadVGran(0, 101, "Введите количество элементов списка (N): ", "Количество элементво списка");
            Console.WriteLine("\nСозданный список:\n");
            cycleList = new CycleList();
            cycleList.CreateCircularList(N);
            cycleList.Show();
            Console.WriteLine();
            Continue();
        }

        private static void Find() //Поиск элемента
        {
            int value = Input("Введите элемент, который хотите найти: ");
            Point wanted = cycleList.Search(value, cycleList.head, cycleList.tail);
            if (wanted.next == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nВ списке нет элемента с введённым значением");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nНайденный элемент: {0}\nСледующий элемент: {1}", wanted.data, wanted.next.data);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Continue();
        }

        private static void Delete(out int k) // Удаление элемента
        {
            k = 0;
            int value = Input("Введите элемент, который хотите удалить из списка: ");
            cycleList.head = cycleList.Remove(value, cycleList.tail, cycleList.head, cycleList.tail);
            if (cycleList.head == null)
            {
                k = 3;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nВ списке не осталось элементов, для дальнейшей работы необходимо пересоздать список");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Continue();
        }

        static void Main(string[] args)
        {
            int k = 3;
            string[] menu = { "Создать список", "Показать список", "Найти элемент", "Удалить элемент", "Выход" };
            while (true)
            {
                int sw = Menu(k, "Доброго времени суток!\nДанная программа реализует рекурсивные методы создания циклического списка,\n" +
                "а также поиска и удаления элементов.\nПриятного пользования!\n", menu);
                switch (sw)
                {
                    case 1:
                        Create();
                        menu[0] = "Пересоздать список";
                        k = 0;
                        break;
                    case 2:
                        Console.WriteLine("Список имеет вид:\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        cycleList.Show();
                        Console.WriteLine();
                        Continue();
                        break;
                    case 3:
                        Find();
                        break;
                    case 4:
                        Delete(out k);
                        break;
                    case 5:
                        return;
                }
            }
        }
    }
}
