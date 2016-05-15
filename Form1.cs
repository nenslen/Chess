using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Chess
{
    public partial class frmChess : Form
    {
        public frmChess()
        {
            InitializeComponent();
        }

        /* IDEAS */
        // Castle moves
        // Side labels (1-8, a-h)
        // Scroll list box


        #region Variables
        ChessBoard brd = new ChessBoard();                   // The chess board
        public PictureBox[,] tiles = new PictureBox[8, 8];   // Visible squares on the form
        public PictureBox[] deadPieces = new PictureBox[32]; // Dead pieces
        Square lastSelected = null;                          // The last square to be selected
        Piece endPawn;                                       // A pawn that has reached the end

        public Color clrHover = Color.LightBlue;
        public Color clrSelected = Color.LightGreen;
        public Color clrPossibleMove = Color.LightGoldenrodYellow;

        public char currentPlayer = 'w'; // Whose turn it is

        public bool brdLoaded = false;   // If game board has been loaded

        public int moveNum = 0;          // How many moves have been played
        #endregion


        #region Events
        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            brd.reset();
            if (!brdLoaded)
            {
                addControls();
                brdLoaded = true;
                btnReset.Text = "Reset";
                btnSave.Enabled = btnLoad.Enabled = true;
            }
            updateBoard();
            currentPlayer = 'w';
            lblTurn.Text = "White's Turn";
            lstTurns.Items.Clear();
        }


        private void SquareClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            int sqRow = Convert.ToInt16(pb.Name.Substring(1, 1));
            int sqCol = Convert.ToInt16(pb.Name.Substring(2, 1));
            Square sq = brd.board[sqRow, sqCol];
            Piece pc = sq.currentPiece;
            List<Square> moves = new List<Square>();


            // Checks if there was a piece on clicked square
            if (pc == null && sq.possibleMove == false) { return; }


            #region Moving Piece
            if (sq.possibleMove == true)
            {
                // Deselectes last square and possible moves
                if (lastSelected != null)
                {
                    brd.board[lastSelected.row, lastSelected.col].selected = false;
                    moves = brd.possibleMoves(lastSelected, lastSelected.currentPiece);

                    for (int i = 0; i < moves.Count(); i++)
                    {
                        int row = moves[i].row;
                        int col = moves[i].col;

                        brd.board[row, col].possibleMove = false;
                    }
                }


                // Performs the turn and updates the board
                lstTurns.Items.Add(++moveNum + ". " + brd.turn(lastSelected, sq, lastSelected.currentPiece));
                updateBoard();


                // Checks for pawn reaching the end
                pawnCheck(sq.currentPiece);


                lastSelected = null;


                // Swaps turns
                if (currentPlayer == 'b') { currentPlayer = 'w'; lblTurn.Text = "White's Turn"; }
                else { currentPlayer = 'b'; lblTurn.Text = "Black's Turn"; }
                return;
            }
            #endregion

            #region Piece Selection
            if (pc.player == currentPlayer)
            {
                // Deselectes last square and possible moves
                if (lastSelected != null)
                {
                    brd.board[lastSelected.row, lastSelected.col].selected = false;
                    moves = brd.possibleMoves(lastSelected, lastSelected.currentPiece);

                    for (int i = 0; i < moves.Count(); i++)
                    {
                        int row = moves[i].row;
                        int col = moves[i].col;

                        brd.board[row, col].possibleMove = false;
                    }
                }


                // Selects new square
                sq.selected = true;
                lastSelected = sq;

                // Shows possible moves for that piece
                moves = brd.possibleMoves(sq, pc);

                for(int i = 0; i < moves.Count(); i++)
                {
                    int row = moves[i].row;
                    int col = moves[i].col;

                    brd.board[row, col].possibleMove = true;
                }

                updateBoard();

                return;
            }
            #endregion

            updateBoard();
        }
        

        private void SquareEnter(object sender, EventArgs e)
        {
            PictureBox sq = sender as PictureBox;
            sq.BackColor = clrHover;

            int sqRow = Convert.ToInt16(sq.Name.Substring(1, 1));
            int sqCol = Convert.ToInt16(sq.Name.Substring(2, 1));


            // Shows the current piece on the square
            if (brd.board[sqRow, sqCol].currentPiece != null)
            {
                lblCurrentSquare.Text = "Piece on: " + brd.board[sqRow, sqCol].location + ": " + brd.board[sqRow, sqCol].currentPiece.player + brd.board[sqRow, sqCol].currentPiece.type + ", ID: " + brd.board[sqRow, sqCol].currentPiece.id;
            }
            else
            {
                lblCurrentSquare.Text = "Null";
            }
        }

        
        private void SquareLeave(object sender, EventArgs e)
        {
            PictureBox sq = sender as PictureBox;

            int row = 0;
            int col = 0;

            row = Convert.ToInt16(sq.Name.Substring(1, 1));
            col = Convert.ToInt16(sq.Name.Substring(2, 1));

            if (brd.board[row, col].selected)
            {
                sq.BackColor = clrSelected;
            }
            else if (brd.board[row, col].possibleMove)
            {
                sq.BackColor = clrPossibleMove;
            }
            else
            {
                sq.BackColor = brd.board[row, col].clrDefault;
            }
        }


        private void DeadSquareClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            int pcIndex = Convert.ToInt16(pb.Name.Substring(1)) - 1; // Gets ID of clicked piece
            Piece pc = brd.pieces[pcIndex]; // The resurrected piece

            // Checks if a valid square was clicked
            if (pc.isAlive) { return; }


            // Kill the pawn
            brd.pieces[endPawn.id].isAlive = false;


            // Put the clicked piece on that square
            pc.row = endPawn.row;
            pc.col = endPawn.col;
            pc.isAlive = true;
            brd.board[endPawn.row, endPawn.col].currentPiece = pc;


            // Adds turn description
            lstTurns.Items.Add(++moveNum + ". " + endPawn.player + endPawn.type + " @ " + brd.board[endPawn.row, endPawn.col].location + " = " + pc.player + pc.type);


            // Swap turns
            if (currentPlayer == 'b') { currentPlayer = 'w'; lblTurn.Text = "White's Turn"; }
            else { currentPlayer = 'b'; lblTurn.Text = "Black's Turn"; }


            // Deselect all dead pieces
            for (int i = 0; i < 32; i++) { deadPieces[i].Enabled = false; deadPieces[i].BackColor = Color.DarkGray; }


            // Re-enable main board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pb = tiles[i, j];
                    pb.Enabled = true;
                }
            }


            endPawn = null;

            updateBoard();
        }


        private void DeadSquareEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = clrHover;
        }


        private void DeadSquareLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            pb.BackColor = Color.DarkGray;

            if (Convert.ToInt16(pb.Name.Substring(1)) <= 16 && currentPlayer == 'b')
            {
                pb.BackColor = clrPossibleMove;
            }

            if (Convert.ToInt16(pb.Name.Substring(1)) > 16 && currentPlayer == 'w')
            {
                pb.BackColor = clrPossibleMove;
            }


        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveGame();
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadGame();
        }
        #endregion


        #region Methods
        // Updates the board pieces and square colors
        private void updateBoard()
        {
            PictureBox pb;
            Square sq;
            Piece pc;


            // Updates backcolors
            for (int i = 0; i < 8; i++) // 8 Rows
            {
                for (int j = 0; j < 8; j++) // 8 Squares in each row
                {
                    pb = tiles[i, j];     // Visible square on form
                    sq = brd.board[i, j]; // Square on the board
                    pc = sq.currentPiece; // Piece on the square
                    

                    // Clears pieces
                    pb.Image = Image.FromFile("Pieces/blank.png");


                    // Sets backcolors
                    pb.BackColor = sq.clrDefault;
                    if (sq.possibleMove) { pb.BackColor = clrPossibleMove; }
                    if (sq.selected) { pb.BackColor = clrSelected; }
                }
            }


            // Updates all pieces
            for (int i = 0; i < 32; i++)
            {
                pc = brd.pieces[i];

                if (pc.isAlive)
                {
                    pb = tiles[pc.row, pc.col];
                    pb.Image = Image.FromFile("Pieces/" + pc.player + pc.type + ".png");
                    sq = brd.board[pc.row, pc.col];
                    sq.currentPiece = brd.pieces[i];

                    pb = deadPieces[i];
                    pb.Image = Image.FromFile("Pieces/blank.png");
                }
                else
                {
                    pb = deadPieces[i];
                    pb.Image = Image.FromFile("Pieces/" + pc.player + pc.type + ".png");
                }
            }
        }


        // Adds panels and buttons to form
        private void addControls()
        {
            // Game Board
            for (int i = 0; i < 8; i++) // 8 Rows
            {
                for (int j = 0; j < 8; j++) // 8 Squares in each row
                {
                    PictureBox sq = new PictureBox();

                    sq.Name = "s" + i.ToString() + j.ToString();
                    sq.Left = j * 40;
                    sq.Top = i * 40;
                    sq.Size = new Size(40, 40);
                    sq.SizeMode = PictureBoxSizeMode.StretchImage;
                    sq.BackColor = brd.board[i, j].clrDefault;
                    
                    sq.Click += new EventHandler(SquareClick);
                    sq.MouseEnter += new EventHandler(SquareEnter);
                    sq.MouseLeave += new EventHandler(SquareLeave);
                    sq.Enabled = true;
                    sq.BorderStyle = BorderStyle.FixedSingle;
                    pnlBoard.Controls.Add(sq);
                    tiles[i, j] = sq;
                }
            }

            int x = 0;

            // Dead pieces
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox sq = new PictureBox();

                    x++;
                    sq.Name = "d" + x.ToString(); ;
                    sq.Left = j * 40;
                    sq.Top = i * 40;
                    sq.Size = new Size(40, 40);
                    sq.SizeMode = PictureBoxSizeMode.StretchImage;
                    sq.BackColor = Color.DarkGray;

                    sq.Click += new EventHandler(DeadSquareClick);
                    sq.MouseEnter += new EventHandler(DeadSquareEnter);
                    sq.MouseLeave += new EventHandler(DeadSquareLeave);
                    sq.Enabled = false;
                    sq.BorderStyle = BorderStyle.FixedSingle;
                    pnlDeadPieces.Controls.Add(sq);
                    deadPieces[x - 1] = sq;
                }
            }
        }


        // Saves a game
        private void saveGame()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files | *.txt";
            sfd.DefaultExt = "txt";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                File.Delete(sfd.FileName);
                StreamWriter SW = File.CreateText(sfd.FileName);

                string line = "";


                // Saves current turn
                SW.WriteLine(currentPlayer);


                // Saves each piece
                for (int i = 0; i <= 31; i++)
                {
                    Piece pc = brd.pieces[i];
                    line += pc.id + "," + pc.row + "," + pc.col + "," + pc.firstMove + "," + pc.isAlive + "," + pc.player + "," + pc.type;
                    SW.Write(line + "\r\n");
                    line = "";
                }


                // Saves log
                for(int i = 0; i < lstTurns.Items.Count; i++)
                {
                    SW.Write(lstTurns.Items[i].ToString() + "\r\n");
                }


                SW.Close();
            }
        }


        // Loads a game
        private void loadGame()
        {
            PictureBox pb;
            Square sq;


            // Gets file to load from user
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files | *.txt";
            ofd.DefaultExt = "txt";
            ofd.ShowDialog();

            
            if (ofd.FileName != "")
            {
                try
                {
                    StreamReader SR = new StreamReader(ofd.FileName);

                    string[] line = new String[6];

                    

                    // Clears old pieces off the board
                    brd.reset();
                    for (int i = 0; i < 8; i++) // 8 Rows
                    {
                        for (int j = 0; j < 8; j++) // 8 Squares in each row
                        {
                            pb = tiles[i, j];     // Visible square on form
                            sq = brd.board[i, j]; // Square on the board

                            pb.Image = Image.FromFile("Pieces/blank.png");
                            sq.currentPiece = null;
                        }
                    }


                    // Loads current turn
                    currentPlayer = Convert.ToChar(SR.ReadLine());
                    if (currentPlayer == 'b') { lblTurn.Text = "Black's Turn"; }
                    else { lblTurn.Text = "White's Turn"; }


                    // Loads pieces
                    for (int i = 0; i < 32; i++)
                    {
                        line = SR.ReadLine().Split(',');

                        Piece pc = new Piece(1, 1, 1, 'z', 'z'); // Used to set the current piece on the board

                        pc.id = brd.pieces[i].id = Convert.ToInt16(line[0]);
                        pc.row = brd.pieces[i].row = Convert.ToInt16(line[1]);
                        pc.col = brd.pieces[i].col = Convert.ToInt16(line[2]);
                        pc.firstMove = brd.pieces[i].firstMove = Convert.ToBoolean(line[3]);
                        pc.isAlive = brd.pieces[i].isAlive = Convert.ToBoolean(line[4]);
                        pc.player = brd.pieces[i].player = Convert.ToChar(line[5]);
                        pc.type = brd.pieces[i].type = Convert.ToChar(line[6]);

                        if (pc.isAlive) { brd.board[pc.row, pc.col].currentPiece = pc; }
                    }

                    moveNum = 0;

                    // Loads log
                    string logLine;
                    lstTurns.Items.Clear();
                    while ((logLine = SR.ReadLine()) != null)
                    {
                        lstTurns.Items.Add(logLine);
                        moveNum++;
                    }


                    SR.Close();
                    updateBoard();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error loading file.\n\n" + e.ToString());
                    brd.reset();
                    if (!brdLoaded)
                    {
                        addControls();
                        brdLoaded = true;
                        btnReset.Text = "Reset";
                        btnSave.Enabled = btnLoad.Enabled = true;
                    }
                    updateBoard();
                    currentPlayer = 'w';
                    lblTurn.Text = "White's Turn";
                    lstTurns.Items.Clear();
                }
            }
        }


        // Checks for pawn reaching the other side
        private void pawnCheck(Piece pc)
        {
            // Checks if a piece has reached the end of the board
            if (pc.row == 0 && currentPlayer == 'w' && pc.type == 'P' ||
                pc.row == 7 && currentPlayer == 'b' && pc.type == 'P')
            {
                // Enables dead piece selection
                if(currentPlayer == 'w')
                {
                    for (int i = 16; i < 32; i++) { deadPieces[i].Enabled = true; deadPieces[i].BackColor = clrPossibleMove; }
                    for (int i = 0; i < 16; i++) { deadPieces[i].Enabled = false; deadPieces[i].BackColor = Color.DarkGray; }
                }
                else
                {
                    for (int i = 0; i < 16; i++) { deadPieces[i].Enabled = true; deadPieces[i].BackColor = clrPossibleMove; }
                    for (int i = 16; i < 32; i++) { deadPieces[i].Enabled = false; deadPieces[i].BackColor = Color.DarkGray; }
                }


                // Disables main board
                PictureBox pb;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        pb = tiles[i, j];
                        pb.Enabled = false;
                    }
                }


                // Swaps current player
                if (currentPlayer == 'b') { currentPlayer = 'w'; lblTurn.Text = "White's Turn"; }
                else { currentPlayer = 'b'; lblTurn.Text = "Black's Turn"; }


                endPawn = pc;
            }
        }
        #endregion



        private void btnTest_Click(object sender, EventArgs e)
        {

        }
    }
}
