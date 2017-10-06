using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        public KUND(string[] kunddata) // Används när kundobjekten skapas från fildata
        {
            this.kundnummer = int.Parse(kunddata[0]);
            this.organisationnummer = kunddata[1];
            this.företagsnamn = kunddata[2];
            this.adress = kunddata[3];
            this.stad = kunddata[4];
            this.region = kunddata[5];
            this.postnummer = kunddata[6];
            this.land = kunddata[7];
            this.telefonnummer = kunddata[8];
            this.kundkonton = new List<KONTO>();
        }
        public KUND(string[] kunddata, int kundnr, int kontonr) // Används vid skapande av ny kund
        {
            this.kundnummer = kundnr;
            this.organisationnummer = kunddata[0];
            this.företagsnamn = kunddata[1];
            this.adress = kunddata[2];
            this.stad = kunddata[3];
            this.region = kunddata[4];
            this.postnummer = kunddata[5];
            this.land = kunddata[6];
            this.telefonnummer = kunddata[7];
            this.kundkonton = new List<KONTO>();
            kundkonton.Add(new KONTO(kontonr,kundnr)); // skapa nytt konto till kund
        }

        public void SkrivUtKundBild() // Anropas av menyklassen
        {
            Console.WriteLine();
            Console.WriteLine($"Kundnummer: {kundnummer}");
            Console.WriteLine($"Organisationnummer: {organisationnummer}");
            Console.WriteLine($"Företagsnamn: {företagsnamn}");
            Console.WriteLine($"Stad: {stad}");
            Console.WriteLine($"Region: {region}");
            Console.WriteLine($"Postnummer: {postnummer}");
            Console.WriteLine($"Land: {land}");
            Console.WriteLine($"Telefonnummer: {telefonnummer}");

            Console.WriteLine();
            Console.WriteLine("Konton");
            SkrivUtKundKonton();
            SkrivUtTotaltSaldo();
        }

        public void SkrivUtKundKonton()
        {
            foreach (var item in Kundkonton)
            {
                Console.WriteLine($"Kontonummer: {item.Kontonummer} Saldo: {item.Saldo} kr");
            }
        }

        public void SkrivUtKundNr()
        {
            Console.WriteLine("{0}: {1}",kundnummer,företagsnamn);
        }

        private void SkrivUtTotaltSaldo()
        {
            decimal temp = 0;
            foreach (var item in kundkonton)
            {
                temp += item.Saldo;

            }
            Console.WriteLine("Totalt saldo: {0} kr",temp);
        }

    }
}
