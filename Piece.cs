using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Piece
    {
        public int id;         // 1 - 24
        public int row;        // 0 - 7
        public int col;        // 0 - 7
        public char type;      // R, N, B, Q, K, P, Z
        public char player;    // b or w
        public bool isAlive;   // If the piece is in use
        public bool firstMove; // For pawns, rooks, and kings special first moves

        // Constructor
        public Piece(int _id, int _row, int _col, char _type, char _player)
        {
            id = _id;
            row = _row;
            col = _col;
            type = _type;
            player = _player;
            isAlive = true;
            firstMove = true;
        }

    }
}
