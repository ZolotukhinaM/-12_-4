using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР12_Часть4
{
    public class PointHashTable<T> // HT<T>
    {
        public T? key;//ключ
        public T? value;//значение
        public PointHashTable<T>? next;//ссылка на следующий элемент

        public PointHashTable(T elem)
        {
            value = elem;
            key = value;
            next = null;
        }

        public override string ToString()
        {
            return key + ": " + value.ToString();
        }

        public override int GetHashCode()
        {
            string v = key.ToString();
            int help = 0;
            foreach (char c in v)
                help += (int)c;
            int code = 0;
            while (help != 0)
            {
                code += help % 10;
                help = help / 10;
            }
            return code;
        }
    }
}
