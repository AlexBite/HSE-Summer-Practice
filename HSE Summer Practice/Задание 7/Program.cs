/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * Выписать все булевы функции от 3 аргументов, которые не линейны. Выписать их
 * вектора в лексикографическом порядке.
 */

using System;
using System.Collections;

namespace Задача_7
{
    //класс перебора чисел
    public class Search : IEnumerable
    {
        public Search() //Конструктор без параметров
        {

        }

        private string this[int i] => LeadToPattern(BytePresenter(i));

        public IEnumerator GetEnumerator()
        {
            for (var i = 0; i < 256; i++)
                yield return LeadToPattern(this[i]);
        }

        private static string BytePresenter(int number)
        {
            string bytePeresenterString = "";

            do
            {
                bytePeresenterString = number % 2 + bytePeresenterString;
                number /= 2;
            } while (number > 0);

            return bytePeresenterString;
        }

        private string LeadToPattern(string byteNumber)
        {
            while (byteNumber.Length < 8)
                byteNumber = "0" + byteNumber;

            return byteNumber;
        }
    }

    public class BooleanFunction
    {
        private int size = 8;
        private readonly byte[][] vector;

        public BooleanFunction(string vectorString)
        {
            vector = new byte[vectorString.Length][];
            for (var j = 0; j < vectorString.Length; j++)
            {
                vector[j] = new byte[size - j];
                vector[j][0] = byte.Parse(vectorString[j].ToString());
            }
        }

        public bool IsLinear()
        {
            var j = 0;
            while (j < size)
            {
                for (var i = 0; i < vector.Length - 1 - j; i++)
                    vector[i][j + 1] =
                        vector[i + 1][j] == 0 && vector[i][j] == 0 || vector[i + 1][j] == 1 && vector[i][j] == 1
                            ? (byte)0
                            : (byte)1;

                j++;
            }

            for (j = 1; j < size; j++)
                if (vector[0][j] == 1 && !IsPow(j))
                    return false;

            return true;
        }

        private bool IsPow(int digit)
        {
            return digit >= 1 && (digit & (digit - 1)) == 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Доброго времени суток!\nДанная программа выводит все нелинейные векторы от булевых функций\nПриятного пользования!\n");
            Console.WriteLine("Полученные нелинейные функции:\n");
            Search perebor = new Search();
            foreach (string str in perebor)
            {
                string function = "";
                foreach (char element in str) function = string.Concat(function, element);
                BooleanFunction func = new BooleanFunction(function);
                if (!func.IsLinear())
                    Console.WriteLine(function);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nДля выхода нажмите клавишу Enter...");
            Console.CursorVisible = false;
            Console.ReadLine();
        }
    }
}
