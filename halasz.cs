using System;
using System.Collections.Generic;

class Program
{
    struct Utas
    static List<Utas> utasok = new List<Utas>();

    static void Main(string[] args)
    {
        bool kilepes = false;
        while (!kilepes)
        {
            Console.WriteLine("1 - Új utas felvétele");
            Console.WriteLine("2 - Utas adatainak módosítása");
            Console.WriteLine("3 - Kilépés");
            Console.WriteLine();

            Console.Write("Válasszon egy menüpontot: ");
            string valasztas = Console.ReadLine();
            Console.WriteLine();

            switch (valasztas)
            {
                case "1":
                    UjUtasFelvetele();
                    break;
                case "2":
                    UtasAdatokModositasa();
                    break;
                case "3":
                    kilepes = true;
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás!");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void UjUtasFelvetele()
    {
        Console.WriteLine("Új utas felvétele");
        Console.WriteLine();

        Console.Write("Név: ");
        string nev = Console.ReadLine();

        Console.Write("Cím: ");
        string cim = Console.ReadLine();

        Console.Write("Telefonszám: ");
        string telefonszam = Console.ReadLine();

        Utas ujUtas = new Utas(nev, cim, telefonszam);
        utasok.Add(ujUtas);

        Console.WriteLine();
        Console.WriteLine("Az utas sikeresen felvéve!");

    }

    static void UtasAdatokModositasa()
    {

    }
}

