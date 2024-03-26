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

namespace Biblioteka_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlCommand komanda;
        SqlConnection konekcija;
        SqlDataAdapter da;
        DataTable dt;
        public void Konekcija()
        {
            komanda = new SqlCommand();
            konekcija = new SqlConnection();
            da = new SqlDataAdapter();
            dt = new DataTable();
            konekcija.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\nikola.milanovic\\maturski\\Biblioteka 1\\bin\\Debug\\Biblioteka 2.mdf\";Integrated Security=True;Connect Timeout=30";
            komanda.Connection = konekcija;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Konekcija();
            listView1.Clear();
            listView1.Columns.Add("CitalacID", 60);
            listView1.Columns.Add("Maticni Broj", 100);
            listView1.Columns.Add("Ime", 100);
            listView1.Columns.Add("Prezime", 100);
            listView1.Columns.Add("Adresa", 100);
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            numericUpDown2.Maximum = DateTime.Now.Year;
            numericUpDown2.Value = numericUpDown2.Maximum;
            numericUpDown1.Maximum = DateTime.Now.Year - 1;
            numericUpDown2.Value = numericUpDown2.Maximum - 10;
            komanda.CommandText = "SELECT citalacID, maticni_broj, ime, prezime, adresa FROM Citalac";
            da.SelectCommand = komanda;
            da.Fill(dt);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string[] podaci =
                {
                    dt.Rows[i][0].ToString(),  dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString()
                };
                ListViewItem stavka = new ListViewItem(podaci);
                listView1.Items.Add(stavka);
            }
            komanda.CommandText = "SELECT * FROM citalac";
            da.SelectCommand = komanda;
            da.Fill(dt);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string kombo = dt.Rows[i][0] + "-" + dt.Rows[i][2] + " " + dt.Rows[i][3];
                comboBox1.Items.Add(kombo);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty)
            {
                Konekcija();
                int id = Convert.ToInt32(textBox1.Text);
                komanda.CommandText = "SELECT citalacID, maticni_broj, ime, prezime, adresa FROM citalac WHERE citalacID = @id";
                komanda.Parameters.AddWithValue("@id", id);
                da.SelectCommand = komanda;
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    textBox2.Text = dt.Rows[0][1].ToString();
                    textBox3.Text = dt.Rows[0][2].ToString();
                    textBox4.Text = dt.Rows[0][3].ToString();
                    textBox5.Text = dt.Rows[0][4].ToString();
                    textBox2.ReadOnly = textBox3.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = true;
                }
                else
                {
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox2.ReadOnly = textBox3.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = false;
                }
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox2.ReadOnly = textBox3.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = false;
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                textBox1.Text = listView1.SelectedItems[0].Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Konekcija();
            komanda.CommandText = "INSERT INTO Citalac(citalacID, maticni_broj, ime, prezime, adresa) VALUES(@id, @mat, @ime, @prezime, @adresa)";
            komanda.Parameters.AddWithValue("@id", textBox1.Text);
            komanda.Parameters.AddWithValue("@mat", textBox2.Text);
            komanda.Parameters.AddWithValue("@ime", textBox3.Text);
            komanda.Parameters.AddWithValue("@prezime", textBox4.Text);
            komanda.Parameters.AddWithValue("@adresa", textBox5.Text);
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Dodato u bazu", "Obavestenje")
            }
        }
    }
}
