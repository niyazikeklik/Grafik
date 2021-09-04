using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafik
{
    public partial class table : Form
    {
        public table()
        {
            InitializeComponent();
        }
        public ArrayList secilenler = new ArrayList();
        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (secilenler.Count == 2)
            {
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.ColumnCount = 2;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 2));

                
            }
           else if (secilenler.Count <= 4)
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 2;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width/2));

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height/2));

            }
            else if (secilenler.Count <= 6)
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 3));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 3));

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 2));
            }
            else if (secilenler.Count <= 8)
            {
                tableLayoutPanel1.RowCount = 2;
                tableLayoutPanel1.ColumnCount = 4;
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 4));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 4));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Width / 4));

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, Screen.PrimaryScreen.Bounds.Height / 2));
            }
           
            int satir = 0, sutun = 0;
            for (int i = 0; i < secilenler.Count; i++)
            {


                grafik yeni = new grafik();
                yeni.TopLevel = false;
                yeni.Dock = DockStyle.Fill;
                yeni.FormBorderStyle = FormBorderStyle.None;
                yeni.Urunadi = secilenler[i].ToString();
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {


        }
        private Form2 anaekrann = (Form2)Application.OpenForms["Form2"];
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
