
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
using System.Windows.Media;

namespace Grafik
{
    public partial class grafik : Form
    {
        public grafik()
        {
            InitializeComponent();
        }
        public string Urunadi = "";

        Form1 tablo = new Form1();

        ArrayList kisiler = new ArrayList();
        // List<DateTime> xekseni = new List<DateTime>();
        List<string> xekseni = new List<string>();

        bool calis = true;
        List<string> xekseni22 = new List<string>();
        private void grafik_Load(object sender, EventArgs e)
        {
            cartesianChart1.Zoom = ZoomingOptions.Xy;
            cartesianChart1.DataTooltip.Visibility = System.Windows.Visibility.Hidden;
            cartesianChart1.DataTooltip.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            bunifuCustomLabel1.Text = Urunadi;
            string[] dizindekiDosyalar = Directory.GetFiles(Application.StartupPath + @"/Urunler/" + Urunadi);
            int dizii = 0;
            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;
                xekseni.Add(dosyaAdi.ToLower().Replace("_", "/").Replace(".csv", ""));
            }
            var orderedList = xekseni.OrderBy(x => DateTime.Parse(x)).ToList();
            xekseni22 = orderedList;

            try
            {


                cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    LabelsRotation = 15,
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
                    yeni.PointGeometrySize = 12;
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

        private void cartesianChart1_DataHover(object sender, ChartPoint chartPoint)
        {
            bunifuCustomLabel1.Text =  Urunadi+"        Tarih: " + xekseni22[Convert.ToInt32(chartPoint.X)] + " S. kişi: " + chartPoint.SeriesView.Title + " Değer: " + chartPoint.SeriesView.Values[Convert.ToInt32(chartPoint.X)].ToString();
           
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Form3 yenii = new Form3();
            yenii.Urunadi = Urunadi;
            yenii.ShowDialog();
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
