namespace ALGA_ZH_A;

public class LancElem<T>
{
    public T tart;
    public LancElem<T>? kov;
    public int darabszam;

    public LancElem(T tart, LancElem<T>? kov)
    {
        this.tart = tart;
        this.kov = kov;
    }

    public LancElem(T tart, LancElem<T>? kov, int darabszam)
    {
        this.tart = tart;
        this.kov = kov;
        this.darabszam = darabszam;
    }
}