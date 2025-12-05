namespace ALGA_ZH_C;

class Program
{
    static void Main(string[] args)
    {
        //Verem<int> peladahasznalat
        Verem<int> verem = new Verem<int>(5);

        // Értékek hozzáadása a veremhez
        verem.Verembe(10);
        verem.Verembe(20);
        verem.Verembe(30);

        Console.WriteLine("Veremben lévő elemek száma: " + verem.Elemszam);  // Kiírja: 3

        // Kiolvassuk az elemeket a veremből
        int elem1 = verem.Verembol();
        int elem2 = verem.Verembol();
            
        Console.WriteLine("Első eltávolított elem: " + elem1);  // Kiírja: 30
        Console.WriteLine("Második eltávolított elem: " + elem2); // Kiírja: 20

        Console.WriteLine("Veremben lévő elemek száma: " + verem.Elemszam);  // Kiírja: 1
        
        //Ihelyettesithetot megvalosito osztaly hasznalata
        // DinamikusVerem létrehozása Szam típusú objektumokkal
        DinamikusVerem<Szam> dinamikusVerem = new DinamikusVerem<Szam>(5);

        // Elemszámok hozzáadása a veremhez
        dinamikusVerem.Verembe(new Szam(4));   // páros, helyettesítendő
        dinamikusVerem.Verembe(new Szam(7));   // páratlan
        dinamikusVerem.Verembe(new Szam(10));  // páros, helyettesítendő
        dinamikusVerem.Verembe(new Szam(15));  // páratlan
        dinamikusVerem.Verembe(new Szam(8));   // páros, helyettesítendő
        
        //Paros szamok megszamlalasa
        bool ParosE(Szam szam)
        {
            if (szam.Ertek % 2 == 0) return true;
            return false;
        }
        
        int parosSzamok = dinamikusVerem.Megszamol(ParosE);
        Console.WriteLine($"A veremben lévő páros számok száma: {parosSzamok}");

        // Elemek kiszedése a veremből helyettesítéssel
        try
        {
            Console.WriteLine("Kivett elem: " + dinamikusVerem.Verembol()); // Nem helyettesítendő elem
            Console.WriteLine("Kivett elem: " + dinamikusVerem.Verembol()); // Helyettesíthető elem
            Console.WriteLine("Kivett elem: " + dinamikusVerem.VerembolCserevel()); // Csere a helyettesítővel
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hiba: " + ex.Message);
        }
    }
}
