using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalExplorer
{
    public partial class ClickFrame : Form
    {
        string mouseClick = "";

        private void MyMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseClick = "Left";
                    break;
                case MouseButtons.Right:
                    mouseClick = "Right";
                    break;
                case MouseButtons.Middle:
                    mouseClick = "Middle";
                    break;
            }
        }

        private void MyMouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseClick = "Left";
                    break;
                case MouseButtons.Right:
                    mouseClick = "Right";
                    break;
                case MouseButtons.Middle:
                    mouseClick = "Middle";
                    break;
            }
        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Transparent frame
        //    // 
        //    this.ClientSize = new System.Drawing.Size(500, 500);
        //    this.Cursor = System.Windows.Forms.Cursors.Cross;
        //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        //    this.Name = "Fractal";
        //    this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        //    this.Opacity = 0.83;
        //    this.ResumeLayout(false);
        //}

        private void ClickFrame_Load(object sender, EventArgs e)
        {
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyMouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyMouseDown);
        }

        public void InitFrame()
        {
            // this.InitializeComponent();
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyMouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyMouseDown);
        }
    }
}
