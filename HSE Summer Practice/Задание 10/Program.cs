/* Автор:
 * -----
 * Бологов Александр, ПИ19-2
 * 
 * Задача:
 * ------
 * Написать метод уничтожения дерева.
 */

using System;

namespace Задача_10
{
    public class Tree // Класс дерева
    {
        public int data { get; set; } // Информационное поле
        public Tree left { get; set; } // Адрес левого поддерева
        public Tree right { get; set; } // Адрес правого поддерева

        public Tree(int value = 0) // Конструктор
        {
            data = value;
            left = null;
            right = null;
        }

        public static Tree MakeRoot(int value) // Создание элемента дерева
        {
            Tree nPoint = new Tree(value);
            return nPoint;
        }

        public static Tree IdealTree(int size, Random rnd) // Создание случайного дерева
        {
            if (size == 0)
                return null;
            Tree root = MakeRoot(rnd.Next(0, 10));
            int numL = size / 2, numR = size - numL - 1;
            root.left = IdealTree(numL, rnd);
            root.right = IdealTree(numR, rnd);
            return root;
        }

        public static void ShowTree(Tree root, int x, int y, int length = 20) // Красивейший вывод дерева
        {
            if (root == null)
                return;

            Console.SetCursorPosition(x, y);
            Console.Write(root.data);

            if (root.left != null)
            {
                int lengthleft = length;
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine('┘');
                Console.SetCursorPosition(x - lengthleft, y + 1);
                Console.Write("┌");
                while (lengthleft > 1)
                {
                    Console.SetCursorPosition(x - lengthleft + 1, y + 1);
                    Console.Write("─");
                    lengthleft--;
                }
            }

            if (root.right != null)
            {
                int lengthright = length;
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine('└');
                Console.SetCursorPosition(x + lengthright, y + 1);
                Console.Write("┐");
                while (lengthright > 1)
                {
                    Console.SetCursorPosition(x + lengthright - 1, y + 1);
                    Console.Write("─");
                    lengthright--;
                }
            }

            if (root.left != null && root.right != null)
            {
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine('┴');
            }

            length /= 2;
            ShowTree(root.left, x - length * 2, y + 2, length);
            ShowTree(root.right, x + length * 2, y + 2, length);
        }

        public static Tree Kill(Tree root) // Рекурсивное уничтожение элементов дерева
        {
            if (root.left != null)
                Kill(root.left);
            if (root.right != null)
                Kill(root.right);
            root.left = null;
            root.right = null;
            root = null;
            return root;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int size = 10;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Доброго времени суток!\nДанная программа реализует метод уничтожения дерева\nПриятного пользования!");
            Console.CursorVisible = false;
            Random rnd = new Random();
            Tree tree = new Tree();
            tree = Tree.IdealTree(size, rnd); //Создание дерева из 10ти элементов
            Tree.ShowTree(tree, 40, 1 + 4); //Вывод созданного дерева
            Console.WriteLine("\n\n\n\n\n\nНажмите любую клавишу, чтобы уничтожить дерево...");
            Console.ReadKey();
            tree = Tree.Kill(tree); //Уничтожение дерева
            Tree.ShowTree(tree, 40, 6 + size); //Вывод уничтоженого дерева (не знаю зачем)
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Дерево уничтожено"); //Констатация факта
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nДля выхода нажмите клавишу Enter...");
            Console.CursorVisible = false;
            Console.ReadLine();
        }
    }
}
