using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР12_Часть4
{
    public class HashTable<T> : ICollection<T>, IEnumerable<T> where T : IComparable, ICloneable
    {
        private PointHashTable<T>[]? table;
        public PointHashTable<T>[]? Table => table;
        private int Capacity;
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        //конструктор для создания коллекции с емкостью capacity
        public HashTable(int capacity = 10)
        {
            Capacity = capacity;
            table = new PointHashTable<T>[Capacity];
            Count = 0;
        }


        //конструктор для инициализации элементами и емкостью коллекции t
        public HashTable(HashTable<T> t)
        {
            HashTable<T> tb = new HashTable<T>(t.Capacity);
            T help;
            foreach (T item in t)
            {
                help = (T)item.Clone();
                tb.Add(item);
                Count++;
            }
            table = tb.table;
            Capacity = t.Capacity;
        }

        //нумератор
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] != null)
                {
                    PointHashTable<T> p = table[i];
                    while (p != null)
                    {
                        yield return p.value;
                        p = p.next;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //добавление элемента
        public void Add(T item)
        {
            PointHashTable<T> point = new PointHashTable<T>(item);
            if (item == null) return;
            int index = point.GetHashCode() % Capacity;
            if (table[index] == null)
            {
                table[index] = point;
                Count++;
            }
            else
            {
                PointHashTable<T> cur = table[index];
                if (string.Compare(cur.key.ToString(), point.key.ToString()) == 0) return;
                while (cur.next != null)
                {
                    cur = cur.next;
                    if (string.Compare(cur.key.ToString(), point.key.ToString()) == 0) return;

                }
                cur.next = point;
                Count++;
            }
            return;
        }
        //индексатор для получения элемента по ключу
        public T this[T key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("Ключ не может быть равен null");

                PointHashTable<T> point = new PointHashTable<T>(key);

                int index = point.GetHashCode() % Capacity;
                if (!this.Contains(key))
                {
                    throw new Exception("Искомого элемента нет в таблице");
                }

                PointHashTable<T> current = table[index];
                while (current.key.ToString() != key.ToString())
                {
                    current = current.next;
                }
                return current.value;
            }
        }
        //добавление элементов
        public void Add(params T[] mas)
        {
            foreach (T item in mas)
            {
                this.Add(item);
            }
        }

        //печать хеш-таблицы
        public void Print()
        {
            if (table == null)
            {
                Console.WriteLine("Таблица пустая!");
                return;
            }
            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] == null) Console.WriteLine(i + " : ");
                else
                {
                    Console.Write(i + " : ");
                    PointHashTable<T> p = table[i];
                    while (p != null)
                    {
                        Console.WriteLine(p);
                        p = p.next;
                    }
                    Console.WriteLine();
                }
            }
        }

        //чистка коллекции
        public void Clear()
        {
            table = new PointHashTable<T>[Capacity];
            Count = 0;
        }
        //проверка, содержит ли коллекция элемент с ключом k
        bool ICollection<T>.Contains(T k)
        {
            PointHashTable<T> point = new PointHashTable<T>(k);
            int code = Math.Abs(point.GetHashCode()) % Capacity;
            if (k.CompareTo(table[code].key) == 0)
                return true;
            point = table[code];
            while (point != null)
            {
                if (k.CompareTo(point.key) == 0) return true;
                point = point.next;
            }
            return false;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public HashTable<T> CopyFrom(HashTable<T> t)
        {
            return t;
        }
        //удаление элемента
        public bool Remove(T item)
        {
            PointHashTable<T> lp = new PointHashTable<T>(item);
            int code = lp.GetHashCode() % Capacity;
            lp = table[code];
            if (table[code] == null) return false;
            if (table[code] != null && item.CompareTo(table[code].value) == 0)
            {
                table[code] = table[code].next;
                Count--;
                return true;
            }
            while (lp.next != null && (item.CompareTo(lp.next.value) != 0))
                lp = lp.next;
            if (lp.next != null)
            {
                lp.next = lp.next.next;
                Count--;
                return true;
            }
            return false;
        }

        //удаление элементов
        public bool Remove(params T[] mas)
        {
            int i = 0;
            foreach (T item in mas)
            {
                if (this.Remove(item))
                {
                    i += 1;
                }
            }
            if (i != 0)
            {
                return true;
            }
            return false;
        }
    }

}
