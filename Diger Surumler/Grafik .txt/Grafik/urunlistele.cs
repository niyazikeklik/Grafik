using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafik
{
    public partial class urunlistele : Form
    {
        public urunlistele()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

       
    private void Form2_Load(object sender, EventArgs e)
    {
        string[] dizindekiKlasorler = Directory.GetDirectories(Application.StartupPath+ @"/Urunler");



        foreach (string klasor in dizindekiKlasorler)

        {

            DirectoryInfo dirInfo = new DirectoryInfo(klasor);

            string klasorAdi = dirInfo.Name;

            DateTime olsTarihi = dirInfo.CreationTime;







            bunifuCustomDataGrid1.Rows.Add(klasorAdi);




        }
    }

    private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {

    }

    private void bunifuCustomDataGrid1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        grafik yeni = new grafik();
        yeni.Urunadi = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();
        yeni.Show();
            
        this.Hide();

    }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count >9)
            {
                MessageBox.Show("En fazla 9 adet ürün seçebilirsiniz!", "Ürün sınırı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (bunifuCustomDataGrid1.SelectedRows.Count== 1)
            {
                MessageBox.Show("Bu özelliği kullanabilmek için en az 2 ürün seçmeniz gerekir! Tek ürün grafiği için lütfen üstünde çift tıklayınız.", "Ürün sınırı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                coklugrafik formm = new coklugrafik();
                foreach (DataGridViewRow drow in bunifuCustomDataGrid1.SelectedRows)  //Seçili 
                {
                    formm.secilenler.Add(drow.Cells[0].Value.ToString());
                }
                formm.Show();
            }
           

           
        }

        private void bunifuCustomDataGrid1_SelectionChanged(object sender, EventArgs e)
        {
            if (9 >= bunifuCustomDataGrid1.SelectedRows.Count &&bunifuCustomDataGrid1.SelectedRows.Count > 1)
            {

                bunifuThinButton21.IdleFillColor = Color.SeaGreen;
                bunifuThinButton21.IdleForecolor = Color.White;
                bunifuThinButton21.ButtonText = "(" + bunifuCustomDataGrid1.SelectedRows.Count.ToString() + ") Seçilenleri Aç";
            }
            else if(9< bunifuCustomDataGrid1.SelectedRows.Count)
            {
                bunifuThinButton21.IdleFillColor = Color.Red;
                bunifuThinButton21.IdleForecolor = Color.White;
                bunifuThinButton21.ButtonText = "(" + bunifuCustomDataGrid1.SelectedRows.Count.ToString() + ") Seçilenleri Aç";

            }
            else if(bunifuCustomDataGrid1.SelectedRows.Count == 1)
            {
                bunifuThinButton21.IdleFillColor = Color.White;
                bunifuThinButton21.IdleForecolor = Color.SeaGreen;
                bunifuThinButton21.ButtonText = "Seçilenleri Aç";
            }
        
        }
    }
}
