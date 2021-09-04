﻿
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Grafik
{
    public partial class Form5 : Form
    {

        public Form5()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            cartesianChart1.Zoom = ZoomingOptions.Xy;
        }
        private void ClearZoom()
        {
            cartesianChart1.AxisX[0].MinValue = double.NaN;
            cartesianChart1.AxisX[0].MaxValue = double.NaN;
            cartesianChart1.AxisY[0].MinValue = double.NaN;
            cartesianChart1.AxisY[0].MaxValue = double.NaN;
        }
        public string Urunadi = "";
        void Griddoldur() { }
        Form4 gridekrani = new Form4();
        Form1 tablo = new Form1();
        void GridOlustur(string[] columns)
        {
            calis = false;
            for (int i = 0; i < columns.Length; i++)
            {

                DataGridViewTextBoxColumn sira = new DataGridViewTextBoxColumn
                {
                    Name = columns[i],
                    Width = 45
                };
                gridekrani.bunifuCustomDataGrid1.Columns.Add(sira);

            }
        }
        ArrayList sutunlar = new ArrayList();
        ArrayList kisiler = new ArrayList();
        // List<DateTime> xekseni = new List<DateTime>();
        List<string> xekseni = new List<string>();

        bool calis = true;
        private void Form5_Load(object sender, EventArgs e)
        {
            label1.Text = Urunadi;
            string[] dizindekiDosyalar = Directory.GetFiles(Application.StartupPath + @"/Urunler/" + Urunadi);
            int dizii = 0;
            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;
                xekseni.Add(dosyaAdi.ToLower().Replace("_", "/").Replace(".csv", ""));
            }

            try
            {
                var orderedList = xekseni.OrderBy(x => DateTime.Parse(x)).ToList();

                cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "Tarihler",
                    Labels = orderedList
                });
                foreach (string dosya in orderedList)
                {
                    FileInfo fileInfo = new FileInfo(dosya.Replace("/", "_") + ".csv");
                    string dosyaAdi = fileInfo.Name;
                    dizii++;
                    string[] allLines = File.ReadAllLines(Application.StartupPath + @"/Urunler/" + Urunadi + @"/" + dosyaAdi);
                    string[] columnTitles = allLines[0].Split(';');
                    if (calis) GridOlustur(columnTitles);
                    var query = allLines.Skip(1).Select((r, index) => new
                    {
                        Data = r.Split(';')
                    }).ToList();

                    for (int i = 0; i < query.Count; i++)
                    {

                        if (!tablo.bunifuCustomDataGrid1.Columns.Contains(query[i].Data[1]))
                        {
                            DataGridViewTextBoxColumn sira = new DataGridViewTextBoxColumn
                            {
                                Name = query[i].Data[1],
                                Width = 45
                            };
                            tablo.bunifuCustomDataGrid1.Columns.Add(sira);
                        }
                        if (!kisiler.Contains(query[i].Data[1].ToString())) kisiler.Add(query[i].Data[1].ToString());
                    }
                    tablo.bunifuCustomDataGrid1.Rows.Add();

                    for (int i22 = 0; i22 < query.Count; i22++)
                    {
                        int index = -1;
                        for (int jj = 0; jj < tablo.bunifuCustomDataGrid1.Columns.Count; jj++)
                        {
                            if (tablo.bunifuCustomDataGrid1.Columns[jj].Name == query[i22].Data[1].ToString())
                            {
                                index = jj;
                                tablo.bunifuCustomDataGrid1.Rows[tablo.bunifuCustomDataGrid1.Rows.Count - 1].Cells[index].Value = query[i22].Data[4];
                                break;
                            }
                        }
                    }
                        
                      


                     /*   gridekrani.bunifuCustomDataGrid1.Rows.Add(
                            gridekrani.bunifuCustomDataGrid1.Rows.Count + 1,
                            query[i].Data[1],
                            query[i].Data[2],
                            query[i].Data[3],
                            query[i].Data[4],
                            query[i].Data[5],
                            query[i].Data[6]
                           );*/
                 


                    }


                
             /*   tabloolustur();*/
                for (int i = 0; i < tablo.bunifuCustomDataGrid1.ColumnCount; i++)
                {
                    List<double> allValues = new List<double>();
                    for (int j = 0; j < tablo.bunifuCustomDataGrid1.RowCount; j++)
                    {
                        allValues.Add(Convert.ToDouble(tablo.bunifuCustomDataGrid1.Rows[j].Cells[i].Value));
                    }
                    LineSeries yeni = new LineSeries();
                    yeni.PointGeometrySize = 20;
                    yeni.LineSmoothness = 0;
                    yeni.Fill = System.Windows.Media.Brushes.Transparent;
                    yeni.Title = kisiler[i].ToString();
                    yeni.Values = new ChartValues<double>(allValues);
                    cartesianChart1.Series.Add(yeni);
                }
            }
            catch (FormatException)
            {

                MessageBox.Show("Formata uymayan dosya ismi!" + Environment.NewLine + Environment.NewLine +
                    "Dosya isimleri arasında alt tire olacak şekilde GG_AA_YYYY şeklinde olmalıdır." + Environment.NewLine + Environment.NewLine +
                    "Örnek kullanım: 24_02_2019", "Uygun olmayan girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }


        }
      /*  void tabloolustur()
        {
            for (int i = 0; i < kisiler.Count; i++)
            {
                List<double> allValues = new List<double>();

                DataGridViewTextBoxColumn sira = new DataGridViewTextBoxColumn
                {
                    Name = kisiler[i].ToString(),
                    Width = 60
                };
                tablo.bunifuCustomDataGrid1.Columns.Add(sira);
                for (int j = 0; j < gridekrani.bunifuCustomDataGrid1.Rows.Count; j++)
                {
                    if (kisiler[i].ToString() == gridekrani.bunifuCustomDataGrid1.Rows[j].Cells[1].Value.ToString())
                        allValues.Add(Convert.ToDouble(gridekrani.bunifuCustomDataGrid1.Rows[j].Cells[4].Value));
                }
                if (tablo.bunifuCustomDataGrid1.Rows.Count < allValues.Count)
                {
                    for (int k = allValues.Count - tablo.bunifuCustomDataGrid1.Rows.Count; k > 0; k--)
                    {
                        tablo.bunifuCustomDataGrid1.Rows.Add();
                    }
                }
                for (int l = 0; l < allValues.Count; l++)
                {
                    tablo.bunifuCustomDataGrid1.Rows[l].Cells[i].Value = allValues[l];

                }
            }*/


      //  }

        private Form2 anaekrann = (Form2)Application.OpenForms["Form2"];

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            anaekrann.Show();
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {




        }

        private void cartesianChart1_DataClick(object sender, ChartPoint chartPoint)
        {


        }


        public static void TakeCroppedScreenShot(string fileName, int x, int y, int width, int height, Size size)
        {

            // SS alan kodlar
            Bitmap Screenshot = new Bitmap(width, height);
            Graphics GFX = Graphics.FromImage(Screenshot);
            GFX.CopyFromScreen(x, y + 25, 0, 0, size);


            SaveFileDialog dosyakaydet = new SaveFileDialog();
            dosyakaydet.Filter = "Jpg Dosyası|*.jpg";
            if (dosyakaydet.ShowDialog() == DialogResult.OK)
            {

                Screenshot.Save(dosyakaydet.FileName);

            }
            // Initialize new PDF document

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

            TakeCroppedScreenShot("oylesine.jpg", panel2.Location.X, panel2.Location.Y, panel2.Width, panel2.Height, panel2.Size);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            ClearZoom();
        }

        private void bunifuiOSSwitch1_OnValueChange(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cartesianChart1_DataHover(object sender, ChartPoint chartPoint)
        {
            label3.Text = chartPoint.SeriesView.Title;
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            cartesianChart1.Zoom = ZoomingOptions.None;
            tablo.form1 = this;
            tablo.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            gridekrani.Show();
        }
    }
}
