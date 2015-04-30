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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_walkMaze = new System.Windows.Forms.Button();
            this.panelMaze.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMaze
            // 
            this.panelMaze.Controls.Add(this.textBox1);
            this.panelMaze.Controls.Add(this.label3);
            this.panelMaze.Controls.Add(this.label2);
            this.panelMaze.Controls.Add(this.label1);
            this.panelMaze.Controls.Add(this.btn_walkMaze);
            this.panelMaze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaze.Location = new System.Drawing.Point(0, 0);
            this.panelMaze.Name = "panelMaze";
            this.panelMaze.Size = new System.Drawing.Size(844, 618);
            this.panelMaze.TabIndex = 0;
            this.panelMaze.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMaze_Paint);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(223, 577);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(44, 21);
            this.textBox1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 12F);
            this.label3.Location = new System.Drawing.Point(622, 578);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "（0,0）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F);
            this.label2.Location = new System.Drawing.Point(499, 578);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "当前单元格坐标：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F);
            this.label1.Location = new System.Drawing.Point(429, 578);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "前进";
            // 
            // btn_walkMaze
            // 
            this.btn_walkMaze.Location = new System.Drawing.Point(314, 571);
            this.btn_walkMaze.Name = "btn_walkMaze";
            this.btn_walkMaze.Size = new System.Drawing.Size(85, 30);
            this.btn_walkMaze.TabIndex = 0;
            this.btn_walkMaze.Text = "开始行走";
            this.btn_walkMaze.UseVisualStyleBackColor = true;
            this.btn_walkMaze.Click += new System.EventHandler(this.btn_walkMaze_Click);
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
            this.panelMaze.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMaze;
        private System.Windows.Forms.Button btn_walkMaze;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

