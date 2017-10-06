using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace BankProgram
{
    class BANK
    {
        // Fields
        private List<KUND> kunder;
        private List<KONTO> konton;
        private FILHANTERING fil_In_Ut;
        private int antalkonton;
        private int antalkunder;
        
        // Properties
        
        internal List<KUND> Kunder { get => kunder; set => kunder = value; }
        internal List<KONTO> Konton { get => konton; set => konton = value; }
        internal FILHANTERING Fil_In_Ut { get => fil_In_Ut; set => fil_In_Ut = value; }
        public int Antalkonton { get => konton.Count; set => antalkonton = value; }
        public int Antalkunder { get => kunder.Count; set => antalkunder = value; }

        // Konstruktor
        public BANK()
        {
            this.fil_In_Ut = new FILHANTERING("bankdata-small.txt");
            //this.fil_In_Ut = new FILHANTERING("bankdata.txt");
            this.kunder = new List<KUND>();
            this.konton = new List<KONTO>();
            
        }
        public void Start() // Metod för att starta upp bankprogrammet
        {
            fil_In_Ut.LäsFil();
            GenereraKundLista();
            GenereraKontoLista();
            GenereraKontonPerKund();
            MENY.MenyMetod(this);

        }

        public void Stop() // Metod för att avsluta bankprogrammet
        {
            DegenereraKundLista();
            DegenereraKontoLista();
            fil_In_Ut.SkrivFil(Antalkunder, Antalkonton);
        }
        // Metod för att generera lista med kundobjekt, anropas då banken startas
        public void GenereraKundLista()
        {
            string[] temp;

            foreach (var kunddata in fil_In_Ut.KundINdata)
            {
                temp = kunddata.Split(';');
                kunder.Add(new KUND(temp));
            }
        }
        public void DegenereraKundLista() // Anropas av Stop-metoden, formaterar för filhanteringen
        {
            StringBuilder temp = new StringBuilder();

            foreach (var kunddata in kunder)
            {
                temp.Clear();
                temp.Append(kunddata.Kundnummer.ToString());
                temp.Append(';');
                temp.Append(kunddata.Organisationnummer);
                temp.Append(';');
                temp.Append(kunddata.Företagsnamn);
                temp.Append(';');
                temp.Append(kunddata.Adress);
                temp.Append(';');
                temp.Append(kunddata.Stad);
                temp.Append(';');
                temp.Append(kunddata.Region);
                temp.Append(';');
                temp.Append(kunddata.Postnummer);
                temp.Append(';');
                temp.Append(kunddata.Land);
                temp.Append(';');
                temp.Append(kunddata.Telefonnummer);
                fil_In_Ut.KundUTdata.Add(temp.ToString());
            }
        }
        public void DegenereraKontoLista() // Anropas av Stop-metoden, formaterar för filhanteringen
        {
            StringBuilder temp = new StringBuilder();

            foreach (var kontodata in konton)
            {
                temp.Clear();
                temp.Append(kontodata.Kontonummer.ToString());
                temp.Append(';');
                temp.Append(kontodata.Kundnummer.ToString());
                temp.Append(';');
                temp.Append(kontodata.Saldo.ToString(CultureInfo.InvariantCulture));
                
                fil_In_Ut.KontoUTdata.Add(temp.ToString());
            }
            
        }
        //// Metod för att generera lista med kontoobjekt, anropas då banken startas
        public void GenereraKontoLista()
        {
            string[] temp;

            foreach (var kontodata in fil_In_Ut.KontoINdata)
            {
                temp = kontodata.Split(';');

                konton.Add(new KONTO(temp));
            }
        }

        // Metod för att addera konton till kundkontolista, anropas då banken startas
        public void GenereraKontonPerKund()
        {
            foreach (var kund in kunder)
            {
                kund.Kundkonton.AddRange(konton.Where(x => x.Kundnummer == kund.Kundnummer));
            }

        }
        public void NyKund(string[] kunddata) // Skapa ny kund, CASE3 i menyklassen
        {
            KUND nykund = new KUND(kunddata, NyttKundNr(), NyttKontoNr());
            
            // Lägg till nya kunden till globala listan med kunder
            kunder.Add(nykund); 
            
            // Lägg till nya konton till globala listan med konton
            konton.Add(nykund.Kundkonton[nykund.Kundkonton.Count - 1]);
            Console.WriteLine("Ny kund med nummer {0} skapades",nykund.Kundnummer);
            Console.WriteLine("Kunden fick ett konto med nummer {0}", nykund.Kundkonton[0].Kontonummer);

        }
        public void NyttKonto(int kundnr) // Skapa nytt konto, CASE5 i menyklassen
        {
            // Hitta index för kundnumret i listan med kunder 
            var temp = Kunder.FindIndex(x => x.Kundnummer == kundnr);
            
            // Lägg till nya kontot till kundkontolistan
            kunder[temp].Kundkonton.Add(new KONTO(NyttKontoNr(),kundnr));
            
            // Lägg till nya kontot till globala listan med konton
            konton.Add(kunder[temp].Kundkonton[kunder[temp].Kundkonton.Count - 1]);

            Console.WriteLine("Konto med nummer {0} skapades för kund {1}",konton[konton.Count-1].Kontonummer,konton[konton.Count-1].Kundnummer);

        }

        public void TaBortKund(int kundnr) // Ta bort kund (Case4 i menyn)
        {
            int temp = kunder.FindIndex(x => x.Kundnummer == kundnr);

            foreach (var konto in kunder[temp].Kundkonton)
            {
                // Ta bort alla kundens konton från globala listan med alla bankkonton
                konton.Remove(konton.Find(x => x.Kontonummer == konto.Kontonummer));
            }

            // Ta bort kunden från listan med kunder
            kunder.Remove(kunder[temp]);
        }

        public void TaBortKonto(int kundnr, int kontonr) // Case 6
        {
            // Hitta index för kontonumret i global listan med konton 
            var temp1 = Konton.FindIndex(x => x.Kontonummer == kontonr);

            // Hitta index för kundnumret i global listan med kunder 
            var temp2 = Kunder.FindIndex(x => x.Kundnummer == kundnr);

            // Hitta index för kontonumret i kundkontolistan 
            var temp3 = Kunder[temp2].Kundkonton.FindIndex(x => x.Kontonummer == kontonr);

            Console.WriteLine("Kontot {0} togs bort från kund {1}",kontonr,kundnr);

            // Ta bort kontot från kundkontolistan
            kunder[temp2].Kundkonton.Remove(Kunder[temp2].Kundkonton[temp3]);

            // Ta bort kontot från globala listan med konton
            konton.Remove(konton[temp1]);


        }
        public void Insättning(int kontonr, decimal summa) // Case 7
        {
            // Hitta index för kontonumret i global listan med konton 
            var temp1 = Konton.FindIndex(x => x.Kontonummer == kontonr);

            konton[temp1].Saldo += summa; // Addera summan till kontot

            Console.WriteLine($"{summa} kr sattes in på konto {kontonr}");
        }
        public bool Uttag(int kontonr, decimal summa) // Case 8
        {
            // Hitta index för kontonumret i global listan med konton 
            var temp = Konton.FindIndex(x => x.Kontonummer == kontonr);

            if (summa <= konton[temp].Saldo)
            {
                konton[temp].Saldo -= summa; // Subtrahera summan från kontot om tillräckligt med pengar finns
                return true;
            }
            return false;

        }
        public void Överföring(int frånkontonr, int tillkontonr, decimal summa) // Case9
        {
            // Hitta index för kontonumret(från) i global listan med konton 
            var temp1 = Konton.FindIndex(x => x.Kontonummer == frånkontonr);

            // Hitta index för kontonumret(till) i global listan med konton 
            var temp2 = Konton.FindIndex(x => x.Kontonummer == tillkontonr);

            konton[temp1].Saldo -= summa; // Subtrahera summan från frånkontot
            konton[temp2].Saldo += summa; // Addera summan till destinationskontot

        }        

        public int NyttKundNr() // Returnera nytt kundnr (högsta kundnr i listan + 1)
        {
            List<KUND> temp = new List<KUND>();
            temp = kunder.OrderByDescending(x => x.Kundnummer).ToList();
            return temp[0].Kundnummer + 1;
        }
        public int NyttKontoNr() // Returnera nytt kontonr (högsta kontonr i listan + 1)
        {
            List<KONTO> temp = new List<KONTO>();
            temp = konton.OrderByDescending(x => x.Kontonummer).ToList();
            return temp[0].Kontonummer + 1;
        }
        
        public decimal TotaltSaldo() // Anropas av menyklassen då menyn skrivs ut
        {
            decimal temp = 0.0m;
            foreach (var konto in Konton)
            {
                temp += konto.Saldo;
            }
            return temp;
        }
        
        public bool SkrivUtSökn(string sökstr)  // Anropas från "Case1" i menymetoden 
        {

            string sökstr2;
            StringBuilder sb = new StringBuilder();
            sb.Append(sökstr);
            sb.Insert(0, " ");
            sökstr2 = sb.ToString().ToUpper(); // sökstr2 används för att få träff på ord i företagsnamn som består
            // av flera ord
            int count = 0;
            foreach (var kund in kunder.Where(x => x.Företagsnamn.ToUpper().StartsWith(sökstr.ToUpper()) ||
                                                   x.Företagsnamn.ToUpper().Contains(sökstr2)
                                                   || x.Postnummer.Contains(sökstr.ToUpper())))
            {
                kund.SkrivUtKundNr();
                count++;
            }

            if (count == 0) return false;
            else return true;
     
        }
    }
}




// Metod för att skriva till fil med bankdata


    

