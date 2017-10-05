using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankProgram
{
    class FILHANTERING
    {
        //Fields
        private string filnamn;
        private List<string> kontoINdata;
        private List<string> kundINdata;

        // Properties

        public string Filnamn { get => filnamn; set => filnamn = value; }
        public List<string> KontoINdata { get => kontoINdata; set => kontoINdata = value; }
        public List<string> KundINdata { get => kundINdata; set => kundINdata = value; }

        // Konstruktor
        public FILHANTERING(string filnamn)
        {
            this.filnamn = filnamn;
            this.kundINdata = new List<string>();
            this.kontoINdata = new List<string>();
        }
        
        // Metod för att läsa fil med bankdata

        public void LäsFil()
        {
            StreamReader reader = new StreamReader(filnamn);
            
            string line;
            string line2;
            string line3;
            int antalkunder;
            int antalkonton;

            line = reader.ReadLine();
            antalkunder = Int32.Parse(line);

            for (int i = 0; i < antalkunder; i++)
            {
                line2 = reader.ReadLine();
                kundINdata.Add(line2);
            }

            line3 = reader.ReadLine();
            antalkonton = Int32.Parse(line3);

            for (int i = 0; i < antalkonton; i++)
            {
                line3 = reader.ReadLine();
                kontoINdata.Add(line3);
            }

            reader.Close();
            
        }

       // public void Hämta

        // Metod för att skriva till fil med bankdata
    }
}
