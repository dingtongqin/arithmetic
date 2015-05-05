namespace MazePath
{
    partial class FrmMaze
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
            this.panelMaze = new System.Windows.Forms.Panel();
            this.mazeControl1 = new Maze.MazeControl();
            this.panelMaze.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMaze
            // 
            this.panelMaze.Controls.Add(this.mazeControl1);
            this.panelMaze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaze.Location = new System.Drawing.Point(0, 0);
            this.panelMaze.Name = "panelMaze";
            this.panelMaze.Size = new System.Drawing.Size(844, 618);
            this.panelMaze.TabIndex = 0;
            // 
            // mazeControl1
            // 
            this.mazeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mazeControl1.Location = new System.Drawing.Point(0, 0);
            this.mazeControl1.Name = "mazeControl1";
            this.mazeControl1.Size = new System.Drawing.Size(844, 618);
            this.mazeControl1.TabIndex = 0;
            // 
            // FrmMaze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 618);
            this.Controls.Add(this.panelMaze);
            this.Name = "FrmMaze";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "迷宫算法";
            this.panelMaze.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMaze;
        private Maze.MazeControl mazeControl1;
    }
}

