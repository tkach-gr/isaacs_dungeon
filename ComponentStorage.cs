using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSIsaac
{
    static class ComponentStorage<T>
    {
        public static List<T> Components { get; set; }

        static ComponentStorage()
        {
            Components = new List<T>();
        }
    }
}
