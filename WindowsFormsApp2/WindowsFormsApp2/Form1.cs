using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int director = 100;
        
        public Form1()
        {
            InitializeComponent();

        }

        public void click_handler(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(newOne));
            t.Start();
        }

        public void newOne()
        {
            // 获取鼠标位置
            Point MOUSE = this.PointToClient(MousePosition);
            int x = MOUSE.X-(director/2);
            int y = MOUSE.Y-(director/2);

            // 通过随机数确定xy轴的方向和速度
            Random rd = new Random();
            int dirx = ((rd.Next()%2==0) ? 1 : -1);
            int diry = ((rd.Next()%2==0) ? 1 : -1);
            int spx = rd.Next(2,6);
            int spy = rd.Next(2,6);

            // 创建画笔、长方形和绘图对象
            Pen p = new Pen(Color.FromArgb(rd.Next() % 255, rd.Next() % 255, rd.Next() % 255), 2);
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            Rectangle rect = new Rectangle(x, y, director, director);

            // 改变泡泡位置，使其运动
            while (true)
            {
                // 清除本泡泡之前的图像
                this.Invalidate(new Rectangle(x - 2, y - 2, director + 4, director + 4));

                // 如果碰到边界，则改变运动方向
                if (x <= 0 || x+director >= ClientSize.Width)
                {
                    dirx *= -1;
                }
                if(y<=0 || y+director >= ClientSize.Height)
                {
                    diry *= -1;
                }
                x += dirx * spx;
                y += diry * spy;
                rect = new Rectangle(x, y, director, director);

                // 在新的位置绘制本泡泡，每20毫秒刷新一次
                g.DrawEllipse(p, rect);
                Thread.Sleep(20);
            }
        }
    }
}
