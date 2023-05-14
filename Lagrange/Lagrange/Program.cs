using System;
using System.IO;

namespace Lagrange
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите метод: ");
            Console.WriteLine("1. Метод Чебышева");
            Console.WriteLine("2. Метод с равностоящими узлами");
            string p = Console.ReadLine();
            switch (p) {
                case "1":
                    Chebyshev();
                    break;
                case "2":
                    Equalnodes();
                    break;
                default:
                    Console.WriteLine("Таково пункта нету");
                    break;
            }
            Console.Read();
        }
        public static void GetInfoFromFile(out int ncount, out int a, out int b, string path) {//Функция для считывания данных из файла
            ncount = 0; a = 0; b = 0;
            if (File.Exists(path))
            {
                var mas = File.ReadAllLines(path);
                ncount = int.Parse(mas[0]);
                a = int.Parse(mas[1]);
                b = int.Parse(mas[2]);
            }
            else {
                Console.WriteLine("Файла не существует");
            }
        }
        public static void Chebyshev()//C узлами Чебышева
        { 
            Console.WriteLine("Введите название файла: ");

            string path = Console.ReadLine()+".txt";
            int ncount;//кол-во узлов интерполяции
            int a; //начало отрезка
            int b;//конец отрезка
            GetInfoFromFile(out ncount, out a, out b,path);
            if (ncount == 0) {
                return;
            }
            Console.WriteLine("Количество узлом интерполяции: "+ncount);
            Console.WriteLine("Начало отрезка: " + a);
            Console.WriteLine("Конец отрезка: " + b);
            double[] nodes = new double[ncount];

            double x = 0;
            for (int i = 0; i < ncount; i++)
            {
                x = Math.Cos(Math.PI * (2 * (i + 1) - 1) / (2 * ncount));
                x = Math.Round(x, 2);
                nodes[i] = x;
                // в nodes узлы интерполяции
            }

            for (int i = 0; i < ncount; i++)
                Console.Write(nodes[i] + " ");
            //Вывод узлов интерполяции

            Console.WriteLine("\n");
            for (x = a; x < b; x = Math.Round(x += 0.01, 3))
            {
                double sum = 0;
                for (int i = 0; i < ncount; i++)
                {
                    double multi = 1;
                    for (int j = 0; j < ncount; j++)
                    {
                        if (i != j) multi = multi * ((x - nodes[j]) / (nodes[i] - nodes[j]));
                    }
                    sum = sum + Math.Abs(x) * multi;
                }
                Console.WriteLine("Разница: {0,-20}, Аргумент: {1,-5}", sum - Math.Abs(x), x);
            }
        }
        public static void Equalnodes()//С равностоящими узлами
        {
            Console.WriteLine("Введите название файла: ");
            string path = Console.ReadLine() + ".txt";
            int ncount;//кол-во узлов интерполяции
            int a; //начало отрезка
            int b;//конец отрезка
            GetInfoFromFile(out ncount, out a, out b, path);
            if (ncount == 0)
            {
                return;
            }
            Console.WriteLine("Количество узлом интерполяции: " + ncount);
            Console.WriteLine("Начало отрезка: " + a);
            Console.WriteLine("Конец отрезка: " + b);
            double[] nodes = new double[ncount];
            double d = (b - a);
            d = d / ncount;

            double x = a;
            for (int i = 0; i < ncount; i++)
            {
                x += d;
                x = Math.Round(x, 3);
                nodes[i] = x;
                // в nodes узлы интерполяции
            }

            for (int i = 0; i < ncount; i++)
                Console.Write(nodes[i] + " ");
            //Вывод узлов интерполяции

            Console.WriteLine("\n");
            for (x = a; x < b; x = Math.Round(x += 0.01, 3))
            {
                double sum = 0;
                for (int i = 0; i < ncount; i++)
                {
                    double multi = 1;
                    for (int j = 0; j < ncount; j++)
                    {
                        if (i != j) multi = multi * ((x - nodes[j]) / (nodes[i] - nodes[j]));
                    }
                    sum = sum + Math.Exp(x) * multi;
                }
                Console.WriteLine("Разница: {0,-20}, Аргумент: {1,-5}", sum - Math.Exp(x), x);
            }
        }
    }
}
