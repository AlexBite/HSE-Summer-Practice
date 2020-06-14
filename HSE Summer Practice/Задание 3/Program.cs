/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * 60 (в)
 * Ввести коордиты точки и, в зависимости от того, входит она в область
 * пересечения графиков или нет, присвоить переменной U определённое значение.
 */

using System;

namespace Задача_3
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, y, U;
            bool ok;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Доброго времени суток!\nДанная программа принимает на вход две абсцису и ординату точки и проверяет,\n" +
                "входит ли эта точка в область пересечения графиков:\nx^2+(y-1)^2=1 и y=1-x^2\nПриятного пользования!\n");
            do
            {
                Console.WriteLine("Введите координату X:");
                string buf = Console.ReadLine();
                ok = double.TryParse(buf, out x);
                if (!ok)
                    Console.WriteLine("Ошибка числового ввода, попробуйте ещё раз.");
            } while (!ok);

            do
            {
                Console.WriteLine("Введите координату Y:");
                string buf = Console.ReadLine();
                ok = double.TryParse(buf, out y);
                if (!ok)
                    Console.WriteLine("Ошибка числового ввода, попробуйте ещё раз.");
            } while (!ok);


            bool area = (y <= 1 - Math.Pow(x, 2)) && (Math.Pow(x, 2) + Math.Pow(y - 1, 2) <= 1);
            if (area)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Точка с заданными координатами входит в область пересечения графиков (U = x-y)");
                U = x - y;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Точка с заданными координатами не входит в область пересечения графиков (U = x*y + 7)");
                U = x * y + 7;
            }
            Console.WriteLine("U = {0}", U);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nДля выхода нажмите клавишу Enter...");
            Console.CursorVisible = false;
            Console.ReadLine();
        }
    }
}
