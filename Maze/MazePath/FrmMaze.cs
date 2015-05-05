using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazePath
{
    public delegate void DrawArrowHandler(int xNum,int yNum,string strArrow,bool bReturnGrid=false);
    public delegate void UpdateCtrlHandler(int xNum,int yNum,bool bReturnGrid);
    public partial class FrmMaze : Form
    {
        public FrmMaze()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint,
                          true);
            this.UpdateStyles();
        }
    }
}
