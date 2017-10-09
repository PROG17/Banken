using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BankProgram
{
    static class MENY
    {
        
        public static void MenyMetod(BANK banken)
        {
            Console.Clear();
            bool bankenigång = true;
            SkrivMeny(banken);
            
            do
            {

                Console.Write("> ");
                string caseSwitch = Console.ReadLine();

                switch (caseSwitch)
                {
                    
                    case "0":
                        Case0(banken);
                        // Metod för att avsluta och spara till fil
                        bankenigång = false;
                        break;

                    case "1": // Sök på kundnamn eller postort
                        Case1(banken);
                        break;

                    case "2": // Visa "kundbild"
                        Case2(banken);
                        break;
                        
                    case "3":
                        Case3(banken); // Skapa kund 
                        break;
                    case "4":
                        Case4(banken); // Ta bort kund (går endast om saldo är noll på kontona)
                        break;
                    case "5":
                        Case5(banken); // Skapa nytt konto
                        break;
                    case "6":
                        Case6(banken); // Ta bort konto (endast om saldo = 0)
                        break;
                    case "7":
                        Case7(banken); // Insättning +saldo
                        break;
                    case "8":
                        Case8(banken); // Uttag -saldo
                        break;
                    case "9":
                        Case9(banken); // Överföring
                        break;
                    default:
                        Console.WriteLine("Ange val 0-9 i siffror");
                        break;
                }
            } while (bankenigång);
        }

        private static void Case0(BANK banken) // Avsluta och spara
        {
            Console.WriteLine("* Avsluta och spara *");
            Console.WriteLine("Antal kunder: {0}", banken.Kunder.Count);
            Console.WriteLine("Antal konton: {0}", banken.Konton.Count);
            Console.WriteLine("Totalt saldo: {0}", banken.TotaltSaldo());
            banken.Stop();
        }

        private static void Case1(BANK banken) // Sök på kundnamn eller postort
        {
            
            Console.WriteLine("* Sök kund *");
            Console.Write("Namn eller postort? ");
            var a = Console.ReadLine();
            if (banken.SkrivUtSökn(a) == false)
            {
                Console.WriteLine("Inga kunder med funna");
            }
            
            
        }

        private static void Case2(BANK banken) // Visa "kundbild"
        {
            Console.WriteLine("* Visa kundbild *");
            Console.Write("Kundnummer? ");
            int a;
            var temp = int.TryParse(Console.ReadLine(), out a);
            if (temp)
            {
                if (banken.Kunder.Exists(x => x.Kundnummer == a))
                {
                    var b = banken.Kunder.FindIndex(x => x.Kundnummer == a);
                    banken.Kunder[b].SkrivUtKundBild();
                }
                else Console.WriteLine("Det kundnumret finns inte!");

            }
            else Console.WriteLine("Ange kundnumret i siffror!");
            
        }
        private static void Case3(BANK banken) // Skapa kund
        {
            Console.WriteLine("* Lägg till kund *");
            Console.Write("Ange organisationnummer: ");
            var a = Console.ReadLine();
            Console.Write("Ange företagsnamn: ");
            var b = Console.ReadLine();
            Console.Write("Ange adress: ");
            var c = Console.ReadLine();
            Console.Write("Ange stad: ");
            var d = Console.ReadLine();
            Console.Write("Ange region: ");
            var e = Console.ReadLine();
            Console.Write("Ange postnummer: ");
            var f = Console.ReadLine();
            Console.Write("Ange land: ");
            var g = Console.ReadLine();
            Console.Write("Ange telefonnummer: ");
            var h = Console.ReadLine();

            string[] stringarray = new[] {a, b, c, d, e, f, g, h};

            banken.NyKund(stringarray);
            Console.WriteLine();

        }
        private static void Case4(BANK banken) // Ta bort kund, ska endast gå om saldot är nolld
        {
            Console.WriteLine("* Ta bort kund *");
            Console.Write("Kundnummer? ");
            int a;
            var temp = int.TryParse(Console.ReadLine(), out a);
            if (temp)
            {
                if (banken.Kunder.Exists(x => x.Kundnummer == a))
                {
                    if (banken.Kunder.Exists(x => x.Kundkonton.Count == 0))
                        banken.TaBortKund(a);
                    else Console.WriteLine("Det går inte att ta bort en kund som har konton!");
                }
                else Console.WriteLine("Det kundnumret finns inte!");
            }
            else Console.WriteLine("Ange kundnumret i siffror!");
        }
        private static void Case5(BANK banken) // Skapa konto
        {
            Console.WriteLine("* Skapa konto *");
            Console.Write("Kundnummer? ");
            int a;
            var temp = int.TryParse(Console.ReadLine(), out a);
            if (temp)
            {
                if (banken.Kunder.Exists(x => x.Kundnummer == a))
                {
                    banken.NyttKonto(a);
                }
                else Console.WriteLine("Det kundnumret finns inte!");
            }
            else Console.WriteLine("Ange kundnumret i siffror!");

        }

        private static void Case6(BANK banken) // Ta bort ett konto, ska endast gå om saldot är nolld
        {
            Console.WriteLine("* Ta bort konto *");
            Console.Write("Kundnummer? ");
            int a;
            int c;
            var temp = int.TryParse(Console.ReadLine(), out a);
            if (temp)
            {
                if (banken.Kunder.Exists(x => x.Kundnummer == a))
                {
                    var b = banken.Kunder.FindIndex(x => x.Kundnummer == a);
                    banken.Kunder[b].SkrivUtKundKonton();
                    Console.Write("Kontonummer? ");
                    temp = int.TryParse(Console.ReadLine(), out c);

                    if (temp)
                    {
                        if (banken.Konton.Exists(x => x.Kontonummer == c))
                        {
                            if (banken.Kunder[b].Kundkonton.Any(x => x.Kontonummer == c && x.Saldo == 0.00m))
                            banken.TaBortKonto(a, c); // int kundnr, int kontonr
                            else Console.WriteLine("Det går inte att ta bort ett konto med ett positivt saldo!");
                        } 
                        else Console.WriteLine("Det kontonumret existerar inte!");
                    }
                    else Console.WriteLine("Ange kontonumret i siffror!");
                }
                else Console.WriteLine("Det kundnumret finns inte!");
            }
            else Console.WriteLine("Ange kundnumret i siffror!");
        }
        private static void Case7(BANK banken) // Sätta in pengar
        {
            Console.WriteLine("* Insättning *");
            Console.Write("Kontonummer? ");
            int a;
            decimal b;
            var temp = int.TryParse(Console.ReadLine(), out a);
            if (temp)
            {
                Console.Write("Summa? ");
                temp = decimal.TryParse(Console.ReadLine(), out b);
                if (temp)
                {
                    if (b > 0.00m)
                        banken.Insättning(a, b); // int kontonr, decimal summa
                    else Console.WriteLine("Summan måste vara positiv!");
                }
                else Console.WriteLine("Skriv summan med siffror och kommatecken för decimaler!");
            }
            else Console.WriteLine("Ange kundnumret i siffror!");
        }
        private static void Case8(BANK banken) // Ta ut pengar
        {
            Console.WriteLine("* Uttag *");
            Console.Write("Kontonummer? ");
            int a;
            decimal b;
            var temp = int.TryParse(Console.ReadLine(), out a);
            if (temp)
            {
                Console.Write("Summa? ");
                temp = decimal.TryParse(Console.ReadLine(), out b);
                if (temp)
                {
                    if (banken.Konton.Exists(x => x.Kontonummer == a))
                    {
                        if (b > 0.00m)
                        {
                            if (banken.Uttag(a, b)) // int kontonr, decimal summa
                                Console.WriteLine($"{b} kr togs ut från konto {a} ");
                            else Console.WriteLine("Summan måste vara mindre eller lika med saldot");
                        }
                        else Console.WriteLine("Summan måste vara positiv!");
                    }
                    else Console.WriteLine("Det kontonumret existerar inte!");
                }
                else Console.WriteLine("Skriv summan med siffror och kommatecken för decimaler!");
            }
            
        }
        private static void Case9(BANK banken) // Överföring
        {
            int a;
            int b;
            decimal c;
            Console.WriteLine("* Överföring *");

            Console.Write("Från? ");
            var tempA = int.TryParse(Console.ReadLine(), out a);
            
            Console.Write("Till? ");
            var tempB = int.TryParse(Console.ReadLine(), out b);

            Console.Write("Summa? ");
            var tempC = decimal.TryParse(Console.ReadLine(), out c);
            if (tempA && tempB && tempC && c > 0.00m)
            {
                if (banken.Konton.Exists(x => x.Kontonummer == a) && banken.Konton.Exists(x => x.Kontonummer == b))
                {
                    if (banken.Överföring(a, b, c)) // int frånkontonr, int tillkontonr, decimal summa
                        Console.WriteLine("Summan {0} kr överfördes från kontot {1} till kontot {2}", c, a, b);
                    else Console.WriteLine("Täckning saknas på kontot {0}", a);
                }
                else Console.WriteLine("Både kontona måste existera!");
            }
            else
            {
                Console.WriteLine("Skriv kontonummer med siffror och summa med siffror och kommatecken!");
                Console.WriteLine("Summan måste vara positiv");
            }
        }
        public static void SkrivMeny(BANK banken) // Skriv ut bankmeny
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
