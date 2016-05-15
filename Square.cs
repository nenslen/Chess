using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    class Square
    {
        public Color clrDefault;          // The default color of the square
        public bool selected = false;     // If the square is selected
        public bool possibleMove = false; // If the square is a possible move for a piece
        public Piece currentPiece = null; // The current piece on the square
        public int row;                   // Square's row
        public int col;                   // Square's column
        public string location;           // a1 - h8


        public Square()
        {
            clrDefault = Color.Brown;
        }

        public Square(Color defaultColor, int _row, int _col)
        {
            clrDefault = defaultColor;
            row = _row;
            col = _col;
        }
    }
}
