using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Fudbalski_Stadioni_A12
{
    public partial class Prosirenje : Form
    {
        public Prosirenje()
        {
            InitializeComponent();
        }

        SqlConnection konekcija;
        SqlCommand komanda;

        void Konekcija()
        {
            konekcija = new SqlConnection();
            komanda = new SqlCommand();
            konekcija.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|FudbalskiStadioni12.mdf;Integrated Security=True;Connect Timeout=30";
            komanda.Connection = konekcija;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Konekcija();
            DateTime datumPocetka = dateTimePicker2.Value;
            DateTime datumZavrsetka = dateTimePicker3.Value;

            if (datumZavrsetka != DateTime.MinValue && datumZavrsetka <= datumPocetka)
            {
                MessageBox.Show("Datum završetka igranja mora biti nakon datuma početka igranja.");
                return;
            }
            komanda.CommandText =
                "INSERT INTO Igrac (Ime, Prezime, NazivDrzave, DatumRodjenja, DatumPocetka, DatumZavrsetka, KlubID) " +
                "VALUES (@ime, @prezime, @drzava, @rodj, @poc, @kraj, @klub)";

            komanda.Parameters.AddWithValue("@ime", textBox1.Text);
            komanda.Parameters.AddWithValue("@prezime", textBox2.Text);
            komanda.Parameters.AddWithValue("@drzava", textBox3.Text);
            komanda.Parameters.AddWithValue("@rodj", dateTimePicker1.Value);
            komanda.Parameters.AddWithValue("@poc", dateTimePicker2.Value);
            komanda.Parameters.AddWithValue("@kraj", dateTimePicker3.Value);
            komanda.Parameters.AddWithValue("@klub", textBox4.Text);

            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspesno uneti podaci!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom unosa podataka: " + ex.Message);
            }
            finally
            {
                konekcija.Close();
            }
        }
    }
}
