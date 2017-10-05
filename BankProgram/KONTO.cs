using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram
{
    class KONTO
    {
        // Fields
        private decimal saldo;
        private int kontonummer;
        private int kundnummer;

        // Properties
        
        public decimal Saldo { get => saldo; set => saldo = value; }
        public int Kundnummer { get => kundnummer; set => kundnummer = value; }
        public int Kontonummer { get => kontonummer; set => kontonummer = value; }

        // Constructor
        public KONTO(string[] kontodata)
        {
            this.kontonummer = int.Parse(kontodata[0]);
            this.kundnummer = int.Parse(kontodata[1]);
            this.saldo = Decimal.Parse(kontodata[2],CultureInfo.InvariantCulture);

        }


        // Metod för att skriva ut kontoinfo
        public void SkrivUtKonto()
        {
            Console.WriteLine(kontonummer);
            Console.WriteLine(kundnummer);
            Console.WriteLine(saldo);
        }
    }
}
