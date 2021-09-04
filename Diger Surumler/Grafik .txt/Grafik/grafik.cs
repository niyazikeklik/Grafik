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
using System.Windows.Threading;

namespace Grafik
{
    public partial class grafik : Form
    {

        public grafik()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
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
            for (int i = 0; i < tablo.bunifuCustomDataGrid1.ColumnCount; i++)
            {
                List<double> allValues = new List<double>();
                for (int j = 0; j < tablo.bunifuCustomDataGrid1.RowCount; j++)
                {
                    double deger = Convert.ToDouble(tablo.bunifuCustomDataGrid1.Rows[j].Cells[i].Value);
                    if (deger == 0) deger = double.NaN;
                    allValues.Add(deger);
                }
                if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
                {
                    //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        LineSeries yeni = new LineSeries();
                        yeni.PointGeometrySize = 12;
                        yeni.LineSmoothness = 0;
                        yeni.Fill = System.Windows.Media.Brushes.Transparent;
                        yeni.Title = kisiler[i].ToString();
                        yeni.Values = new ChartValues<double>(allValues);
                        cartesianChart1.Series.Add(yeni);
                    });
                }
              
            }
        }
        int İsimBaslangicbul(string satir)
        {
            for (int i = 3; i < satir.Length; i++)
            {
                if (satir[i].ToString().ToLower() != " " && !Char.IsNumber(satir[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        string isimbul(string satir)
        {
            try
            {
                string cikti = "";
                int isimbaslangicindex = İsimBaslangicbul(satir);
                int isimuzunluk = satir.IndexOf(" ", isimbaslangicindex) - isimbaslangicindex;
                string isim = satir.Substring(isimbaslangicindex, isimuzunluk);
                int isimbitis = isimbaslangicindex + isim.Length;
                string soyisim = satir.Substring(isimbitis, satir.IndexOf("  ", isimbitis) - isimbitis);
                return cikti = isim + soyisim;
            }
            catch (Exception)
            {
                return "error";
            }
        }
        int degerbaslangicbul(string isim, string satir)
        {
            for (int i = (satir.IndexOf(isim) + isim.Length); i < satir.Length; i++)
            {
                if (satir[i] != ' ') return i;
            }
            return -1;
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
        Random rnd = new Random();
        void tabloolustur(string isim, double deger)
        {
            for (int jj = 0; jj < tablo.bunifuCustomDataGrid1.Columns.Count; jj++)
            {
                if (tablo.bunifuCustomDataGrid1.Columns[jj].Name == isim)
                {

                    tablo.bunifuCustomDataGrid1.Rows[tablo.bunifuCustomDataGrid1.Rows.Count - 1].Cells[jj].Value = rnd.Next(-500000, 500000) + deger;
                    break;
                }
            }
        }
        List<string> eksenekle(List<string> orderedList)
        {

            Axis eksenekle = new Axis();
            eksenekle.LabelsRotation = 15;
            eksenekle.Title = "TARİHLER";
            eksenekle.Labels = orderedList;
            cartesianChart1.AxisX.Add(eksenekle);
            return orderedList;
        }
        void Calistir()
        {

            label1.Text = Urunadi;
            bunifuCustomLabel1.Text = Urunadi;
            string[] dizindekiDosyalar = Directory.GetFiles(Application.StartupPath + @"/Urunler/" + Urunadi);

            foreach (string dosya in dizindekiDosyalar)
            {
                FileInfo fileInfo = new FileInfo(dosya);
                string dosyaAdi = fileInfo.Name;
                xekseni.Add(dosyaAdi.ToLower().Replace("_", "/").Replace(".txt", ""));
            }
            try {
              if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
                {
                    //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                    this.Invoke((MethodInvoker)delegate ()
                  {
                        Siralanmisxekseni = eksenekle(xekseni.OrderBy(x => DateTime.Parse(x)).ToList());
                    });
                }

               }
            catch (FormatException)
            {
                MessageBox.Show("Formata uymayan dosya ismi veya veri girişi!" + Environment.NewLine + Environment.NewLine +
                    "Dosya isimleri arasında alt tire olacak şekilde GG_AA_YYYY şeklinde olmalıdır." + Environment.NewLine + Environment.NewLine +
                    "Örnek kullanım: 24_02_2019", "Uygun olmayan girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }

            foreach (string dosya in Siralanmisxekseni)
            {
                bool satirekle = true;
                string dosyaAdi = dosya.Replace("/", "_") + ".txt";
                StreamReader sr = new StreamReader(Application.StartupPath + @"/Urunler/" + Urunadi + "//" + dosyaAdi, Encoding.GetEncoding("iso-8859-9"));
                string satir = sr.ReadLine();
                int satirgec = 0;
                int satirsayisi = 0;
                while (satir != null)
                {
                    if (satirgec < 8) { satirgec++; satir = sr.ReadLine(); continue; }
                    if (satir.Length < 2) { satir = sr.ReadLine(); continue; }
                    satir = satir.Replace("\t", "   ");
                    satirsayisi++;
                   
                    string isim = isimbul(satir);
                    if (isim == "error")
                    {
                        MessageBox.Show(dosyaAdi + " İsimli dosyada " + satirsayisi + ". satırda isim sütununda uygunsuz veri formatı tespit edildi.", "Uygun olmayan girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }
                    double deger = -1;
                    try
                    {
                        int degerbaslangic = degerbaslangicbul(isim, satir);
                        deger = Convert.ToDouble(satir.Substring(degerbaslangic, satir.IndexOf(' ', degerbaslangic) - degerbaslangic));
                    }
                    catch (Exception)
                    {

                        MessageBox.Show(dosyaAdi + " İsimli dosyada " + satirsayisi + ". satırda deger sütununda uygunsuz veri formatı tespit edildi.", "Uygun olmayan girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }

                    sutun(isim);
                    if (satirekle)
                    {
                        tablo.bunifuCustomDataGrid1.Rows.Add();
                        for (int i = 0; i < tablo.bunifuCustomDataGrid1.ColumnCount; i++)
                        {
                            tablo.bunifuCustomDataGrid1.Rows[tablo.bunifuCustomDataGrid1.Rows.Count - 1].Cells[i].Value = 0;

                        }
                        satirekle = false;
                    }
                    tabloolustur(isim, deger);
                    satir = sr.ReadLine();
                }
                sr.Close();
            }
          
             GrafikÇiz();

          
        }
     
        Form1 yukleniyor = new Form1();
        private void Form5_Load(object sender, EventArgs e)
        {
            Thread start = new Thread(new ThreadStart(Calistir));
            start.Start();

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
