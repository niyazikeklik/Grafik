using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafik
{
    public partial class coklugrafik : Form
    {
        public coklugrafik()
        {

          //  Control.CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
        }
        public ArrayList secilenler = new ArrayList();
        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        void acilis()
        {
            int satir = 0, sutun = 0;
            for (int i = 0; i < secilenler.Count; i++)
            {
                grafik yeni = new grafik();
                yeni.TopLevel = false;
                yeni.Dock = DockStyle.Fill;
                yeni.FormBorderStyle = FormBorderStyle.None;
                yeni.Urunadi = secilenler[i].ToString();
                yeni.panel3.Visible = true;
                yeni.panel1.Visible = false;
                yeni.WindowState = FormWindowState.Normal;
                tableLayoutPanel1.Controls.Add(yeni, sutun, satir);
                sutun++;
                if (sutun == tableLayoutPanel1.ColumnCount)
                {
                    satir++;
                    sutun = 0;
                }
                yeni.Show();
            }
        }
            private void Form3_Load(object sender, EventArgs e)
        {
         
            if (secilenler.Count == 2)
            {
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 2;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 2));
                tableLayoutPanel1.ColumnStyles[0].Width = Screen.PrimaryScreen.Bounds.Width / 2;
                tableLayoutPanel1.ColumnStyles[1].Width = Screen.PrimaryScreen.Bounds.Width / 2;
            }
            else if (secilenler.Count <= 4)
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 2;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 2));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 2));
                tableLayoutPanel1.ColumnStyles[0].Width = Screen.PrimaryScreen.Bounds.Width / 2;
                tableLayoutPanel1.ColumnStyles[1].Width = Screen.PrimaryScreen.Bounds.Width / 2;
                tableLayoutPanel1.RowStyles[0].Height = Screen.PrimaryScreen.Bounds.Height / 2;
                tableLayoutPanel1.RowStyles[1].Height = Screen.PrimaryScreen.Bounds.Height / 2;

            }
            else if (secilenler.Count <= 6)
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 3));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 3));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 2));
                tableLayoutPanel1.ColumnStyles[0].Width = Screen.PrimaryScreen.Bounds.Width / 3;
                tableLayoutPanel1.ColumnStyles[1].Width = Screen.PrimaryScreen.Bounds.Width / 3;
                tableLayoutPanel1.ColumnStyles[2].Width = Screen.PrimaryScreen.Bounds.Width / 3;
                tableLayoutPanel1.RowStyles[0].Height = Screen.PrimaryScreen.Bounds.Height / 2;
                tableLayoutPanel1.RowStyles[1].Height = Screen.PrimaryScreen.Bounds.Height / 2;
            }
            else if (secilenler.Count <= 8)
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 4;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 4));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 4));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 4));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 2));
                tableLayoutPanel1.ColumnStyles[0].Width = Screen.PrimaryScreen.Bounds.Width / 4;
                tableLayoutPanel1.ColumnStyles[1].Width = Screen.PrimaryScreen.Bounds.Width / 4;
                tableLayoutPanel1.ColumnStyles[2].Width = Screen.PrimaryScreen.Bounds.Width / 4;
                tableLayoutPanel1.ColumnStyles[3].Width = Screen.PrimaryScreen.Bounds.Width / 4;
                tableLayoutPanel1.RowStyles[0].Height = Screen.PrimaryScreen.Bounds.Height / 2;
                tableLayoutPanel1.RowStyles[1].Height = Screen.PrimaryScreen.Bounds.Height / 2;
            }
            else if (secilenler.Count == 9)
            {
                tableLayoutPanel1.RowCount = 3;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 3));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 3));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 3));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 3));
                tableLayoutPanel1.ColumnStyles[0].Width = Screen.PrimaryScreen.Bounds.Width / 3;
                tableLayoutPanel1.ColumnStyles[1].Width = Screen.PrimaryScreen.Bounds.Width / 3;
                tableLayoutPanel1.ColumnStyles[2].Width = Screen.PrimaryScreen.Bounds.Width / 3;
                tableLayoutPanel1.RowStyles[0].Height = Screen.PrimaryScreen.Bounds.Height / 3;
                tableLayoutPanel1.RowStyles[1].Height = Screen.PrimaryScreen.Bounds.Height / 3;
                tableLayoutPanel1.RowStyles[2].Height = Screen.PrimaryScreen.Bounds.Height / 3;

            }
            acilis();
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {


        }
        private urunlistele anaekrann = (urunlistele)Application.OpenForms["urunlistele"];
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
         
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            anaekrann.Show();
            this.Close();
        }
    }
}
