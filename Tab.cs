using System;
using Coordinates;

namespace Tab
{
    class Tablero
    {
        // casillas de juego
        private struct Casilla
        {
            public char estado; // "o" sin descubir, "*" mina, "x" bandera
                                // "·" sin minas alrededor, "1" a "8" número de minas alrededor
            public bool mina; // true - hay mina ; false - no hay mina
        }
        private int fils, cols;         // número de filas y columnas del tablero
        private Casilla[,] casilla;     // matriz de casillas del tablero
        private Coor cursor;            // posición del cursor (fila, columna)
        private int nMinas, nMarcadas;  // número de minas y número de casillas marcadas con bandera
        private bool primerClick;       // para garantizar que el primer click no sea una mina
        private bool debug;            // para depuración, el renderizado muestra el tablero con las minas
        static Random rand = new Random(); // generador de aleatorios para colocar minas


        // getters para fils y cols
        public int Fils
        {
            get => fils;
        }
        public int Cols
        {
            get => cols;
        }

        // constructor del tablero de tamaño fils x cols con nMinas distribuidas aleatoriamente
        public Tablero(int fils, int cols, int numMinas)
        {
            this.fils = fils;
            this.cols = cols;
            this.cursor = new Coor(0, 0); // se inicializa el cursor en la posición (0, 0)
            nMinas = numMinas;
            casilla = new Casilla[fils, cols]; // se crea la matriz de casillas
            // se inicializan las casillas
            for (int i = 0; i < fils; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    casilla[i, j].estado = 'o'; // se inicializan todas las casillas como sin descubrir
                    casilla[i, j].mina = false; // se inicializan todas las casillas sin mina
                }
            }
            PonMinas1(nMinas);
        }
        // constructor del tablero para pruebas, conociendo la posición de las minas
        public Tablero(int fils, int cols, (int, int)[] posMinas)
        {
            this.fils = fils;
            this.cols = cols;
            this.cursor = new Coor(0, 0); // se inicializa el cursor en la posición (0, 0)
            casilla = new Casilla[fils, cols]; // se crea la matriz de casillas
            // se inicializan las casillas
            for (int i = 0; i < fils; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    casilla[i, j].estado = 'o'; // se inicializan todas las casillas como sin descubrir
                    casilla[i, j].mina = false; // se inicializan todas las casillas sin mina
                }
            }
            nMinas = posMinas.Length;
            for (int i = 0; i < nMinas; i++)
            {
                casilla[posMinas[i].Item1, posMinas[i].Item2].mina = true;  // se colocan las minas en las posiciones indicadas
            }
        }


        // genera n minas en posiciones aleatorias distintas
        private void PonMinas1(int nMinas)
        {
            int fil, col; // se declaran la variables
            for (int i = 0; i < nMinas; i++)
            {
                fil = rand.Next(fils); // se genera una fila aleatoria entre 0 y fils-1
                col = rand.Next(cols); // se genera una columna aleatoria entre 0 y cols-1
                if (!casilla[fil, col].mina) casilla[fil, col].mina = true; // se coloca la mina si no hay una ya en esa posición
                else i--; // si hay mina, vuelve a generar otra posición
            }
        }


        // método para renderizar el tablero, mostrando las minas si bomba es true
        public void Render(bool bomba)
        {
            ConsoleColor[] colores = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor)); // se obtinen los colores disponibles en la consola
            Console.Clear(); // se limpia la consola

            // se pinta todo el tablero
            for (int i = 0; i < fils; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (casilla[i, j].estado == '*') // casilla con mina descubierta (click del fallo)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("*");
                        Console.ResetColor();
                        Console.Write(" "); // se añade un espacio para mantener el formato del tablero, ya que cada casilla ocupa dos caracteres
                    }
                    else if (casilla[i, j].estado == 'o') // casilla sin descubrir
                    {
                        if (bomba && casilla[i, j].mina) // si bomba es true y la casilla tiene mina, se pinta como casilla con mina sin descubrir
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("*");
                            Console.ResetColor();
                            Console.Write(" "); // se añade un espacio para mantener el formato del tablero, ya que cada casilla ocupa dos caracteres
                        }
                        else // si no, se pinta como casilla sin descubrir normal
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("o ");
                        }
                    }
                    else if (casilla[i, j].estado == 'x') // casilla marcada con bandera
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("x ");
                    }
                    else if (casilla[i, j].estado == 'c') // casilla descubierta sin minas alrededor
                    {
                        Console.Write("  ");
                    }
                    else if (casilla[i, j].estado >= '1' && casilla[i, j].estado <= '8') // casilla descubierta con número de minas alrededor
                    {
                        Console.ForegroundColor = colores[casilla[i, j].estado - '0']; // se asigna un color diferente para cada número de minas alrededor
                        Console.Write(casilla[i, j].estado + " ");
                    }
                    // no hay más casos, pero no se pone un else porque el estado de la casilla solo puede ser uno de los anteriores
                }
                Console.WriteLine(); // salto de línea al final de cada fila
            }

            Console.SetCursorPosition(cursor.Y * 2, cursor.X); // se posiciona el cursor en la posición actual del cursor
            Console.BackgroundColor = ConsoleColor.White; // se pinta el cursor de blanco
            Console.ForegroundColor = ConsoleColor.Black; // se pinta el contenido de negro
            Console.Write(casilla[cursor.X, cursor.Y].estado); // se muestra el estado

            Console.ResetColor(); // se resetean los colores para el siguiente renderizado
            Console.SetCursorPosition(0, fils + 1); // se posiciona el cursor al final del tablero para no sobreescribirlo
        }
        // cambia el debug para que se muestren las minas
        public void ActivaDebug()
        {
            debug = true;
        }


        // método para mover el cursor
        public void MueveCursor(Coor dir)
        {
            // se actualiza la posición del cursor sumando la dirección dada
            cursor.Y += dir.X;
            cursor.X += dir.Y;

            // se asegura que el cursor no se salga del tablero
            if (cursor.X < 0) cursor.X = 0;
            if (cursor.X >= fils) cursor.X = fils - 1;
            if (cursor.Y < 0) cursor.Y = 0;
            if (cursor.Y >= cols) cursor.Y = cols - 1;
        }
        // método para marcar o desmarcar una casilla con bandera
        public void MarcaMina()
        {
            if (casilla[cursor.X, cursor.Y].estado == 'o') // si la casilla está sin descubrir, se marca con bandera
            {
                casilla[cursor.X, cursor.Y].estado = 'x';
                nMarcadas++;
            }
            else if (casilla[cursor.X, cursor.Y].estado == 'x') // si la casilla ya está marcada, se desmarca
            {
                casilla[cursor.X, cursor.Y].estado = 'o';
                nMarcadas--;
            }
        }
    }
}
