using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Fudbalski_Stadioni_A12
{
    public partial class Statistika : Form
    {
        public Statistika()
        {
            InitializeComponent();
            PrikaziGrafikon();
        }

        void PrikaziGrafikon()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|FudbalskiStadioni12.mdf;Integrated Security=True;Connect Timeout=30";
            string select = @"
                SELECT Stadion.Naziv AS Stadion, COUNT(*) AS BrojUtakmica
                FROM Utakmica
                INNER JOIN Klub AS Domacin ON Utakmica.DomacinID = Domacin.KlubID
                INNER JOIN Stadion ON Domacin.StadionID = Stadion.StadionID
                WHERE YEAR(Utakmica.DatumIgranja) = YEAR(GETDATE()) - 2 
                GROUP BY Stadion.Naziv;
            ";

            using (SqlConnection konekcija = new SqlConnection(connectionString))
            {
                using (SqlCommand komanda = new SqlCommand(select, konekcija))
                {
                    try
                    {
                        konekcija.Open();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(komanda);
                        da.Fill(dt);

                        if (dt.Rows.Count > 10)
                        {
                            DataTable dtTop10 = dt.AsEnumerable().Take(10).CopyToDataTable();
                            dt = dtTop10;
                        }

                        chart1.Series.Clear();
                        chart1.Series.Add("Utakmica");
                        chart1.Series["Utakmica"].ChartType = SeriesChartType.Column;

                        foreach (DataRow row in dt.Rows)
                        {
                            chart1.Series["Utakmica"].Points.AddXY(row["Stadion"], row["BrojUtakmica"]);
                        }
                        chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Došlo je do greške prilikom prikazivanja podataka: " + ex.Message);
                    }
                }
            }
        }
    }
}
