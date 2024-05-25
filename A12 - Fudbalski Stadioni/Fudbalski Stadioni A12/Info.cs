using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fudbalski_Stadioni_A12
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            label1.Text = "Aplikacija sadrži podatake o svim stadionima kao i o broju utakmica održanih na datim stadionima. \n\nU slčaju pogrešnog unosa informacija sve podatke je moguće izmeniti. \n\nDugme 'Pretraga' omogućava korisniku lakše pretraživanje stadiona prema državi u kojoj se nalaze. \n\nInfo forma prikazuje uputstvo za korišćenje same aplikacije. \n\nForma 'statistika' grafički prikazuje broj održanih utakmica na datom stadionu u toku prošle godine.";
        }
    }
}
