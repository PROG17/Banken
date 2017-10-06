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
        private List<string> kontoUTdata;
        private List<string> kundUTdata;
 
        // Properties

        public string Filnamn { get => filnamn; set => filnamn = value; }
        public List<string> KontoINdata { get => kontoINdata; set => kontoINdata = value; }
        public List<string> KundINdata { get => kundINdata; set => kundINdata = value; }
        public List<string> KontoUTdata { get => kontoUTdata; set => kontoUTdata = value; }
        public List<string> KundUTdata { get => kundUTdata; set => kundUTdata = value; }

        // Konstruktor
        public FILHANTERING(string filnamn)
        {
            this.filnamn = filnamn;
            this.kundINdata = new List<string>();
            this.kontoINdata = new List<string>();
            this.kundUTdata = new List<string>();
            this.kontoUTdata = new List<string>();
        }
        
        // Metod för att läsa fil med bankdata och mellanlagra i listor

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

        // Metod för att skriva bankdata till fil

        public void SkrivFil(int antalkunder, int antalkonton)
        {

            var filnamn2 = DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            StreamWriter writer = new StreamWriter(filnamn2);
            
            Console.WriteLine($"Sparar till {filnamn2}");
            
            writer.WriteLine(antalkunder.ToString()); // Skriv antal kunder på första raden till fil

            foreach (var kund in KundUTdata) // Skriv ut alla kunder till fil
            {
                writer.WriteLine(kund);
            }

            writer.WriteLine(antalkonton.ToString()); // Skriv ut antal konton på separat rad i filen

            foreach (var konto in KontoUTdata) // Skriv ut alla konton till fil
            {
                writer.WriteLine(konto);
            }
            
            writer.Close(); // Stäng filskrivning
        }

    }
}
