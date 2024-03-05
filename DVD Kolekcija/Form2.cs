using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVD_Kolekcija
{
    public partial class Form2 : Form
    {
        SqlConnection konekcija;
        SqlCommand komanda;
        SqlDataAdapter da;
        DataTable dt;

        public void Konekcija()
        {
            konekcija = new SqlConnection();
            komanda = new SqlCommand();
            da = new SqlDataAdapter();
            dt = new DataTable();
            konekcija.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=""D:\NIKOLA.MILANOVIC\MATURSKI\DVD KOLEKCIJA\BIN\DEBUG\DVD KOLEKCIJA1.MDF"";Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            komanda.Connection = konekcija;
        }
        public void BrisiTabelu()
        {
            dt.Rows.Clear();
            dt.Columns.Clear();
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Konekcija();
            komanda.CommandText = "SELECT p.ime AS Producent, COUNT(pr.filmId) AS 'Broj filmova' FROM producent p INNER JOIN producirao pr ON pr.producentId = p.producentId GROUP BY p.ime";
            da.SelectCommand = komanda;
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Producent"].IsValueShownAsLabel = true;
            Random r = new Random();
            chart1.Series["Producent"].Points.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++){
                chart1.Series["Producent"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value, dataGridView1.Rows[i].Cells[1].Value);
                chart1.Series["Producent"].Points[i].Color = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
                chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
