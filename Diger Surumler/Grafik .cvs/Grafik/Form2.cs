using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafik
{
    public partial class Form2 : Form
    {
        public Form2()
        {
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
        Form5 yeni = new Form5();
        yeni.Urunadi = bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();
        yeni.Show();
        this.Hide();

    }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (bunifuCustomDataGrid1.SelectedRows.Count >8)
            {
                MessageBox.Show("En fazla 8 adet ürün seçebilirsiniz!", "Ürün sınırı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (bunifuCustomDataGrid1.SelectedRows.Count== 1)
            {
                MessageBox.Show("Bu özelliği kullanabilmek için en az 2 ürün seçmeniz gerekir! Tek ürün grafiği için lütfen üstünde çift tıklayınız.", "Ürün sınırı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                table formm = new table();
                foreach (DataGridViewRow drow in bunifuCustomDataGrid1.SelectedRows)  //Seçili 
                {
                    formm.secilenler.Add(drow.Cells[0].Value.ToString());
                }
                formm.Show();
            }
           

           
        }
    }
}
