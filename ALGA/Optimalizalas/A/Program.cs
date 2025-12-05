namespace ALGA_ZH_A;

class Program
{
    static void Main(string[] args)
    {
        // 1. LancoltLista<string> példa
        LancoltLista<string> lista = new LancoltLista<string>();
        
        lista.Beszur(0, "alma");
        lista.Beszur(1, "korte");
        lista.Beszur(2, "szilva");

        Console.WriteLine("Beszúrás után a lista elemei:");
        
        Console.WriteLine($"Elemszám: {lista.Elemszam}");
        
        lista.Torles("korte");
        Console.WriteLine("Törlés után a lista elemei:");
        
        Console.WriteLine($"Elemszám: {lista.Elemszam}");
        
        // 2. SzamoloLancoltLista<string> példa
        SzamoloLancoltLista<string> szamoloLista = new SzamoloLancoltLista<string>();

        // 3.
        szamoloLista.Beszur(0, "alma");
        szamoloLista.Beszur(1, "alma"); 
        szamoloLista.Beszur(2, "korte");
        szamoloLista.Beszur(3, "szilva");

        Console.WriteLine($"Elemszám beszúrás után: {szamoloLista.Elemszam}");
        
        szamoloLista.Torles("alma");
        Console.WriteLine($"Elemszám törlés után: {szamoloLista.Elemszam}");
        
        szamoloLista.Torles("alma");
        Console.WriteLine($"Elemszám végleges törlés után: {szamoloLista.Elemszam}");
        
        SzamoloLancoltLista<string> szamoloLista2 = new SzamoloLancoltLista<string>();
        
        szamoloLista2.Beszur(0, "szilvafa");
        szamoloLista2.Beszur(1, "korte");
        szamoloLista2.Beszur(2, "barack");
        szamoloLista2.Beszur(3, "szilvafa");

        Console.WriteLine($"Elemszám módosítás előtt: {szamoloLista2.Elemszam}");
        
        szamoloLista2.DarabSzamModositas((tartalom, darabszam) => 
        {
            if (tartalom.Length > 5)
                return darabszam * 2;
            return darabszam;
        });

        Console.WriteLine($"Elemszám módosítás után: {szamoloLista2.Elemszam}");
    }
}