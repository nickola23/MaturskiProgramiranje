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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace IzlozbaPasa
{
    public partial class Form1 : Form
    {
        SqlConnection konekcija;
        SqlCommand komanda;
        DataTable dt;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }

        void Konekcija()
        {
            konekcija = new SqlConnection();
            konekcija.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Izlozba pasa-osnovno.mdf;Integrated Security=True;Connect Timeout=30";
            komanda = new SqlCommand();
            komanda.Connection = konekcija;
            dt = new DataTable();
            da = new SqlDataAdapter();
        }

        public void BrisiTabelu()
        {
            dt.Rows.Clear();
            dt.Columns.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Konekcija();
            button3.Enabled = false;
            komanda.CommandText = "SELECT pas_id, ime FROM pas";
            da.SelectCommand = komanda;
            da.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString();
                string ime = dt.Rows[i][1].ToString();
                comboBox1.Items.Add(id + " - " + ime);
            }
            BrisiTabelu();
            komanda.CommandText = "SELECT id_izlozbe, mesto, datum FROM izlozba";
            da.SelectCommand = komanda;
            da.Fill(dt);
            for(int i = 0; i < dt.Rows.Count; i++) {
                string id = dt.Rows[i][0].ToString();
                string mesto = dt.Rows[i][1].ToString();
                string datum = Convert.ToDateTime(dt.Rows[i][2].ToString()).ToString("dd.MM.yyyy");
                comboBox2.Items.Add(id + " - " + mesto + " - " + datum);
                comboBox4.Items.Add(id + " - " + mesto + " - " + datum);
            }
            BrisiTabelu();
            komanda.CommandText = "SELECT id_kategorije, naziv FROM kategorija";
            da.SelectCommand = komanda;
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = dt.Rows[i][0].ToString();
                string naziv = dt.Rows[i][1].ToString();
                comboBox3.Items.Add(id + " - " + naziv);
            }
            BrisiTabelu();
        }

        public bool VecPrijavljen(string pasId, string izlozbaId, string kategorijaId)
        {
            Konekcija();
            konekcija.Open();
            komanda.CommandText = "SELECT COUNT(*) FROM rezultat WHERE id_izlozbe = @idIzlozba AND id_kategorije = @idKategorija AND pas_id = @pasId";
            komanda.Parameters.AddWithValue("@idIzlozba", izlozbaId);
            komanda.Parameters.AddWithValue("@idKategorija", kategorijaId);
            komanda.Parameters.AddWithValue("@pasID", pasId);
            int br = Convert.ToInt32(komanda.ExecuteScalar());
            return br > 0;
        }

        DateTime DatumPrijave()
        {
            Konekcija();
            string izlozbaId = comboBox2.Text.Split('-')[0];
            komanda.CommandText = "SELECT datum FROM izlozba WHERE id_izlozbe = @izlozbaId";
            komanda.Parameters.AddWithValue("@izlozbaId", izlozbaId);
            da.SelectCommand = komanda;
            da.Fill(dt);
            return Convert.ToDateTime(dt.Rows[0][0]);
        }

        public void ZakasnelaPrijava()
        {
            if(DatumPrijave() <= DateTime.Now.AddDays(2))
            {
                MessageBox.Show("Zakasnela prijava", "Greska");
                comboBox1.Focus();
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled=true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pasId = comboBox1.Text.Split('-')[0];
            string izlozbaId = comboBox2.Text.Split('-')[0];
            string kategorijaId = comboBox3.Text.Split('-')[0];
            ZakasnelaPrijava();
            if (VecPrijavljen(pasId, izlozbaId, kategorijaId))
            {
                MessageBox.Show("Pas je vec prijavljen na ovu izlozbu");
                return;
            }
            konekcija.Close();
            komanda.CommandText = "INSERT INTO rezultat(id_izlozbe, id_kategorije, pas_id) VALUES (@izlozba, @kategorija, @pas)";
            komanda.Parameters.AddWithValue("@izlozba", izlozbaId);
            komanda.Parameters.AddWithValue("@kategorija", kategorijaId);
            komanda.Parameters.AddWithValue("@pas", pasId);
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Pas je dodat", "Uspesno" );
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Greska");
            }
            finally
            {
                konekcija.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZakasnelaPrijava();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Konekcija();
            string idizlozbe = comboBox4.Text.Split('-')[0];
            komanda.CommandText = "SELECT COUNT(*), COUNT(rezultat) FROM rezultat WHERE id_izlozbe = @idIzlozbe";
            komanda.Parameters.AddWithValue("@idIzlozbe", idizlozbe);
            BrisiTabelu();
            da.SelectCommand = komanda;
            da.Fill(dt);
            label7.Text = dt.Rows[0][0].ToString();
            label8.Text = dt.Rows[0][1].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Konekcija();
            string idizlozbe = comboBox4.Text.Split('-')[0];
            komanda.CommandText = "SELECT k.id_kategorije AS Sifra, k.naziv AS 'Naziv Kategorije', COUNT(r.rezultat) AS 'Broj Pasa' FROM kategorija k JOIN rezultat r ON k.id_kategorije = r.id_kategorije WHERE r.id_izlozbe = @idIzlozbe GROUP BY k.id_kategorije, k.naziv ORDER BY k.id_kategorije";
            komanda.Parameters.AddWithValue("@idIzlozbe", idizlozbe);
            da.SelectCommand = komanda;
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            chart1.DataSource = dt;
            chart1.Series["Rezultat"].IsValueShownAsLabel = true;
            chart1.Series["Rezultat"].Points.Clear();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow red = dt.Rows[i];
                chart1.Series["Rezultat"].Points.AddXY(red[1].ToString(), red[2].ToString());
            }
            chart1.Visible = true;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime trenutnoVreme = DateTime.Now;
            Konekcija();

            string bekap = "backup" + trenutnoVreme.ToString("dd.MM.yyyy HH.mm.ss");
            // string bekap = "backup12.11.1975 23.01.";
            Text = bekap;
            // Putanja gde ćete sačuvati bekap fajl
            string backupPath = "C:\\Bekap\\" + bekap + ".bak";

            komanda.CommandText = @"BACKUP DATABASE Izlozba pasa-osnovno TO DISK = '{backupPath}'";
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Kreiran bekap");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greska");
            }
            finally
            {
                konekcija.Close();
            }
        }
    }
}
