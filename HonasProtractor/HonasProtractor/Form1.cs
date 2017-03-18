using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HonasProtractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            this.BackColor = Color.LightSlateGray;
            this.TransparencyKey = Color.LightSlateGray;

            this.TopMost = true;

            this.ResizeRedraw = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.FillRectangle(Brushes.LightSlateGray, e.ClipRectangle);
        }


        private Point lastLocation;
        private void protractorCtrl1_MouseDown(object sender, MouseEventArgs e)
        {
            lastLocation = e.Location;
        }

        private void protractorCtrl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void protractorCtrl1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }
    }
}
