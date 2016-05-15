namespace Chess
{
    partial class frmChess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCurrentSquare = new System.Windows.Forms.Label();
            this.lblTurn = new System.Windows.Forms.Label();
            this.pnlBoard = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lstTurns = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.pnlDeadPieces = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblCurrentSquare
            // 
            this.lblCurrentSquare.AutoSize = true;
            this.lblCurrentSquare.Location = new System.Drawing.Point(336, 10);
            this.lblCurrentSquare.Name = "lblCurrentSquare";
            this.lblCurrentSquare.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentSquare.TabIndex = 0;
            this.lblCurrentSquare.Text = "label1";
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurn.ForeColor = System.Drawing.Color.Black;
            this.lblTurn.Location = new System.Drawing.Point(334, 306);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(120, 26);
            this.lblTurn.TabIndex = 4;
            this.lblTurn.Text = "White\'s Turn";
            // 
            // pnlBoard
            // 
            this.pnlBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBoard.Location = new System.Drawing.Point(6, 10);
            this.pnlBoard.Name = "pnlBoard";
            this.pnlBoard.Size = new System.Drawing.Size(322, 322);
            this.pnlBoard.TabIndex = 5;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReset.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(339, 41);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(115, 56);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Start";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(359, 253);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lstTurns
            // 
            this.lstTurns.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTurns.FormattingEnabled = true;
            this.lstTurns.ItemHeight = 16;
            this.lstTurns.Location = new System.Drawing.Point(464, 12);
            this.lstTurns.Name = "lstTurns";
            this.lstTurns.Size = new System.Drawing.Size(221, 484);
            this.lstTurns.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(339, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(115, 56);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnLoad.Enabled = false;
            this.btnLoad.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(339, 165);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(115, 56);
            this.btnLoad.TabIndex = 11;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pnlDeadPieces
            // 
            this.pnlDeadPieces.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDeadPieces.Location = new System.Drawing.Point(6, 339);
            this.pnlDeadPieces.Name = "pnlDeadPieces";
            this.pnlDeadPieces.Size = new System.Drawing.Size(322, 160);
            this.pnlDeadPieces.TabIndex = 12;
            // 
            // frmChess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(697, 505);
            this.Controls.Add(this.pnlDeadPieces);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstTurns);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pnlBoard);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.lblCurrentSquare);
            this.Name = "frmChess";
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentSquare;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Panel pnlBoard;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ListBox lstTurns;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Panel pnlDeadPieces;

    }
}

