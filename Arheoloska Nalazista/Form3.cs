using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arheoloska_Nalazista
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            numericUpDown1.Minimum = 1980;
            numericUpDown1.Maximum = 2023;
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 10;
            dataGridView1.Columns[0].HeaderText = "Grad";
            dataGridView1.Columns[1].HeaderText = "Broj Antikviteta";
            dataGridView1.Columns[0].Width = (dataGridView1.Width - dataGridView1.RowHeadersWidth) / 2;
            dataGridView1.Columns[1].Width = (dataGridView1.Width - dataGridView1.RowHeadersWidth) / 2;
        }
        SqlCommand komanda;
        SqlConnection konekcija;
        SqlDataAdapter da;
        DataTable dt;
        void Konekcija()
        {
            konekcija = new SqlConnection();
            konekcija.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\nikola.milanovic\\maturski\\Arheoloska Nalazista\\bin\\Debug\\Arheoloska nalazista.mdf\";Integrated Security=True;Connect Timeout=30";
            komanda = new SqlCommand();
            komanda.Connection = konekcija;
            dt = new DataTable();
            da = new SqlDataAdapter();
        }
        void Analiza()
        {
            Konekcija();
            komanda.CommandText = 
                "SELECT g.ime_grada, COUNT(a.antikvitetID) " +
                "FROM gradovi g " +
                "INNER JOIN lokaliteti l ON g.gradID = l.gradID " +
                "INNER JOIN antikviteti a ON a.lokalitetID = l.lokalitetID " +
                "WHERE a.datum_pronalaska > @godina " +
                "GROUP BY g.ime_grada " +
                "HAVING COUNT(*) > @broj_antikviteta";
            komanda.Parameters.AddWithValue("@godina", numericUpDown1.Value.ToString());
            komanda.Parameters.AddWithValue("@broj_antikviteta", numericUpDown2.Value);
            da.SelectCommand = komanda;
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Analiza();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Analiza();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["Arheologija"].IsValueShownAsLabel = true;
            Random r = new Random();
            ArrayList grad = new ArrayList();
            ArrayList broj_antikviteta = new ArrayList();
            chart1.Series["Arheologija"].Points.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                grad.Add(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value));
                broj_antikviteta.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value));
                chart1.Series["Arheologija"].Points.AddXY(grad[i].ToString(), broj_antikviteta[i].ToString());
                chart1.Series["Arheologija"].Points[i].Color = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
            }
        }
    }
}
