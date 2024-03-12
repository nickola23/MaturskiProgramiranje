using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arheoloska_Nalazista
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlCommand komanda;
        SqlConnection konekcija;
        SqlDataAdapter da;
        DataTable dt;
        private const string NO_IMAGE = "antikviteti\\no_image.jpg";
        int k = 0;
        void Konekcija()
        {
            konekcija = new SqlConnection();
            konekcija.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\nikola.milanovic\\maturski\\Arheoloska Nalazista\\bin\\Debug\\Arheoloska nalazista.mdf\";Integrated Security=True;Connect Timeout=30";
            komanda = new SqlCommand();
            komanda.Connection = konekcija;
            dt = new DataTable();
            da = new SqlDataAdapter();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Konekcija();
            komanda.CommandText = "SELECT * FROM tipovi_antikviteta";
            da.SelectCommand = komanda;
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox1.Text = dt.Rows[k][1].ToString();
            label2.Text = dt.Rows[k][0].ToString();
        }
        private void PrikaziTrenutnog()
        {
            textBox1.Text = dt.Rows[k][1].ToString();
            label2.Text = dt.Rows[k][0].ToString();
            if (dt.Rows[k][2] == DBNull.Value)
            {
                pictureBox1.Image = Image.FromFile(NO_IMAGE);
                pictureBox1.Tag = NO_IMAGE;
            }
            else
            {
                byte[] slikaBytes = (byte[])dt.Rows[k][2];
                MemoryStream ms = new MemoryStream(slikaBytes);
                pictureBox1.Image = Image.FromStream(ms);
            }
        }
        public void napred_nazad()
        {
            if(k == 0) 
            {
                button2.Enabled = false;
                button3.Enabled = true;
            }
            else if(k  == dt.Rows.Count - 1)
            {
                button2.Enabled = true;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            PrikaziTrenutnog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(k > 0)  k--;
            napred_nazad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (k < dt.Rows.Count - 1) k++;
            napred_nazad();
        }
    }
}
