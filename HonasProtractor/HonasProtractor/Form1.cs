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

        string extendstr = "▽";
        string collapsestr = "△";
        bool bcollapsed = false;

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
            //closebtn.Width = 50;
            //closebtn.Location = new Point(protractorCtrl1.Location.X, pictureBox1.Location.Y);
            //closebtn.Text = extendstr;

            this.Height = pictureBox1.Height + (protractorCtrl1.Width / 2);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            if (bcollapsed == false)
            {
                //closebtn.Text = extendstr;
                pictureBox1.BackgroundImage = null;
                pictureBox1.Visible = false;
            }
            else
            {
                //closebtn.Text = collapsestr;
                pictureBox1.BackgroundImage = Properties.Resources.HonasLogo;
                pictureBox1.Visible = true;
            }
            bcollapsed = !bcollapsed;
        }

        private void widthToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tbChangeWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int bresult = -1;
                int.TryParse(tbChangeWidth.Text, out bresult);

                if (bresult < 0)
                    return;

                this.Width = bresult;
                this.Height = pictureBox1.Height + bresult / 2;

                protractorCtrl1.Update();

                //closebtn.Location = new Point(protractorCtrl1.Location.X, protractorCtrl1.Location.Y + protractorCtrl1.Height);
            }
        }

        private void logoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bcollapsed == false)
            {
                //closebtn.Text = extendstr;
                pictureBox1.BackgroundImage = null;
                pictureBox1.Visible = false;
            }
            else
            {
                //closebtn.Text = collapsestr;
                pictureBox1.BackgroundImage = Properties.Resources.HonasLogo;
                pictureBox1.Visible = true;
            }
            bcollapsed = !bcollapsed;

            logoToolStripMenuItem.Checked = !bcollapsed;
        }
    }
}
