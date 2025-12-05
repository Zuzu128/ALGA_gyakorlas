namespace ALGA_ZH_D;

public class FaElem<T> where T : IComparable<T>
{
    public T tart;
    public FaElem<T>? bal;
    public FaElem<T>? jobb;

    public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb)
    {
        this.tart = tart;
        this.bal = bal;
        this.jobb = jobb;
    }
}