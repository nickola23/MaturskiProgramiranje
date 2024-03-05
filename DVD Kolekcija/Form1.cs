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

namespace DVD_Kolekcija
{
    public partial class Form1 : Form
    {
        SqlConnection konekcija;
        SqlCommand komanda;
        SqlDataAdapter da;
        DataTable dt;
        List<Producent> producenti;

        public Form1()
        {
            InitializeComponent();
            textBox1.Enabled = false;
        }

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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Konekcija();
            komanda.CommandText = "UPDATE producent SET ime = @ime, email = @email WHERE producentid = @producentid ";
            komanda.Parameters.AddWithValue("@ime", textBox2.Text);
            komanda.Parameters.AddWithValue("@email", textBox3.Text);
            komanda.Parameters.AddWithValue("@producentid", textBox1.Text);
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspesna izmena", "Obavestenje");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Greska");
            }
            finally
            {
                konekcija.Close();
            }
            Form1_Load(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Konekcija();
            komanda.CommandText = "SELECT ProducentId, Ime, Email FROM producent";
            da.SelectCommand = komanda;
            da.Fill(dt);
            producenti = new List<Producent>();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                producenti.Add(new Producent(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString()));
            }
            listBox1.DataSource = producenti;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = producenti[listBox1.SelectedIndex].ime;
            textBox1.Text = producenti[listBox1.SelectedIndex].sifra;
            textBox3.Text = producenti[listBox1.SelectedIndex].email;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form2 forma2 = new Form2();
            forma2.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form3 forma3 = new Form3();
            forma3.ShowDialog();
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
