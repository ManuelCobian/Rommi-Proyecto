using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Filtrar
    {
        String value = "This is a short string.";
        Char delimiter = 's';

        public void filtrar()
        {
            String[] substrings = value.Split(delimiter);
            foreach (var substring in substrings)
                Console.WriteLine(substring);
        }
       
    }
}
