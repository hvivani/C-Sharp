using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tcplistener_console
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.Write(“tcplistener by hvivani – 20110225 – para el negro: \r\n”);
            if (args.Length == 0)
            {
                System.Console.WriteLine(“Parametros: Puerto IP   \r\n”);
                System.Console.WriteLine(“Ejemplo: tcplistener 2525 127.0.0.1  \r\n”);
                System.Console.WriteLine(“si tenes mas de una interfaz de red, usa la ip de dicha interfaz.  \r\n”);
                return 1;
            }
            else
            {
                multicnnNew.iniciar(args[0], args[1]);
                return 0;
            }

        } 
    }
}
