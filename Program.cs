using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Detyre
{
    public string Titulli { get; set; }
    public bool EshteKryer { get; set; }

    public Detyre(string titulli)
    {
        Titulli = titulli;
        EshteKryer = false;
    }

    public abstract void MarkoSiKryer();
}

public class DetyreNormale : Detyre
{
    public DetyreNormale(string titulli) : base(titulli)
    {
    }

    public override void MarkoSiKryer()
    {
        EshteKryer = true;
    }
}

public class DetyreAvancuar : Detyre
{
    public int Prioriteti { get; set; }
    public DateTime DataSkadimit { get; set; }
    public TimeSpan KohaSkadimit { get; set; }

    public DetyreAvancuar(string titulli, int prioriteti, DateTime dataSkadimit, TimeSpan kohaSkadimit) : base(titulli)
    {
        Prioriteti = prioriteti;
        DataSkadimit = dataSkadimit;
        KohaSkadimit = kohaSkadimit;
    }

    public override void MarkoSiKryer()
    {
        EshteKryer = true;
        Prioriteti = 0;
    }
}

public class ListaDetyrave
{
    public List<Detyre> detyrat;

    public ListaDetyrave()
    {
        detyrat = new List<Detyre>();
    }

    public void ShtoDetyre(Detyre detyre)
    {
        if (detyre == null)
        {
            throw new ArgumentNullException(nameof(detyre), "Detyra nuk mund tE jetE null.");
        }
        detyrat.Add(detyre);
    }

    public void FshijDetyren(int indeksi)
    {
        if (indeksi < 1 || indeksi > detyrat.Count)
        {
            throw new IndexOutOfRangeException("Indeks i pavlefshEm pEr detyrEn.");
        }
        detyrat.RemoveAt(indeksi - 1);
    }

    public void MarkoDetyrenSiKryer(int indeksi)
    {
        if (indeksi < 1 || indeksi > detyrat.Count)
        {
            throw new IndexOutOfRangeException("Indeks i pavlefshEm pEr detyrEn.");
        }
        detyrat[indeksi - 1].MarkoSiKryer();
    }

    public void ShfaqDetyrat()
    {
        Console.WriteLine("Lista e Detyrave:");
        for (int i = 0; i < detyrat.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {detyrat[i].Titulli}");
        }
    }

    public void RenditSipasPrioritetit()
    {
        detyrat = detyrat.OrderByDescending(x => (x is DetyreAvancuar ? (x as DetyreAvancuar).Prioriteti : 0)).ToList();
    }

    public void RenditSipasSkadences()
    {
        detyrat = detyrat.OrderBy(x => (x is DetyreAvancuar ? (x as DetyreAvancuar).DataSkadimit : DateTime.MaxValue)).ToList();
    }

    public List<Detyre> GjejDetyrat(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            throw new ArgumentException("Fjala kyce nuk mund tE jetE null ose bosh.", nameof(keyword));
        }
        return detyrat.Where(x => x.Titulli.Contains(keyword)).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ListaDetyrave listaDetyrave = new ListaDetyrave();


        CheckForDeadlines(listaDetyrave);
    }

    static void CheckForDeadlines(ListaDetyrave listaDetyrave)
    {
        while (true)
        {
            DateTime currentDateTime = DateTime.Now;

            foreach (var detyre in listaDetyrave.detyrat)
            {
                if (detyre is DetyreAvancuar advancedTask)
                {
                    if (currentDateTime.Date == advancedTask.DataSkadimit.Date &&
                        currentDateTime.TimeOfDay >= advancedTask.KohaSkadimit)
                    {
                        Console.WriteLine($"Task '{detyre.Titulli}' is overdue! It's time to do it.");
                    }
                }
            }

            System.Threading.Thread.Sleep(60000);
        }
    }
}
