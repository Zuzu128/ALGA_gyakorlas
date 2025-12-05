namespace ALGA_ZH_B;

public class FaElem<T> where T : IComparable
{
    public T tart;
    public FaElem<T>? bal;
    public FaElem<T>? jobb;
    public int darabszam;

    public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb)
    {
        this.tart = tart;
        this.bal = bal;
        this.jobb = jobb;
    }

    public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb, int darabszam)
    {
        this.tart = tart;
        this.bal = bal;
        this.jobb = jobb;
        this.darabszam = darabszam;
    }
}