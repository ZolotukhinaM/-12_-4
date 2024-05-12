using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using ClassLibrary2;
namespace ЛР12_Часть4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4 ЧАСТЬ");
            CelestialBody[] ar = new CelestialBody[15];
            Console.WriteLine("вспомогательный массив:");
            for (int i = 0; i < ar.Length / 2; i++)
            {
                CelestialBody item = new CelestialBody();
                item.RandomInit();
                ar[i] = item;
                Console.WriteLine(item);
            }
            for (int i = ar.Length / 2; i < ar.Length; i++)
            {
                CelestialBody it = new CelestialBody();
                it.RandomInit();
                ar[i] = it;
                Console.WriteLine(it);
            }

            HashTable<CelestialBody> t = new HashTable<CelestialBody>();
            t.Add(ar);
            Console.WriteLine("хеш-таблица:");
            t.Print();
            Console.WriteLine();

            Console.WriteLine("перебор foreachем:");
            foreach (var item in t)
                Console.WriteLine(item.ToString());
            Console.WriteLine();
            Console.WriteLine("хеш-таблица, проинициализированная элементами и емкостью прошлой коллекции коллекции:");

            HashTable<CelestialBody> tb = new(t);
            tb.Print();
            Console.WriteLine($"количество элементов в таблице: {tb.Count}");
            Console.WriteLine();

            CelestialBody men1 = new CelestialBody();
            men1.Init();
            bool check = tb.Contains(men1);
            Console.WriteLine($"таблица содержит элемент с ключом {men1}: {check}");
            if (check)
            {
                Console.WriteLine($"значение элемента с индексом {men1} : {tb[men1]}");
            }
            Console.WriteLine();

            Console.WriteLine("вспомогательный массив, для удаления элементов из таблицы:");
            CelestialBody[] ar2 = new CelestialBody[5];

            for (int i = 0; i < ar2.Length; i++)
            {
                ar2[i] = ar[i];
                Console.WriteLine(ar[i]);
            }

            tb.Remove(ar2);
            tb.Print();
            Console.WriteLine($"количество элементов в таблице: {tb.Count}");
            Console.WriteLine();

            Console.WriteLine("таблица после чистки:");
            tb.Clear();
            tb.Print();
            Console.WriteLine($"количество элементов в таблице: {tb.Count}");

            Console.ReadKey();

        }
    }
}
