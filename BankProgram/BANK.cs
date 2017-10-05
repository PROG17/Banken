using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
        
        // Properties
        
        internal List<KUND> Kunder { get => kunder; set => kunder = value; }
        internal List<KONTO> Konton { get => konton; set => konton = value; }
        internal FILHANTERING Fil_In_Ut { get => fil_In_Ut; set => fil_In_Ut = value; }

        // Konstruktor
        public BANK()
        {
            //this.fil_In_Ut = new FILHANTERING("bankdata-small.txt");
            this.fil_In_Ut = new FILHANTERING("bankdata.txt");
            this.kunder = new List<KUND>();
            this.konton = new List<KONTO>();
            
        }
        // Huvudmetod i banken
        public void Start()
        {
            fil_In_Ut.LäsFil();
            GenereraKundLista();
            GenereraKontoLista();
            GenereraKontonPerKund();
            MENY.MenyMetod(this);

        }
        // Metod för att generera lista med kundobjekt
        public void GenereraKundLista()
        {
            string[] temp;
            foreach (var kunddata in fil_In_Ut.KundINdata)
            {
                temp = kunddata.Split(';');
                kunder.Add(new KUND(temp));
            }
            //return Kunder;
        }
        //// Metod för att generera lista med kontoobjekt
        public void GenereraKontoLista()
        {
            string[] temp;
            foreach (var kontodata in fil_In_Ut.KontoINdata)
            {
                temp = kontodata.Split(';');

                konton.Add(new KONTO(temp));
            }
            //return Konton;

        }

        public void NyKund(string[] kunddata) // Skapa ny kund, anropas från CASE3 menyklassen
        {
            KUND nykund = new KUND(kunddata, NyttKundNr(), NyttKontoNr());
            kunder.Add(nykund); // Lägg till nya kunden till globala listan med kunder
            konton.Add(nykund.Kundkonton[nykund.Kundkonton.Count - 1]); // Lägg till nya konton till globala listan med konton
            
        }

        public void TaBortKund(int kundnr) // Ta bort kund (Case4 i menyn)
        {
            int temp = kunder.FindIndex(x => x.Kundnummer == kundnr);

            foreach (var konto in kunder[temp].Kundkonton)
            {
                //var temp2 = 
                konton.Remove(konton.Find(x => x.Kontonummer == konto.Kontonummer));
                //konton.Remove(konto);
            }

            kunder.Remove(kunder[temp]);
        }

        public void NyttKonto() // Skapa nytt konto
        {
            //konton.Add(new KONTO(NyttKontoNr(),));
        }
        // Metod för att addera konton till kundkontolista
        public void GenereraKontonPerKund()
        {
            foreach (var kund in kunder)
            {
                kund.Kundkonton.AddRange(konton.Where(x => x.Kundnummer == kund.Kundnummer));
            }            

        }

        public int NyttKundNr() // Returnera nytt kundnr
        {
            List<KUND> temp = new List<KUND>();
            temp = kunder.OrderByDescending(x => x.Kundnummer).ToList();
            return temp[0].Kundnummer + 1;
        }
        public int NyttKontoNr() // Returnera nytt kontonr
        {
            List<KONTO> temp = new List<KONTO>();
            temp = konton.OrderByDescending(x => x.Kontonummer).ToList();
            return temp[0].Kontonummer + 1;
        }
        // Anropas av menyklassen
        public decimal TotaltSaldo()
        {
            decimal temp = 0.0m;
            foreach (var konto in Konton)
            {
                temp += konto.Saldo;
            }
            return temp;
        }
        
        // Anropas från "Case1" i menymetoden, sök på företagsnamn och postort
        public void SkrivUtSökn(string sökstr) 
        {

            string sökstr2;
            StringBuilder sb = new StringBuilder();
            sb.Append(sökstr);
            sb.Insert(0, " ");
            sökstr2 = sb.ToString().ToUpper(); // sökstr2 används för att få träff på ord i företagsnamn som består
            // av flera ord

            foreach (var kund in kunder.Where(x => x.Företagsnamn.ToUpper().StartsWith(sökstr.ToUpper()) ||
                                                   x.Företagsnamn.ToUpper().Contains(sökstr2)
                                                   || x.Postnummer.Contains(sökstr)))
            {
                kund.SkrivUtKundNr();
            }

        }
    }
}




// Metod för att skriva till fil med bankdata


    

