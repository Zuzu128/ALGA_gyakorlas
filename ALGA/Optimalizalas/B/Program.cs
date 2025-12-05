namespace ALGA_ZH_B;

class Program
{
    static void Main(string[] args)
    {
        // FaHalmaz tesztelése
        FaHalmaz<int> fa = new FaHalmaz<int>();

        // Beszúrások
        fa.Beszur(10);
        fa.Beszur(5);
        fa.Beszur(15);

        // Elemszam kiírása
        Console.WriteLine("FaElemek száma: " + fa.Elemszam); // Kiírja: 3

        // Keresés
        Console.WriteLine("Van-e 5 a fában? " + fa.Eleme(5)); // Kiírja: True
        Console.WriteLine("Van-e 20 a fában? " + fa.Eleme(20)); // Kiírja: False


        // SzamoloFaHalmaz tesztelése
        SzamoloFaHalmaz<int> fa2 = new SzamoloFaHalmaz<int>();

        // Beszúrások
        fa2.Beszur(10);
        fa2.Beszur(5);
        fa2.Beszur(15);
        fa2.Beszur(10);

        // Elemszam kiírása (figyelembe véve a darabszámokat)
        Console.WriteLine("Számoló fa elemszáma (figyelembe véve a darabszámokat): " +
                          fa2.Elemszam); // Kiírja: 4 (10 kétszer)

        // Szűrés minDarabSzam alapján (pl. darabszám >= 2)
        SzamoloFaHalmaz<int> szurtFa = fa2.Szures(2);
        Console.WriteLine("Szűrt elemszám (darabszám >= 2): " +
                          szurtFa.Elemszam); // Kiírja: 2 (mert 10 van kétszer)

        // Szures minta
        Func<int, int, bool> feltetel = (ertek, darabszam) => darabszam < ertek;

        SzamoloFaHalmaz<int> ujHalmaz = fa2.Szures(feltetel);
        Console.WriteLine("Szűrt elemszám (darabszám < érték): " + ujHalmaz.Elemszam);
    }
}