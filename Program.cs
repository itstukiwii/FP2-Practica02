
// Marta Reyes Funk
// Diego Gonzalez Martin
// (Hermes)

using Tab;
using Coordinates;

namespace FP2_Practica02
{
    internal class Program
    {
        static void Main()
        {
            const bool DEBUG = false; // para activar el modo debug, que muestra las minas desde el principio
            bool fin = false; // para saber si ha terminado el juego
            bool win = false; // para saber si ha ganado
            bool explota = false; // para saber si ha perdido

            Tablero tablero = new Tablero(10, 15, 15); // se crea un tablero de 10x10 con 15 minas
            if (DEBUG) tablero.ActivaDebug(); // si el modo debug está activado, se muestran las minas desde el principio

            tablero.Render(explota); // se renderiza el tablero sin mostrar las minas
            tablero.RenderGUI(); // se pinta el GUI
            while (!fin)
            {
                char input = LeeInput(); // se lee el input del usuario
                if (input == 'q') // si el usuario quiere salir
                {
                    fin = true; // se termina el juego
                }
                else
                {
                    fin = ProcesaInput(tablero, input); // se procesa el input y se actualiza el tablero
                    tablero.Render(explota); // se renderiza el tablero con el estado actualizado
                    tablero.RenderGUI(); // se pinta el GUI
                    win = tablero.Terminado(); // se comprueba si ha ganado
                    explota = fin; // si ha terminado porque ha hecho click en una mina, entonces ha perdido
                    fin = fin || win; // el juego termina si ha ganado o ha perdido
                }
            }
            // al salir del bucle, se muestra el resultado final
            if (win)
            {
                tablero.Render(explota); // se renderiza el tablero final
                Console.WriteLine("Enhorabuena, has ganado yayyy (^w^)9"); // se muestra mensaje de victoria
            }
            else if (explota)
            {
                tablero.Render(explota); // se renderiza el tablero final
                Console.ForegroundColor = ConsoleColor.Red; // se cambia el color de la consola a rojo para mostrar el mensaje de derrota
                Console.WriteLine("BOOOOOOOM!");
                Console.ResetColor(); // se resetea el color de la consola
                Console.WriteLine("Has perdido, nooo (T-T)"); // se muestra mensaje de derrota
            }
            else
            {
                Console.WriteLine("Has salido del juego, hasta la próxima! (OuO)/"); // se muestra mensaje de salida
            }
            }

        public static char LeeInput()
        {
            char d = ' ';
            string tecla = Console.ReadKey(true).Key.ToString();
            switch (tecla)
            {
                case "LeftArrow": d = 'l'; break;   // izquierda
                case "UpArrow": d = 'u'; break;     // arriba
                case "RightArrow": d = 'r'; break;  // derecha
                case "DownArrow": d = 'd'; break;   // abajo
                case "Spacebar": d = 'c'; break;    // click para destapar casilla
                case "Enter": d = 'x'; break;       // click para marcar/desmarcar con bandera
                case "Escape": d = 'q'; break;      // salir del juego

            }
            return d;
        }
        public static bool ProcesaInput(Tablero t, char c)
        {
            switch (c)
            {
                case 'l': t.MueveCursor(new Coor (-1, 0)); break; // izquierda
                case 'u': t.MueveCursor(new Coor (0, -1)); break; // arriba
                case 'r': t.MueveCursor(new Coor (1, 0)); break; // derecha
                case 'd': t.MueveCursor(new Coor (0, 1)); break; // abajo
                case 'c': return t.ClickCasilla();
                case 'x': t.MarcaMina(); break;
            }
            return false;
        }
    }
}
