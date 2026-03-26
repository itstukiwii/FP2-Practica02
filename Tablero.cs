using System;
using Coordinates;

namespace Tablero
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
    }
}
