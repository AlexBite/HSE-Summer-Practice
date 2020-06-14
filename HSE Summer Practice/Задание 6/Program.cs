/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * Ввести A1 , A2 , A3 , М. Построить последовательность чисел Aк = Aк–1 / 2 + Aк-2 / 2 + Aк–3 / 2.
 * Довести ее до An <= М. Напечатать последовательность, N. Сообщить, выполняется ли
 * равенство а N = М.
 */

using System;

namespace Задача_6
{
    class Program
    {
        private static double Input(string task)
        //Ввод целых чисел
        {
            double number;
            bool ok = false;
            do
            {
                Console.WriteLine(task);
                ok = double.TryParse(Console.ReadLine(), out number);
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ввод неправильный, нужно ввести целое число. Повторите попытку:\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!ok);
            return number;
        }

        //Рекурсия последовательности
        static void Recursion(double ak1, double ak2, double ak3)
        {
            double newA = ak1 / 2 + ak2 / 2 + ak3 / 2;
            Console.Write("\na{0} = {1}\n", count, newA);
            if (newA < Double.MinValue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nВведена убывающая последовательность, максимум никогда не будет достигнут");
                return;
            }
            if (!Check(newA))
                Recursion(newA, ak1, ak2);
            else return;
        }

        //Проверка на минимум
        static bool Check(double a)
        {
            if (a >= M)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (a > M)
                    Console.WriteLine("\nN = {0}, An = {1}, An > M", count, a);
                else
                    Console.WriteLine("\nN = {0}, An = {1}, An = M", count, a);
                return true;
            }
            else
            {
                count++;
                return false;
            }
        }

        static double M;
        static int count = 1;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            double a1 = Input("Введите первое число последовательнсоти:");
            double a2 = Input("Введите второе число последовательности:");
            double a3 = Input("Введите третье число последовательности:");
            M = Input("Введите максимум последовательности:");

            Console.WriteLine("\na{0} = {1}\n", count, a1);
            if (!Check(a1))
            {
                Console.WriteLine("a{0} = {1}\n", count, a2);
                if (!Check(a2))
                {
                    Console.WriteLine("a{0} = {1}", count, a3);
                    if (!Check(a3))
                        Recursion(a3, a2, a1);
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nДля выхода нажмите клавишу Enter...");
            Console.CursorVisible = false;
            Console.ReadLine();
        }
    }
}
