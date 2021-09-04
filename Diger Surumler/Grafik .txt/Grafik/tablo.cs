using System;
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
    public partial class tablo : Form
    {
        public tablo()
        {
            InitializeComponent();
        }
       public grafik form1;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            form1.cartesianChart1.Zoom = LiveCharts.ZoomingOptions.Xy;
            e.Cancel=true;
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
