//Primera aproximación: Recorriendo toda la cadena.

            Boolean palindroma = true;
            string str = new string(Console.ReadLine().ToCharArray());
            for (int i=0; i < str.Length; i++)
            {
                if (str[i] != str[str.Length-i-1])
                    palindroma = false;
            }
            Console.WriteLine(palindroma.ToString());


//Segunda aproximación: Solo evaluamos la mitad de la cadena

            Boolean palindroma = true;
            string str = new string(Console.ReadLine().ToCharArray());
            for (int i=0; i < str.Length; i++)
            {
                if (str[i] != str[str.Length-i-1])
                    palindroma = false;
            }
            Console.WriteLine(palindroma.ToString());
            
//Tercera aproximación: el bucle corta por falso de palindroma.

            string str = new string(Console.ReadLine().ToCharArray());
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (str[i] != str[str.Length - i - 1])
                    return false;
            }
            return true;            
            
            
