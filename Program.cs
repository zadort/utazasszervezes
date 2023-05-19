using System;
using System.Collections.Generic;
using System.IO;

class Utazo
{
    public string Nev { get; set; }
    public string Cim { get; set; }
    public string Telefonszam { get; set; }
}

class UtiCel
{
    public string Nev { get; set; }
    public decimal Ar { get; set; }
    public int MaxLetszam { get; set; }
    public List<Utazo> Jelentkezok { get; set; } = new List<Utazo>();
}

class UtazasiIroda
{
    private List<Utazo> utasok = new List<Utazo>();
    private List<UtiCel> utazasok = new List<UtiCel>();

    public void UjUtas(string nev, string cim, string telefonszam)
    {
        Utazo utas = new Utazo
        {
            Nev = nev,
            Cim = cim,
            Telefonszam = telefonszam
        };

        utasok.Add(utas);
    }

    public void Modositas(string nev, string ujCim, string ujTelefonszam)
    {
        Utazo utas = utasok.Find(u => u.Nev == nev);
        if (utas != null)
        {
            utas.Cim = ujCim;
            utas.Telefonszam = ujTelefonszam;
        }
        else
        {
            Console.WriteLine("Nem található utas ezzel a névvel.");
        }
    }

    public void UjUt(string nev, decimal ar, int maxLetszam)
    {
        UtiCel ut = new UtiCel
        {
            Nev = nev,
            Ar = ar,
            MaxLetszam = maxLetszam
        };

        utazasok.Add(ut);
    }
}
