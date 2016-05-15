using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
    class ChessBoard
    {
        public Square[,] board = new Square[8,8];      // 8x8 Game board (a1 - h8)
        public List<Piece> pieces = new List<Piece>(); // Holds game pieces


        // Default constructor
        public ChessBoard()
        {
            reset();   
        }


        // Sets a piece on a square
        public void setSquare(int row, int col, Piece pc)
        {
            board[row, col].currentPiece = pc;
        }


        // Gets a piece on a square
        public Piece getSquare(int row, int col)
        {
            return board[row, col].currentPiece;
        }


        // Resets board to default state
        public void reset()
        {
            Array.Clear(board, 0, board.Length);
            pieces.Clear();

            createPieces();
            createSquares();
            setPieces();
        }


        // Creates game pieces
        private void createPieces()
        {
            int i = 0;

            // Black top row
            pieces.Add(new Piece(i++, 0, 0, 'R', 'b'));
            pieces.Add(new Piece(i++, 0, 1, 'N', 'b'));
            pieces.Add(new Piece(i++, 0, 2, 'B', 'b'));
            pieces.Add(new Piece(i++, 0, 3, 'Q', 'b'));
            pieces.Add(new Piece(i++, 0, 4, 'K', 'b'));
            pieces.Add(new Piece(i++, 0, 5, 'B', 'b'));
            pieces.Add(new Piece(i++, 0, 6, 'N', 'b'));
            pieces.Add(new Piece(i++, 0, 7, 'R', 'b'));

            // Black pawns
            pieces.Add(new Piece(i++, 1, 0, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 1, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 2, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 3, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 4, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 5, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 6, 'P', 'b'));
            pieces.Add(new Piece(i++, 1, 7, 'P', 'b'));

            // White pawns
            pieces.Add(new Piece(i++, 6, 0, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 1, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 2, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 3, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 4, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 5, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 6, 'P', 'w'));
            pieces.Add(new Piece(i++, 6, 7, 'P', 'w'));

            // White bottom row
            pieces.Add(new Piece(i++, 7, 0, 'R', 'w'));
            pieces.Add(new Piece(i++, 7, 1, 'N', 'w'));
            pieces.Add(new Piece(i++, 7, 2, 'B', 'w'));
            pieces.Add(new Piece(i++, 7, 3, 'Q', 'w'));
            pieces.Add(new Piece(i++, 7, 4, 'K', 'w'));
            pieces.Add(new Piece(i++, 7, 5, 'B', 'w'));
            pieces.Add(new Piece(i++, 7, 6, 'N', 'w'));
            pieces.Add(new Piece(i++, 7, 7, 'R', 'w'));
        }


        // Creates board squares
        private void createSquares()
        {
            char col = 'z';
            bool white = true;

            // Adds 64 squares
            for (int i = 0; i < 8; i++) // 8 Rows
            {
                for (int j = 0; j < 8; j++) // 8 Squares in each row
                {
                    Square sq = new Square();

                    switch (j)
                    {
                        case 0:
                            col = 'a';
                            break;
                        case 1:
                            col = 'b';
                            break;
                        case 2:
                            col = 'c';
                            break;
                        case 3:
                            col = 'd';
                            break;
                        case 4:
                            col = 'e';
                            break;
                        case 5:
                            col = 'f';
                            break;
                        case 6:
                            col = 'g';
                            break;
                        case 7:
                            col = 'h';
                            break;
                    }

                    sq.row = i;
                    sq.col = j;
                    sq.location = col.ToString() + (8 - i).ToString(); /////////

                    if (white) { sq.clrDefault = Color.LightGray; white = false; }
                    else { sq.clrDefault = Color.DarkGray; white = true; }

                    board[i, j] = sq;
                }
                white = !white;
            }
        }


        // Sets pieces on board
        private void setPieces()
        {
            Piece pc;

            for (int i = 0; i < pieces.Count(); i++)
            {
                pc = pieces[i];
                board[pc.row, pc.col].currentPiece = pc;
            }
        }


        // Returns a list of possible moves
        public List<Square> possibleMoves(Square sq, Piece pc)
        {
            List<Square> moves = new List<Square>();
            Piece currPiece;


            #region Black Pawns
            if (pc.player == 'b' && pc.type == 'P')
            {
                int r;
                int c;

                #region Diagonal Squares
                // Only allows these moves if there are enemies there

                // Bottom left
                r = pc.row + 1;
                c = pc.col - 1;
                if (r <= 7 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece != null && currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Bottom Right
                r = pc.row + 1;
                c = pc.col + 1;
                if (r <= 7 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece != null && currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }
                #endregion

                #region Bottom Squares

                // Square in front
                r = pc.row + 1;
                c = pc.col;
                if (r <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null) { moves.Add(board[r, c]); }
                    else { return moves; }
                }

                // 2nd Square if it's the first move
                if (pc.firstMove)
                {
                    r = pc.row + 2;
                    c = pc.col;
                    if (r <= 7)
                    {
                        currPiece = board[r, c].currentPiece;
                        if (currPiece == null) { moves.Add(board[r, c]); }
                    }
                }
                #endregion
            }
            #endregion


            #region White Pawns
            if (pc.player == 'w' && pc.type == 'P')
            {
                int r;
                int c;

                #region Diagonal Squares
                // Only allows these moves if there are enemies there

                // Top left
                r = pc.row - 1;
                c = pc.col - 1;
                if (r >= 0 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece != null && currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Top Right
                r = pc.row - 1;
                c = pc.col + 1;
                if (r >= 0 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece != null && currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }
                #endregion

                #region Top Squares
                
                // Square in front
                r = pc.row - 1;
                c = pc.col;
                if (r >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null) { moves.Add(board[r, c]); }
                    else { return moves; }
                }

                // 2nd Square if it's the first move
                if (pc.firstMove)
                {
                    r = pc.row - 2;
                    c = pc.col;
                    if (r >= 0)
                    {
                        currPiece = board[r, c].currentPiece;
                        if (currPiece == null) { moves.Add(board[r, c]); }
                    }
                }
                int j;
                #endregion
            }
            #endregion


            #region Rooks
            if (pc.type == 'R')
            {
                #region Left Squares
                for (int i = pc.col; i > 0; i--)
                {
                    currPiece = board[sq.row, i - 1].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[sq.row, i - 1]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                #region Right Squares
                for (int i = pc.col; i < 7; i++)
                {
                    currPiece = board[sq.row, i + 1].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[sq.row, i + 1]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                #region Top Squares
                for (int i = pc.row; i > 0; i--)
                {
                    currPiece = board[i - 1, pc.col].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[i - 1, pc.col]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                #region Bottom Squares
                for (int i = pc.row; i < 7; i++)
                {
                    currPiece = board[i + 1, pc.col].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[i + 1, pc.col]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion
            }
            #endregion


            #region Knights
            if (pc.type == 'N')
            {
                int r;
                int c;

                // Top left
                r = pc.row - 2;
                c = pc.col - 1;
                if (r >= 0 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Top Right
                r = pc.row - 2;
                c = pc.col + 1;
                if (r >= 0 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Middle Left Top
                r = pc.row - 1;
                c = pc.col - 2;
                if (r >= 0 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Middle Left Bottom
                r = pc.row + 1;
                c = pc.col - 2;
                if (r <= 7 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Middle Right Top
                r = pc.row - 1;
                c = pc.col + 2;
                if (r >= 0 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Middle Right Bottom
                r = pc.row + 1;
                c = pc.col + 2;
                if (r <= 7 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Bottom left
                r = pc.row + 2;
                c = pc.col - 1;
                if (r <= 7 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Bottom Right
                r = pc.row + 2;
                c = pc.col + 1;
                if (r <= 7 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }
            }
            #endregion


            #region Bishops
            if (pc.type == 'B')
            {
                int r;
                int c;

                #region Top Left Diagonal
                r = pc.row - 1;
                c = pc.col - 1;
                while (r >= 0 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r--;
                    c--;
                }
                #endregion

                #region Top Right Diagonal
                r = pc.row - 1;
                c = pc.col + 1;
                while (r >= 0 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r--;
                    c++;
                }
                #endregion

                #region Bottom Left Diagonal
                r = pc.row + 1;
                c = pc.col - 1;
                while (r <= 7 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r++;
                    c--;
                }
                #endregion

                #region Bottom Right Diagonal
                r = pc.row + 1;
                c = pc.col + 1;
                while (r <= 7 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r++;
                    c++;
                }
                #endregion
            }
            #endregion


            #region Queens
            if (pc.type == 'Q')
            {
                #region Left Squares
                for (int i = pc.col; i > 0; i--)
                {
                    currPiece = board[sq.row, i - 1].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[sq.row, i - 1]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                #region Right Squares
                for (int i = pc.col; i < 7; i++)
                {
                    currPiece = board[sq.row, i + 1].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[sq.row, i + 1]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                #region Top Squares
                for (int i = pc.row; i > 0; i--)
                {
                    currPiece = board[i - 1, pc.col].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[i - 1, pc.col]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                #region Bottom Squares
                for (int i = pc.row; i < 7; i++)
                {
                    currPiece = board[i + 1, pc.col].currentPiece;

                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[i + 1, pc.col]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }
                }
                #endregion

                int r;
                int c;

                #region Top Left Diagonal
                r = pc.row - 1;
                c = pc.col - 1;
                while (r >= 0 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r--;
                    c--;
                }
                #endregion

                #region Top Right Diagonal
                r = pc.row - 1;
                c = pc.col + 1;
                while (r >= 0 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r--;
                    c++;
                }
                #endregion

                #region Bottom Left Diagonal
                r = pc.row + 1;
                c = pc.col - 1;
                while (r <= 7 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r++;
                    c--;
                }
                #endregion

                #region Bottom Right Diagonal
                r = pc.row + 1;
                c = pc.col + 1;
                while (r <= 7 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player)
                    {
                        moves.Add(board[r, c]);

                        if (currPiece != null)
                        {
                            if (currPiece.player != pc.player) { break; } // Enemy piece in the way
                        }
                    }
                    else
                    {
                        break; // Friendly piece in the way
                    }

                    r++;
                    c++;
                }
                #endregion
            }
            #endregion


            #region Kings
            if (pc.type == 'K')
            {
                int r;
                int c;

                #region Top
                // left
                r = pc.row - 1;
                c = pc.col - 1;
                if (r >= 0 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Right
                r = pc.row - 1;
                c = pc.col + 1;
                if (r >= 0 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Middle
                r = pc.row - 1;
                c = pc.col;
                if (r >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }
                #endregion

                #region Middle
                // Left
                r = pc.row;
                c = pc.col - 1;
                if (c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Right
                r = pc.row;
                c = pc.col + 1;
                if (c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }
                #endregion

                #region Bottom
                // left
                r = pc.row + 1;
                c = pc.col - 1;
                if (r <= 7 && c >= 0)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Right
                r = pc.row + 1;
                c = pc.col + 1;
                if (r <= 7 && c <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }

                // Middle
                r = pc.row + 1;
                c = pc.col;
                if (r <= 7)
                {
                    currPiece = board[r, c].currentPiece;
                    if (currPiece == null || currPiece.player != pc.player) { moves.Add(board[r, c]); }
                }
                #endregion

                #region Castle
                // Black
                if (pc.player == 'b')
                {
                    if (pc.firstMove && pieces[0].firstMove && pieces[0].isAlive && board[0, 1].currentPiece == null && board[0, 2].currentPiece == null && board[0, 3].currentPiece == null)
                    {
                        moves.Add(board[0, 2]);
                    }
                    if (pc.firstMove && pieces[7].firstMove && pieces[7].isAlive && board[0, 5].currentPiece == null && board[0, 6].currentPiece == null)
                    {
                        moves.Add(board[0, 6]);
                    }
                }
                else // White
                {
                    if (pc.firstMove && pieces[24].firstMove && pieces[24].isAlive && board[7, 1].currentPiece == null && board[7, 2].currentPiece == null && board[7, 3].currentPiece == null)
                    {
                        moves.Add(board[7, 2]);
                    }
                    if (pc.firstMove && pieces[31].firstMove && pieces[31].isAlive && board[7, 5].currentPiece == null && board[7, 6].currentPiece == null)
                    {
                        moves.Add(board[7, 6]);
                    }
                }
                #endregion
            }
            #endregion


            return moves;
        }


        // Performs a turn, returns what happened
        public string turn(Square srcSq, Square destSq, Piece pc)
        {
            string action = "";

            action = pc.player.ToString() + pc.type.ToString() + " @ " + srcSq.location + " ";
            
            
            // Removes enemy from board
            if (destSq.currentPiece != null)
            {
                action += " x  " + destSq.currentPiece.player.ToString() + destSq.currentPiece.type.ToString();
                action += " @ " + destSq.location;
                pieces[destSq.currentPiece.id].isAlive = false;
            }
            else
            {
                action += " -> " + destSq.location;
            }
            
            srcSq.currentPiece = null;
            destSq.currentPiece = pc;
            pc.col = destSq.col;
            pc.row = destSq.row;
            pc.firstMove = false;

            return action;
            

            
        }

    }
}
