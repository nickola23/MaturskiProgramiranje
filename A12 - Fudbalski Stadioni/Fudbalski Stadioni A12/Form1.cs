using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fudbalski_Stadioni_A12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection konekcija;
        SqlCommand komanda;
        DataTable dt, dt1;
        SqlDataAdapter da;

        void Konekcija()
        {
            konekcija = new SqlConnection();
            komanda = new SqlCommand();
            da = new SqlDataAdapter();
            dt = new DataTable();
            dt1 = new DataTable();
            konekcija.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|FudbalskiStadioni12.mdf;Integrated Security=True;Connect Timeout=30";
            komanda.Connection = konekcija;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Prikaz();
            Konekcija();
            komanda.CommandText = "SELECT * FROM grad";
            da.SelectCommand = komanda;
            da.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt1.Rows[i][1].ToString());
            }
            dt1.Clear();
            konekcija.Close();
        }

        public void Prikaz()
        {
            Konekcija();
            listView1.Clear();

            listView1.Columns.Add("Sifra", 50);
            listView1.Columns.Add("Naziv", 100);
            listView1.Columns.Add("Grad", 100);
            listView1.Columns.Add("Kapacitet", 100);
            listView1.Columns.Add("Adresa", 250);
            listView1.Columns.Add("Br. ulaza", 50); 

            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            komanda.CommandText = 
                "SELECT s.StadionID, s.Naziv, g.Grad, s.Kapacitet, s.Adresa, s.BrojUlaza " +
                "FROM stadion s JOIN grad g " +
                "ON s.GradID = g.GradID";
            da.SelectCommand = komanda;
            da.Fill(dt);

            foreach (DataRow red in dt.Rows)
            {
                string[] podaci ={
                        red[0].ToString(),
                        red[1].ToString(),
                        red[2].ToString(),
                        red[3].ToString(),
                        red[4].ToString(),
                        red[5].ToString()
                    };
                ListViewItem stavka = new ListViewItem(podaci);
                listView1.Items.Add(stavka);
            }
            dt.Clear();
            konekcija.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pretraga pretraga = new Pretraga();
            pretraga.ShowDialog();

            string drzava = pretraga.pretrazivanje;

            if(drzava == "") Prikaz();
            else
            {
                Konekcija();

                listView1.Clear();
                listView1.Items.Clear();

                listView1.Columns.Add("Sifra", 50);
                listView1.Columns.Add("Naziv", 100);
                listView1.Columns.Add("Grad", 100);
                listView1.Columns.Add("Kapacitet", 100);
                listView1.Columns.Add("Adresa", 250);
                listView1.Columns.Add("Br. ulaza", 50);

                listView1.View = View.Details;
                listView1.FullRowSelect = true;

                komanda.CommandText = 
                    "SELECT s.StadionID, s.Naziv, g.Grad, s.Kapacitet, s.Adresa, s.BrojUlaza " +
                    "FROM stadion s " +
                    "JOIN grad g ON s.GradID = g.GradID " +
                    "JOIN drzave d ON g.DrzavaID = d.DrzavaID " +
                    "WHERE d.naziv = @drzava";

                komanda.Parameters.AddWithValue("@drzava", drzava);

                da.SelectCommand = komanda;
                da.Fill(dt);

                foreach (DataRow red in dt.Rows)
                {
                    string[] podaci ={
                            red[0].ToString(),
                            red[1].ToString(),
                            red[2].ToString(),
                            red[3].ToString(),
                            red[4].ToString(),
                            red[5].ToString()
                    };
                    ListViewItem stavka = new ListViewItem(podaci);
                    listView1.Items.Add(stavka);
                }

                dt.Clear();
                konekcija.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Konekcija();
            komanda.CommandText = 
                "UPDATE stadion " +
                "SET Naziv=@naziv, Adresa=@adresa, Kapacitet=@kapacitet, BrojUlaza=@brulaza, GradID=(SELECT GradID FROM grad WHERE grad=@grad) " +
                "WHERE StadionID=@id";

            komanda.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
            komanda.Parameters.AddWithValue("@naziv", textBox2.Text);
            komanda.Parameters.AddWithValue("@adresa", textBox3.Text);
            komanda.Parameters.AddWithValue("@kapacitet", Convert.ToInt32(numericUpDown1.Value));
            komanda.Parameters.AddWithValue("@brulaza", Convert.ToInt32(numericUpDown2.Value));
            komanda.Parameters.AddWithValue("@grad", comboBox1.Text);

            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspesno izvrsena izmena!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                konekcija.Close();
            }

            Prikaz();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Statistika statistika = new Statistika();
            statistika.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Prosirenje pro = new Prosirenje();
            pro.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item;
            if (listView1.SelectedItems.Count != 0)
            {
                textBox1.Enabled = false;
                item = listView1.SelectedItems[0];

                textBox1.Text = item.SubItems[0].Text;
                textBox2.Text = item.SubItems[1].Text;
                textBox3.Text = item.SubItems[4].Text;

                numericUpDown1.Value = Convert.ToInt32(item.SubItems[3].Text);
                numericUpDown2.Value = Convert.ToInt32(item.SubItems[5].Text);


                comboBox1.Text = item.SubItems[2].Text;
            }
            else
            {
                textBox1.Text = textBox2.Text = textBox3.Text = comboBox1.Text = "";
                numericUpDown1.Value = numericUpDown2.Value = 0;
            }
        }
    }
}
