using System;
using System.Collections.Generic;
using System.IO;

namespace UtazasiIrodaProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Utas> utasok = new List<Utas>();
            List<Utazas> utazasok = new List<Utazas>();
            string menu = "0";
            while (menu != "6")
            {
                Console.WriteLine("1. Új utas felvétele\n2. Utas adatainak módosítása\n3. Új utazás felvétele\n4. Utazásra jelentkezés\n5. Előleg befizetése és módosítása\n6. Kilépés");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        Utas u = new Utas();
                        Console.Write("Add meg az utas nevét: ");
                        u.Nev = Console.ReadLine();
                        Console.Write("Add meg az utas címét: ");
                        u.Cim = Console.ReadLine();
                        Console.Write("Add meg az utas telefonszámát: ");
                        u.Telefonszam = Console.ReadLine();
                        utasok.Add(u);
                        break;
                    case "2":
                        Console.Write("Add meg az utas nevét, akinek az adatait módosítani szeretnéd: ");
                        string nev = Console.ReadLine();
                        bool talalt = false;
                        foreach (Utas utas in utasok)
                        {
                            if (utas.Nev == nev)
                            {
                                Console.Write("Add meg az új címet: ");
                                utas.Cim = Console.ReadLine();
                                Console.Write("Add meg az új telefonszámot: ");
                                utas.Telefonszam = Console.ReadLine();
                                talalt = true;
                                break;
                            }
                        }
                        if (!talalt)
                        {
                            Console.WriteLine("Nincs ilyen nevű utas az adatbázisban!");
                        }
                        break;
                    case "3":
                        Utazas ut = new Utazas();
                        Console.Write("Add meg az úticélt: ");
                        ut.Uticel = Console.ReadLine();
                        Console.Write("Add meg az árat: ");
                        ut.Ar = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Add meg a maximális létszámot: ");
                        ut.MaxLetszam = Convert.ToInt32(Console.ReadLine());
                        utazasok.Add(ut);
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
