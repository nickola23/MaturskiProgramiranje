using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVD_Kolekcija
{
    internal class Producent
    {
        public string ime, sifra, email;
        public Producent(string sifra, string ime, string email)
        {
            this.ime = ime;
            this.sifra = sifra;
            this.email = email;
        }
        public override string ToString()
        {
            return sifra.PadRight(5 - sifra.Length) + "\t" + ime.PadRight(45 - ime.Length) + "\t" + email.PadRight(40 - email.Length);
        }

    }
}
