using System;


class Program
{
    static void Main()
    {
        Console.WriteLine("¡Bienvenido  Battleship!");
        Console.WriteLine("");
        Console.WriteLine("Las reglas son simples, encuentra los 3 barcos que estan escondidos en el tablero, estos son de diferentes tamaños pero si impactas una casilla el la que se encuentra el mismo, este se hundira por completo");
        Console.WriteLine("");
        Console.WriteLine( "OBTEN LA VICTORIA NAVAL!");
        Console.WriteLine("");
        Console.WriteLine("Presiona cualquier tecla para comenzar...");
        Console.ReadKey();

        char[,] tablero = new char[8, 8];
        iniciartablero(tablero);
        agregarbarco(tablero, 3);

        while (true)
        {
            Console.Clear();
            mostrartablero(tablero);
            Console.WriteLine("Ingresa la coordenada para buscar un barco (fila)");
            int fila = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Ingresa la coordenada para buscar un barco (columna)");
            int columna = int.Parse(Console.ReadLine()) - 1;

            if (fila < 0 || fila >= tablero.GetLength(0) || columna < 0 || columna >= tablero.GetLength(1))
            {
                Console.WriteLine("Coordenadas inválidas. Intenta nuevamente.");
                Console.ReadKey();
                continue;
            }

            if (golpearbarco(tablero, fila, columna))
            {
                Console.WriteLine("Has encontrado un barco, Pulsa cualquier tecla para limpiar las coordenadas");
                if (nomasbarcos(tablero))
                {
                    Console.Clear() ;
                    Console.WriteLine("Todos los barcos han sido derribados, bien hecho capitan!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("No hay barco en esa posición, Pulsa cualquier tecla para limpiar las coordenadas");
            }
            Console.ReadKey();
        }
    }

    static void iniciartablero(char[,] tablero)
    {
        for (int fila = 0; fila < tablero.GetLength(0); fila++)
        {
            for (int columna = 0; columna < tablero.GetLength(1); columna++)
            {
                tablero[fila, columna] = '~';
            }
        }
    }

    static void mostrartablero(char[,] tablero)
    {
        Console.WriteLine("  1 2 3 4 5 6 7 8");
        for (int fila = 0; fila < tablero.GetLength(0); fila++)
        {
            Console.Write((fila + 1) + " ");
            for (int columna = 0; columna < tablero.GetLength(1); columna++)
            {
                if (tablero[fila, columna] == '~' || tablero[fila, columna] == 'X')
                {
                    Console.Write(tablero[fila, columna] + " ");
                }
                else
                {
                    Console.Write("~ ");
                }
            }
            Console.WriteLine();
        }
    }

    static void agregarbarco(char[,] tablero, int cantidadBarcos)
    {
        for (int i = 0; i < cantidadBarcos; i++)
        {
            ponerbarco(tablero, tamaño());
        }
    }

    static int tamaño()
    {
        Random rnd = new Random();
        return rnd.Next(1, 4);
    }

    static void ponerbarco(char[,] tablero, int tamañoBarco)
    {
        Random rnd = new Random();
        int fila;
        int columna;
        bool orientacionHorizontal;

        do
        {
            fila = rnd.Next(tablero.GetLength(0));
            columna = rnd.Next(tablero.GetLength(1));
            orientacionHorizontal = rnd.Next(2) == 0;
        } while (!sepuedeagregarbarco(tablero, fila, columna, tamañoBarco, orientacionHorizontal));

        barcosentablero(tablero, fila, columna, tamañoBarco, orientacionHorizontal);
    }

    static bool sepuedeagregarbarco(char[,] tablero, int fila, int columna, int tamañoBarco, bool orientacionHorizontal)
    {
        if (orientacionHorizontal)
        {
            if (columna + tamañoBarco > tablero.GetLength(1))
                return false;

            for (int i = 0; i < tamañoBarco; i++)
            {
                if (tablero[fila, columna + i] != '~')
                    return false;
            }
        }
        else
        {
            if (fila + tamañoBarco > tablero.GetLength(0))
                return false;

            for (int i = 0; i < tamañoBarco; i++)
            {
                if (tablero[fila + i, columna] != '~')
                    return false;
            }
        }

        return true;
    }

    static void barcosentablero(char[,] tablero, int fila, int columna, int tamañoBarco, bool orientacionHorizontal)
    {
        char barco = (char)('A' + tamañoBarco - 1);

        if (orientacionHorizontal)
        {
            for (int i = 0; i < tamañoBarco; i++)
            {
                tablero[fila, columna + i] = barco;
            }
        }
        else
        {
            for (int i = 0; i < tamañoBarco; i++)
            {
                tablero[fila + i, columna] = barco;
            }
        }
    }

    static bool golpearbarco(char[,] tablero, int fila, int columna)
    {
        if (tablero[fila, columna] != '~' && tablero[fila, columna] != 'X')
        {
            derribarbarco(tablero, fila, columna);
            return true;
        }
        else
        {
            return false;
        }
    }

    static void derribarbarco(char[,] tablero, int fila, int columna)
    {
        int tamañoBarco = tablero[fila, columna] - 'A' + 1;
        bool orientacionHorizontal = bhorizontal(tablero, fila, columna, tamañoBarco);

        if (orientacionHorizontal)
        {
            for (int i = 0; i < tamañoBarco; i++)
            {
                tablero[fila, columna + i] = 'X';
            }
        }
        else
        {
            for (int i = 0; i < tamañoBarco; i++)
            {
                tablero[fila + i, columna] = 'X';
            }
        }
    }

    static bool bhorizontal(char[,] tablero, int fila, int columna, int tamañoBarco)
    {
        for (int i = 0; i < tamañoBarco; i++)
        {
            if (tablero[fila, columna + i] == tablero[fila, columna])
            {
                continue;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    static bool nomasbarcos(char[,] tablero)
    {
        for (int fila = 0; fila < tablero.GetLength(0); fila++)
        {
            for (int columna = 0; columna < tablero.GetLength(1); columna++)
            {
                if (tablero[fila, columna] != '~' && tablero[fila, columna] != 'X')
                {
                    return false;
                }
            }
        }

        return true;
    }
}