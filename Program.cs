using System;
using System.Collections.Generic;
using System.IO;

namespace utazasszervezes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Utazásszervezés";
            List<Utas> utasok = new List<Utas>(); // Az eddig felvett utasokat tároló lista
            List<Utazas> utazasok = new List<Utazas>(); // Az eddig felvitt utazásokat tároló lista
            string menu = "0"; // A menüpont kiválasztására szolgáló változó
            while (menu != "6") // Amíg a felhasználó nem választja a kilépést
            {
                Console.Clear();
                Console.WriteLine("1. Új utas felvétele\n2. Utas adatainak módosítása\n3. Új utazás felvétele\n4. Utazásra jelentkezés\n5. Előleg befizetése és módosítása\n6. Kilépés"); // Menü kiírása
                menu = Console.ReadLine(); // Menüpont kiválasztása
                Console.Clear();
                switch (menu) // A kiválasztott menüpont alapján való elágazás
                {
                    case "1":
                        Utas u = new Utas(); // Új Utas objektum létrehozása
                        Console.Write("Add meg az utas nevét: ");
                        u.Nev = Console.ReadLine(); // Az utas nevének beolvasása
                        Console.Write("Add meg az utas címét: ");
                        u.Cim = Console.ReadLine(); // Az utas címének beolvasása
                        Console.Write("Add meg az utas telefonszámát: ");
                        u.Telefonszam = Console.ReadLine(); // Az utas telefonszámának beolvasása
                        utasok.Add(u); // Az új utas hozzáadása a listához
                        break;
                    case "2":
                        Console.Write("Add meg az utas nevét, akinek az adatait módosítani szeretnéd: ");
                        string nev = Console.ReadLine(); // Az utas nevének beolvasása
                        bool talalt = false; // Változó az utas kereséséhez
                        foreach (Utas utas in utasok) // Végigmegyünk az összes utason a listában
                        {
                            if (utas.Nev == nev) // Ha az utas neve megegyezik a keresettel
                            {
                                Console.Write("Add meg az új címet: ");
                                utas.Cim = Console.ReadLine(); // Az új cím beolvasása
                                Console.Write("Add meg az új telefonszámot: ");
                                utas.Telefonszam = Console.ReadLine(); // Az új telefonszám beolvasása
                                talalt = true; // A keresett utas megtalálva
                                break;
                            }
                        }
                        if (!talalt) // Ha nem találtuk meg az utast
                        {
                            Console.WriteLine("Nincs ilyen nevű utas az adatbázisban!");
                        }
                        break;
                    case "3":
                        Utazas ut = new Utazas(); // Új Utazas objektum létrehozása
                        Console.Write("Add meg az úticélt: ");
                        ut.Uticel = Console.ReadLine(); // Az uticél beolvasása
                        Console.Write("Add meg az árat: ");
                        ut.Ar = Convert.ToInt32(Console.ReadLine()); // Az ár beolvasása
                        Console.Write("Add meg a maximális létszámot: ");
                        ut.MaxLetszam = Convert.ToInt32(Console.ReadLine()); // A maximális létszám beolvasása
                        utazasok.Add(ut); // Az új utazás hozzáadása a listához
                        break;
                    case "4":
                        Console.Write("Add meg az utazás azonosítóját: ");
                        int azonosito = Convert.ToInt32(Console.ReadLine());
                        Utazas utazas = utazasok[azonosito - 1];
                        if (utazas.Jelentkezesek.Count >= utazas.MaxLetszam)
                        {
                            Console.WriteLine("Az utazásra sajnos nem lehet jelentkezni, mert betelt!");
                        }
                        else
                        {
                            Console.Write("Add meg az utas nevét: ");
                            string nev2 = Console.ReadLine();
                            bool talalt2 = false;
                            foreach (Utas utas in utasok)
                            {
                                if (utas.Nev == nev2)
                                {
                                    Jelentkezes jel = new Jelentkezes();
                                    jel.Utas = utas;
                                    Console.Write("Add meg a befizetett előleget: ");
                                    jel.Eloleg = Convert.ToInt32(Console.ReadLine());
                                    if (jel.Eloleg > utazas.Ar)
                                    {
                                        Console.WriteLine("Az előleg nem lehet nagyobb, mint az út ára!");
                                    }
                                    else
                                    {
                                        utazas.Jelentkezesek.Add(jel);
                                        Console.WriteLine("Sikeres jelentkezés!");
                                    }
                                    talalt2 = true;
                                    break;
                                }
                            }
                            if (!talalt2)
                            {
                                Console.WriteLine("Nincs ilyen nevű utas az adatbázisban!");
                            }
                        }
                        break;
                    case "5":
                        Console.Write("Add meg az utas nevét: ");
                        string nev3 = Console.ReadLine();
                        bool talalt3 = false;
                        foreach (Utazas utazas2 in utazasok)
                        {
                            foreach (Jelentkezes jel in utazas2.Jelentkezesek)
                            {
                                if (jel.Utas.Nev == nev3)
                                {
                                    Console.WriteLine("Az utas jelentkezett a következő utazásra: {0}", utazas2.Uticel);
                                    Console.WriteLine("Az előleg összege: {0}", jel.Eloleg);
                                    Console.Write("Add meg az új előleget: ");
                                    int ujEloleg = Convert.ToInt32(Console.ReadLine());
                                    if (ujEloleg > utazas2.Ar)
                                    {
                                        Console.WriteLine("Az előleg nem lehet nagyobb, mint az út ára!");
                                    }
                                    else
                                    {
                                        jel.Eloleg = ujEloleg;
                                        Console.WriteLine("Az előleg sikeresen módosítva!");
                                    }
                                    talalt3 = true;
                                    break;
                                }
                            }
                            if (talalt3)
                            {
                                break;
                            }
                        }
                        if (!talalt3)
                        {
                            Console.WriteLine("Nincs ilyen nevű utas az adatbázisban, vagy még nem jelentkezett egyetlen utazásra sem!");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Viszlát!");
                        break;
                    default:
                        Console.WriteLine("Nincs ilyen menüpont!");
                        break;
                }
                // utaslista kiírása fájlba
                using (StreamWriter file = new StreamWriter("utasok.txt"))
                {
                    foreach (Utas utas in utasok)
                    {
                        file.WriteLine("Név: {0}, Cím: {1}, Telefonszám: {2}", utas.Nev, utas.Cim, utas.Telefonszam);
                    }
                }
            }
        }
    }

    class Utas
    {
        public string Nev { get; set; }
        public string Cim { get; set; }
        public string Telefonszam { get; set; }
    }

    class Utazas
    {
        public string Uticel { get; set; }
        public int Ar { get; set; }
        public int MaxLetszam { get; set; }
        public List<Jelentkezes> Jelentkezesek { get; set; }

        public Utazas()
        {
            Jelentkezesek = new List<Jelentkezes>();
        }
    }

    class Jelentkezes
    {
        public Utas Utas { get; set; }
        public int Eloleg { get; set; }
    }
}
