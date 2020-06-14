using System;

namespace Задача_9
{
    class Point // Класс списка
    {
        public int data { get; set; } //информационное поле
        public Point next { get; set; } //адресное поле

        public Point(int Data = 0) //Конструктор с параметрами
        {
            data = Data;
            next = null;
        }
    }

    class CycleList
    {
        public Point head { get; set; } // Начало списка

        public Point tail { get; set; } //Конец списка

        public CycleList() //Конструктор без параметров
        {
            head = null;
            tail = null;
        }

        public void CreateCircularList(int N) //Конструктор с параметрами
        {
            if (N == 1)
            {
                Point head = new Point(N); //Создание начала

                Point tail = head;
                //Привязка конца к началу
                head.next = tail;
                tail.next = head;

                this.head = tail.next;
                this.tail = tail;
            }
            else
            {
                Point head = new Point(N); //Создание начала

                Point tail = new Point(1); //Создание конца

                //Привязка конца к началу
                head.next = tail;
                tail.next = head;

                Add(N - 1, head, tail);

                this.head = tail.next;
                this.tail = tail;
            }
        }

        public static void Add(int N, Point head, Point tail) //Рекурсивное добавление элементов
        {
            if (N == 1)
                return;
            else
            {
                Point buf = new Point(N);
                head.next = buf;
                buf.next = tail;
                Add(N - 1, buf, tail);
            }
        }

        public Point Search(int value, Point curr, Point end) //Рекурсивный поиск элемента
        {
            if (curr.data == value || (curr.data == end.data && curr.next == end.next)) //Проверка на список из одного элемента
            {
                if (curr.data == value)
                    return curr;
                else
                    return new Point(0);
            }
            else
            {
                Point wanted = Search(value, curr.next, end);
                return wanted;
            }
        }

        public Point Remove(int value, Point prev, Point curr, Point end) //Рекурсивное удаление элемента
        {
            if (curr.data == value && (curr.data == end.data && curr.data == head.data)) //Проверка на список из одного элемента
            {
                end = null;
                return end;
            }
            else if (curr.data == value || (curr.data == end.data && curr.next == end.next))
            {
                if (curr.data == value)
                {
                    if (curr.data == head.data)
                    {
                        prev.next = curr.next;
                        return curr.next;
                    }
                    else if (curr.next != end.next)
                    {
                        prev.next = curr.next;
                        return head;
                    }
                    else if (curr.data == end.data && head.next == curr)
                    {
                        tail = head;
                        prev.next = curr.next;
                        return head;
                    }
                    prev.next = curr.next;
                    return curr.next;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nВ списке нет элемента с введённым значением");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return curr.next;
            }
            else if (curr.data != value && tail.next == head.next)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nВ списке нет элемента с введённым значением");
                Console.ForegroundColor = ConsoleColor.White;
                return head;
            }
            else
            {
                Point deleted = Remove(value, prev.next, curr.next, end);
                return deleted;
            }
        }

        public void Show() //Вывод списка
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Point current = head;
            do
            {
                Console.Write(current.data + " ");
                current = current.next;
            } while (current != head);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
