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
        #region 变量
        private int nLines =10;
        private int nRows = 10;
        private int nSingleGrid = 40;
        private List<Point> m_lstInitialForbidGrid = new List<Point>();
        /// <summary>
        /// 不允许行走的格子
        /// </summary>
        private List<Point> m_lstForbidGrid = new List<Point>();
        /// <summary>
        /// 记录走出迷宫的路线
        /// </summary>
        private List<Point> m_lstMazePath = new List<Point>();
        private const string CONST_DIRECTION_UP = "↑";
        private const string CONST_DIRECTION_RIGHT = "→";
        private const string CONST_DIRECTION_DOWN = "↓";
        private const string CONST_DIRECTION_LEFT = "←";
        private const int CONST_INT_MAX_WITH_HEIGHT = 400;
        private Graphics g;
        private Point pointStart;
        private BackgroundWorker worker = new BackgroundWorker();
        #endregion

        #region 构造函数与初始化
        public FrmMaze()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint,
                          true);
            this.UpdateStyles();
            worker.DoWork+=worker_DoWork;
            nSingleGrid = CONST_INT_MAX_WITH_HEIGHT / nRows;
            this.panelMaze.MouseClick+=panelMaze_MouseClick;
            g = this.panelMaze.CreateGraphics();
            m_lstInitialForbidGrid.Clear();
            m_lstInitialForbidGrid.Add(new Point(1, 1));
            m_lstInitialForbidGrid.Add(new Point(2, 1));
            m_lstInitialForbidGrid.Add(new Point(4, 1));
            m_lstInitialForbidGrid.Add(new Point(4, 2));
            m_lstInitialForbidGrid.Add(new Point(5, 2));
            m_lstInitialForbidGrid.Add(new Point(1, 3));
            m_lstInitialForbidGrid.Add(new Point(3, 3));
            m_lstInitialForbidGrid.Add(new Point(0, 4));
            m_lstInitialForbidGrid.Add(new Point(5, 4));
            m_lstInitialForbidGrid.Add(new Point(1, 5));
            m_lstInitialForbidGrid.Add(new Point(3, 5));
            m_lstInitialForbidGrid.Add(new Point(6, 0));
            m_lstInitialForbidGrid.Add(new Point(4, 8));
            m_lstInitialForbidGrid.Add(new Point(7, 5));
            m_lstInitialForbidGrid.Add(new Point(9, 3));
            m_lstInitialForbidGrid.Add(new Point(9, 5));
            m_lstInitialForbidGrid.Add(new Point(7, 7));
            m_lstInitialForbidGrid.Add(new Point(8, 3));
            m_lstInitialForbidGrid.Add(new Point(7, 3));
            m_lstInitialForbidGrid.Add(new Point(6, 4));
            m_lstInitialForbidGrid.Add(new Point(7, 1));
            m_lstInitialForbidGrid.Add(new Point(8, 7));
            m_lstInitialForbidGrid.Add(new Point(5, 7));
            m_lstInitialForbidGrid.Add(new Point(7, 9));
            m_lstInitialForbidGrid.Add(new Point(6, 9));
            m_lstInitialForbidGrid.Add(new Point(2, 7));
            m_lstInitialForbidGrid.Add(new Point(2, 9));
            m_lstInitialForbidGrid.Add(new Point(9, 1));
            m_lstInitialForbidGrid.Add(new Point(3, 6));
            m_lstInitialForbidGrid.Add(new Point(4, 4));
            m_lstInitialForbidGrid.Add(new Point(7, 8));
            m_lstInitialForbidGrid.Add(new Point(3, 8));
            InitalMaze();
        }
        /// <summary>
        /// 初始化迷宫
        /// </summary>
        private void InitalMaze()
        {
            m_lstForbidGrid.Clear();
            m_lstForbidGrid.AddRange(m_lstInitialForbidGrid);
        }
        #endregion

        #region 画迷宫
        /// <summary>
        /// 画迷宫的线条
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pointStart"></param>
        private void DrawLines(Graphics g, Point pointStart)
        {
            using (SolidBrush lineBrush = new SolidBrush(Color.Black))
            {
                using (Pen linePen = new Pen(lineBrush))
                {
                    for (int i = 0; i <= nLines; i++)
                    {
                        g.DrawLine(linePen, pointStart.X, pointStart.Y + i * nSingleGrid, pointStart.X + nRows * nSingleGrid, pointStart.Y + i * nSingleGrid);
                    }
                    for (int j = 0; j <= nRows; j++)
                    {
                        g.DrawLine(linePen, pointStart.X + j * nSingleGrid, pointStart.Y, pointStart.X + j * nSingleGrid, pointStart.Y + nRows * nSingleGrid);
                    }
                }
            }
        }
        /// <summary>
        /// 画迷宫禁走格子
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pointStart"></param>
        private void DrawForbidGrids(Graphics g, Point pointStart)
        {
            foreach (Point item in m_lstInitialForbidGrid)
            {
                if (item.Y < nLines && item.X < nRows)
                {
                    using (HatchBrush brush = new HatchBrush(HatchStyle.DashedUpwardDiagonal, Color.Green, Color.Black))
                    {
                        Rectangle rect = new Rectangle(pointStart.X + item.X * nSingleGrid, pointStart.Y + item.Y * nSingleGrid, nSingleGrid, nSingleGrid);
                        g.FillRectangle(brush, rect);
                    }
                }

            }
        }
        /// <summary>
        /// 在格子中画箭头
        /// </summary>
        /// <param name="xNum"></param>
        /// <param name="yNum"></param>
        /// <param name="strArrow"></param>
        /// <param name="bReturnGrid"></param>
        private void DrawArrow(int xNum, int yNum, string strArrow, bool bReturnGrid = false)
        {
            Color color = Color.Black;
            if (bReturnGrid)
                color = Color.Red;
            Font font = new Font("宋体", 20, FontStyle.Bold);
            SizeF size = g.MeasureString(strArrow, font);
            PointF printPoint = new PointF((float)(pointStart.X + xNum * nSingleGrid) + ((float)nSingleGrid - size.Width) / 2, (float)(pointStart.Y + yNum * nSingleGrid) + ((float)nSingleGrid - size.Height) / 2);
            using (Brush brush = new SolidBrush(color))
            {
                g.DrawString(strArrow, font, brush, printPoint);
            }
        }
        #endregion

        #region 事件处理
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int xNum = -1;//记录当前格子的横向坐标
            int yNum = 0;//记录当前格子的纵向坐标
            string strArrow = string.Empty;
            bool bReturnGrid = false;
            bool bGoOutOfMaze = true;
            while (xNum != nRows || yNum != nLines - 1)
            {
                if (!this.IsDisposed)
                this.Invoke(new UpdateCtrlHandler(UpdateCtrl), xNum, yNum, bReturnGrid);
                bReturnGrid = false;
                Thread.Sleep(100);
                //右走
                xNum++;
                if (CanWalk(xNum, yNum))
                {
                    if (!this.IsDisposed)
                    this.Invoke(new DrawArrowHandler(DrawArrow), --xNum, yNum, CONST_DIRECTION_RIGHT, false);
                    //可以走通
                    m_lstMazePath.Add(new Point(xNum, yNum));
                    xNum++;
                    continue;
                }
                else
                {
                    xNum--;
                }
                //下走
                yNum++;
                if (CanWalk(xNum, yNum))
                {
                    if (!this.IsDisposed)
                    this.Invoke(new DrawArrowHandler(DrawArrow), xNum, --yNum, CONST_DIRECTION_DOWN, false);
                    m_lstMazePath.Add(new Point(xNum, yNum));
                    yNum++;
                    continue;
                }
                else
                {
                    yNum--;
                }
                //左走
                xNum--;
                if (CanWalk(xNum, yNum))
                {
                    if (!this.IsDisposed)
                    this.Invoke(new DrawArrowHandler(DrawArrow), ++xNum, yNum, CONST_DIRECTION_LEFT, false);
                    m_lstMazePath.Add(new Point(xNum, yNum));
                    xNum--;
                    continue;
                }
                else
                {
                    xNum++;
                }
                //上走
                yNum--;
                if (CanWalk(xNum, yNum))
                {
                    if (!this.IsDisposed)
                    this.Invoke(new DrawArrowHandler(DrawArrow), xNum, ++yNum, CONST_DIRECTION_UP, false);
                    m_lstMazePath.Add(new Point(xNum, yNum));
                    yNum--;
                    continue;
                }
                else
                {
                    yNum++;
                }

                //均走不通，退格
                if (m_lstMazePath.Count > 1)
                {
                    //加入禁入单元格
                    if (m_lstForbidGrid.Where(item => item.X == xNum && item.Y == yNum).ToList().Count == 0)
                    {
                        m_lstForbidGrid.Add(new Point(xNum, yNum));
                    }
                    //从路径中删除
                    List<Point> lstPoint = m_lstMazePath.Where(item => item.X == xNum && item.Y == yNum).ToList();
                    if (lstPoint.Count > 0)
                    {
                        foreach (Point item in lstPoint)
                            m_lstMazePath.Remove(item);
                    }
                    //退回到前一个格子
                    Point pointPre = m_lstMazePath[m_lstMazePath.Count - 1];
                    string strConstArrow = string.Empty;
                    if (yNum - 1 == pointPre.Y && xNum == pointPre.X)
                        strConstArrow = CONST_DIRECTION_UP;
                    else if (xNum + 1 == pointPre.X && yNum == pointPre.Y)
                        strConstArrow = CONST_DIRECTION_RIGHT;
                    else if (xNum - 1 == pointPre.X && yNum == pointPre.Y)
                        strConstArrow = CONST_DIRECTION_LEFT;
                    else if (yNum + 1 == pointPre.Y && xNum == pointPre.X)
                        strConstArrow = CONST_DIRECTION_DOWN;
                    if(!this.IsDisposed)
                    this.Invoke(new DrawArrowHandler(DrawArrow), xNum, yNum, strConstArrow, true);
                    xNum = pointPre.X;
                    yNum = pointPre.Y;
                    bReturnGrid = true;
                    continue;
                }
                else
                {
                    if (!this.IsDisposed)
                    this.Invoke(new EventHandler(delegate { MessageBox.Show("无路可退，此为死迷宫！"); }));
                    bGoOutOfMaze = false;
                    break;
                }
            }
            if (bGoOutOfMaze)
            {
                if (!this.IsDisposed)
                this.Invoke(new EventHandler(delegate { MessageBox.Show("恭喜，从迷宫中成功走出!"); }));
            }
        }
        protected void panelMaze_MouseClick(object sender, MouseEventArgs e)
        {
            if(!worker.IsBusy)
            {
                Point panelPoint = panelMaze.PointToClient(this.PointToScreen(e.Location));
                int xNum = (panelPoint.X - pointStart.X) / nSingleGrid;
                int yNum = (panelPoint.Y - pointStart.Y) / nSingleGrid;
                if (yNum < nLines && xNum < nRows&&yNum>=0&&xNum>=0)
                {
                    List<Point> lstResult = m_lstInitialForbidGrid.Where(item => item.X == xNum && item.Y == yNum).ToList();
                    if (lstResult.Count > 0)
                    {
                        foreach (Point item in lstResult)
                            m_lstInitialForbidGrid.Remove(item);
                    }
                    else
                    {
                        Point newPoint = new Point(xNum, yNum);
                        m_lstInitialForbidGrid.Add(newPoint);
                        m_lstForbidGrid.Add(newPoint);
                    }
                    RefreshMaze();
                }
                base.OnMouseClick(e);
            }
        }

        private void panelMaze_Paint(object sender, PaintEventArgs e)
        {
            pointStart = new Point((this.Width - nRows * nSingleGrid) / 2, (this.Height - nLines * nSingleGrid) / 2);
            DrawLines(g, pointStart);
            DrawForbidGrids(g, pointStart);
            DrawArrow(-1, 0, CONST_DIRECTION_RIGHT);
            DrawArrow(nLines, nRows - 1, CONST_DIRECTION_RIGHT);
        }
        private void btn_walkMaze_Click(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                RefreshMaze();
                worker.RunWorkerAsync();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 更新界面控件值
        /// </summary>
        /// <param name="xNum"></param>
        /// <param name="yNum"></param>
        /// <param name="bReturnGrid"></param>
        private void UpdateCtrl(int xNum, int yNum, bool bReturnGrid)
        {
            label3.Text = string.Format("({0},{1})", xNum, yNum);
            if (bReturnGrid)
            {
                label1.ForeColor = Color.Red;
                label1.Text = "后退";
            }
            else
            {
                label1.ForeColor = Color.Black;
                label1.Text = "前进";
            }
        }

        /// <summary>
        /// 判断该格是否可走
        /// </summary>
        /// <param name="xNum"></param>
        /// <param name="yNum"></param>
        /// <returns></returns>
        private bool CanWalk(int xNum, int yNum)
        {
            List<Point> lstFind = m_lstForbidGrid.Where(item => item.X == xNum && item.Y == yNum).ToList();//查找是否存在进制的格子
            //判断是否为禁止的格子
            if (lstFind.Count == 0)
            {
                lstFind = m_lstMazePath.Where(item1 => item1.X == xNum && item1.Y == yNum).ToList();
                //判断是否为移走的格子
                if (lstFind.Count == 0)
                {
                    //判断是否已经超越迷宫的边界
                    if (xNum == nLines && yNum == nRows - 1)
                        return true;
                    else if ((xNum < 0 || xNum > nRows - 1 || yNum < 0 || yNum > nLines - 1))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 刷新界面
        /// </summary>
        private void RefreshMaze()
        {
            int nOrderNum = 10;
            if (!int.TryParse(textBox1.Text, out nOrderNum))
                nOrderNum = 10;
            nRows = nLines = nOrderNum;
            nSingleGrid = CONST_INT_MAX_WITH_HEIGHT / nOrderNum;
            m_lstForbidGrid.Clear();
            m_lstMazePath.Clear();
            this.panelMaze.Invalidate();
            InitalMaze();
        }
        #endregion
    }
}
