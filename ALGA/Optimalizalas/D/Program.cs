namespace ALGA_ZH_D;

class Program
{
    static void Main(string[] args)
    {
        // FaHalmaz<int> példányosítása
        FaHalmaz<int> fa = new FaHalmaz<int>();

        // Elembeszúrás
        fa.Beszur(5);
        fa.Beszur(3);
        fa.Beszur(7);
        fa.Beszur(2);
        fa.Beszur(4);
        fa.Beszur(6);
        fa.Beszur(8);

        // Elemek keresése
        Console.WriteLine("A fa tartalmazza a 4-es elemet? " + fa.Eleme(4)); // true
        Console.WriteLine("A fa tartalmazza a 10-es elemet? " + fa.Eleme(10)); // false

        // Új elem beszúrása és ellenőrzés
        fa.Beszur(10);
        Console.WriteLine("10-es elem beszúrása után: " + fa.Eleme(10)); // true
        
        // Függőség szűrési feltétel: Csak pozitív számok elfogadhatóak
        Func<SzamFuggo, bool> elfogadhato = x => x.Szam > 0;

        //.....
        
        // FuggoFaHalmaz példány létrehozása
            FuggoFaHalmaz<SzamFuggo> fa2 = new FuggoFaHalmaz<SzamFuggo>(elfogadhato);

            // Elembeszúrás (független elemek)
            fa2.Beszur(new SzamFuggo(-55));
            fa2.Beszur(new SzamFuggo(3));
            fa2.Beszur(new SzamFuggo(7));

            // Függő elemek beszúrása
            SzamFuggo fugg1 = new SzamFuggo(-2, new SzamFuggo(-1));
            SzamFuggo fugg2 = new SzamFuggo(4, fugg1);
            fa2.Beszur(fugg1);
            fa2.Beszur(fugg2);

            FaHalmaz<SzamFuggo> problemak = fa2.ProblemasElemek();
            
            // Problémás elemek kiíratása
            Console.WriteLine("Problémás elemek a fában:");
            problemak.PreOrderBejaras(x => Console.WriteLine(x)); 
    }
}