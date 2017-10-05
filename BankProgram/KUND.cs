using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram
{
    class KUND
    {

        // Fields

        private int kundnummer;
        private string organisationnummer;
        private string företagsnamn;
        private string adress;
        private string stad;
        private string region;
        private string postnummer;
        private string land;
        private string telefonnummer;
        private List<KONTO> kundkonton;
        
        // Properties
        public int Kundnummer { get => kundnummer; set => kundnummer = value; }
        public string Organisationnummer { get => organisationnummer; set => organisationnummer = value; }
        public string Företagsnamn { get => företagsnamn; set => företagsnamn = value; }
        public string Adress { get => adress; set => adress = value; }
        public string Stad { get => stad; set => stad = value; }
        public string Region { get => region; set => region = value; }
        public string Postnummer { get => postnummer; set => postnummer = value; }
        public string Land { get => land; set => land = value; }
        public string Telefonnummer { get => telefonnummer; set => telefonnummer = value; }
        internal List<KONTO> Kundkonton { get => kundkonton; set => kundkonton = value; }

        // Constructor

        public KUND(string[] kunddata)
        {
            this.kundnummer = int.Parse(kunddata[0]);
            this.organisationnummer = kunddata[1];
            this.företagsnamn = kunddata[2];
            this.adress = kunddata[3];
            this.stad = kunddata[4];
            this.Region = kunddata[5];
            this.Postnummer = kunddata[6];
            this.Land = kunddata[7];
            this.Telefonnummer = kunddata[8];
            this.Kundkonton = new List<KONTO>();
        }

        public void SkrivUtKundBild()
        {
            Console.WriteLine(kundnummer);
            Console.WriteLine(organisationnummer);
            Console.WriteLine(företagsnamn);
            Console.WriteLine(adress);
            Console.WriteLine(stad);
            Console.WriteLine(region);
            Console.WriteLine(postnummer);
            Console.WriteLine(land);
            Console.WriteLine(telefonnummer);

            foreach (var item in Kundkonton)
            {
                Console.WriteLine(item.Kontonummer);
            }

        }

        public void SkrivUtKundNr()
        {
            Console.WriteLine("{0}: {1}",kundnummer,företagsnamn);
        }

    }
}
