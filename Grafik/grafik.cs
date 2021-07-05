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
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
namespace Grafik
{
    public partial class grafik : Form
    {
        public grafik()
        {
            // System.Windows.Controls..CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
          
       
            cartesianChart1.Zoom = ZoomingOptions.Xy;
            cartesianChart1.DataTooltip.Visibility = System.Windows.Visibility.Hidden;
            cartesianChart1.DataTooltip.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
        }
        private void ClearZoom()
        {
            cartesianChart1.AxisX[0].MinValue = double.NaN;
            cartesianChart1.AxisX[0].MaxValue = double.NaN;
            cartesianChart1.AxisY[0].MinValue = double.NaN;
            cartesianChart1.AxisY[0].MaxValue = double.NaN;
        }
        public string Urunadi = "";
        tablo tablo = new tablo();
        ArrayList kisiler = new ArrayList();
        List<string> xekseni = new List<string>();
        List<string> Siralanmisxekseni = new List<string>();
        void GrafikÇiz()
        {
            for (int i = 1; i < tablo.bunifuCustomDataGrid1.ColumnCount; i++)
            {
                List<double> allValues = new List<double>();
                for (int j = 0; j < tablo.bunifuCustomDataGrid1.RowCount; j++)
                {
                    string deger = tablo.bunifuCustomDataGrid1.Rows[j].Cells[i].Value.ToString();
                    double deger2;
                    if (deger.Length == 0) deger2 = double.NaN;
                    else deger2 = Convert.ToDouble(deger);
                    allValues.Add(Convert.ToDouble(deger2));
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
      
        
        void sutun(string isim)
        {
            if (!tablo.bunifuCustomDataGrid1.Columns.Contains(isim))
            {
                DataGridViewTextBoxColumn sira = new DataGridViewTextBoxColumn
                {
                    Name = isim,
                    Width = 45
                };
                tablo.bunifuCustomDataGrid1.Columns.Add(sira);
            }
            if (!kisiler.Contains(isim)) kisiler.Add(isim);
        }
        void tabloolustur(string isim, double deger)
        {
            for (int jj = 0; jj < tablo.bunifuCustomDataGrid1.Columns.Count; jj++)
            {
                if (tablo.bunifuCustomDataGrid1.Columns[jj].Name == isim)
                {
                    tablo.bunifuCustomDataGrid1.Rows[tablo.bunifuCustomDataGrid1.Rows.Count - 1].Cells[jj].Value = deger;
                    break;
                }
            }
        }
        List<string> eksenekle(List <string> liste) {
           
            Axis eksenekle = new Axis();
            eksenekle.LabelsRotation = 15;
            eksenekle.Title = "TARİHLER";
            eksenekle.Labels = liste;
            cartesianChart1.AxisX.Add(eksenekle);
            return liste;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            bunifuCustomLabel1.Text = Urunadi;
            label1.Text = Urunadi;
            string[] allLines = File.ReadAllLines(Application.StartupPath + @"/Urunler/" + Urunadi + ".csv");
            string[] columnTitles = allLines[0].Split(';');
            for (int i = 0; i < columnTitles.Length; i++)
            {
                sutun(columnTitles[i]);
            }
            var query = allLines.Skip(1).Select((r, index) => new
            {
                i = index,
                Data = r.Split(';')
            }).ToList();
            for (int i = 0; i < query.Count; i++)
            {
                xekseni.Add(query[i].Data[0].ToLower().Replace(".", "/"));
                tablo.bunifuCustomDataGrid1.Rows.Add();
                for (int j = 0; j < columnTitles.Length; j++)
                {
                    tablo.bunifuCustomDataGrid1.Rows[i].Cells[j].Value = query[i].Data[j];
                }
            }
            
         
            Siralanmisxekseni = eksenekle(xekseni.OrderBy(x => DateTime.Parse(x)).ToList());
            GrafikÇiz();
        }
        private urunlistele anaekrann = (urunlistele)Application.OpenForms["urunlistele"];
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            anaekrann.Show();
            this.Close();
        }
        public static void TakeCroppedScreenShot(string fileName, int x, int y, int width, int height, Size size)
        {
            Bitmap Screenshot = new Bitmap(width, height);
            Graphics GFX = Graphics.FromImage(Screenshot);
            GFX.CopyFromScreen(x, y + 25, 0, 0, size);
            SaveFileDialog dosyakaydet = new SaveFileDialog();
            dosyakaydet.Filter = "Jpg Dosyası|*.jpg";
            if (dosyakaydet.ShowDialog() == DialogResult.OK)
            {
                Screenshot.Save(dosyakaydet.FileName);
            }
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            TakeCroppedScreenShot("oylesine.jpg", panel2.Location.X, panel2.Location.Y, panel2.Width, panel2.Height, panel2.Size);
        }
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            ClearZoom();
        }
        private void cartesianChart1_DataHover(object sender, ChartPoint chartPoint)
        {
            if (panel1.Visible)
            {
                label3.Text = "Tarih: " + Siralanmisxekseni[Convert.ToInt32(chartPoint.X)].ToString();
                label4.Text = "Seçilen kişi: " + chartPoint.SeriesView.Title;
                label5.Text = "Değer: " + chartPoint.SeriesView.Values[Convert.ToInt32(chartPoint.X)].ToString();
            }
            else if (panel3.Visible)
            {
                bunifuCustomLabel1.Text = Urunadi + "        Tarih: " + Siralanmisxekseni[Convert.ToInt32(chartPoint.X)] + " S. kişi: " + chartPoint.SeriesView.Title + " Değer: " + chartPoint.SeriesView.Values[Convert.ToInt32(chartPoint.X)].ToString();
            }
        }
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            cartesianChart1.Zoom = ZoomingOptions.None;
            tablo.form1 = this;
            tablo.Show();
        }
        private void bunifuiOSSwitch1_OnValueChange_1(object sender, EventArgs e)
        {
            if (bunifuiOSSwitch1.Value)
            {
                cartesianChart1.DataTooltip.Visibility = System.Windows.Visibility.Visible; label6.Text = "Pop-up ON";
            }
            else
            {
                cartesianChart1.DataTooltip.Visibility = System.Windows.Visibility.Hidden;
                label6.Text = "Pop-up OFF";
            }
        }
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            grafik yenii = new grafik();
            yenii.Urunadi = Urunadi;
            yenii.FormBorderStyle = FormBorderStyle.FixedSingle;
            yenii.panel3.Visible = false;
            yenii.panel1.Visible = true;
            yenii.WindowState = FormWindowState.Maximized;
            yenii.ShowDialog();
        }
    }
}
