﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram
{
    static class MENY
    {
        
        public static void MenyMetod(BANK banken)
        {
            Console.Clear();
            SkrivMeny(banken);
            while (true)
            {

                Console.Write("> ");
                string caseSwitch = Console.ReadLine();

                switch (caseSwitch)
                {
                    
                    case "0":
                        Console.WriteLine("Avsluta och spara");
                        // Metod för att avsluta och spara till fil
                        // a,b
                        break;

                    case "1": // Sök på kundnamn eller postort
                        Case1(banken);
                        break;

                    case "2": // Visa "kundbild"
                        Case2(banken);
                        break;
                        
                    case "3":
                        Console.WriteLine("Skapa kund");
                        // Metod som skapar kund
                        break;
                    case "4":
                        Console.WriteLine("Ta bort kund");
                        // Metod som tar bort kund (går endast om saldo är noll på kontona
                        break;
                    case "5":
                        Console.WriteLine("Skapa konto");
                        // Metod som skapar konto
                        break;
                    case "6":
                        Console.WriteLine("Ta bort konto");
                        // Metod som tar bort konto
                        break;
                    case "7":
                        Console.WriteLine("Insättning");
                        // Metod som sätter in pengar på konto +saldo
                        break;
                    case "8":
                        Console.WriteLine("Uttag");
                        // Metod som tar ut pengar -saldo
                        break;
                    case "9":
                        Console.WriteLine("Överföring");
                        // Metod som för över pengar från ett kundkonto till annat kundkonto
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
        }

        

        private static void Case1(BANK banken) // Sök på kundnamn eller postort
        {
            Console.WriteLine("* Sök kund *");
            Console.Write("Namn eller postort? ");
            var a = Console.ReadLine();
            banken.SkrivUtSökn(a);
        }

        private static void Case2(BANK banken) // Visa "kundbild"
        {
            Console.Write("Kundnummer? ");
            var a = int.Parse(Console.ReadLine());
            var b = banken.Kunder.FindIndex(x => x.Kundnummer == a);
            banken.Kunder[b].SkrivUtKundBild();
        }
        public static void SkrivMeny(BANK banken)
        {
            
            Console.WriteLine("******************************");
            Console.WriteLine("* VÄLKOMMEN TILL BANKAPP 1.0 *");
            Console.WriteLine("******************************");

            Console.WriteLine("Läser in bankdata.txt...");
            Console.WriteLine("Antal kunder: {0}",banken.Kunder.Count);
            Console.WriteLine("Antal konton: {0}", banken.Konton.Count);
            Console.WriteLine("Totalt saldo: {0}", banken.TotaltSaldo());
            Console.WriteLine();

            Console.WriteLine("0) Avsluta och spara");
            Console.WriteLine("1) Sök kund");
            Console.WriteLine("2) Visa kundbild");
            Console.WriteLine("3) Skapa kund");
            Console.WriteLine("4) Ta bort kund");
            Console.WriteLine("5) Skapa konto");
            Console.WriteLine("6) Ta bort konto");
            Console.WriteLine("7) Insättning");
            Console.WriteLine("8) Uttag");
            Console.WriteLine("9) Överföring");
            Console.WriteLine();
        }
    }
}
