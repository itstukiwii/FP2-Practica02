
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
            bool fin = false;
            Tablero tablero = new Tablero(10, 15, 15); // se crea un tablero de 10x10 con 15 minas
            tablero.Render(true); // se renderiza el tablero sin mostrar las minas
            while (!fin)
            {
                char input = LeeInput(); // se lee el input del usuario
                fin = ProcesaInput(tablero, input); // se procesa el input y se actualiza el tablero
                tablero.Render(true); // se renderiza el tablero mostrando las minas si es necesario
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
                case 'q': return true;
            }
            return false;
        }
    }
}
