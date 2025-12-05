using System;

namespace OE.ALGA.Adatszerkezetek
{
    // 5. heti labor feladat - Tesztek: 05_BinarisKeresoFaTesztek.cs

    public class FaElem<T> where T : IComparable
    {
        public T tart;
        public FaElem<T>? bal;
        public FaElem<T>? jobb;
        //public int darab = 0;

        public FaElem(T tart, FaElem<T>? bal, FaElem<T>? jobb)
        {
            this.tart = tart;
            this.bal = bal;
            this.jobb = jobb;
        }
    }

    public class FaHalmaz<T> : Halmaz<T> where T : IComparable
    {
        FaElem<T>? gyoker;

        void ReszfaBejarasPreorder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p != null)
            {
                muvelet(p.tart);
                ReszfaBejarasPreorder(p.bal, muvelet);
                ReszfaBejarasPreorder(p.jobb, muvelet);
            }
        }

        void ReszfaBejarasInorder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p != null)
            {
                ReszfaBejarasInorder(p.bal, muvelet);
                muvelet(p.tart);
                ReszfaBejarasInorder(p.jobb, muvelet);
            }
        }

        void ReszfaBejarasPostorder(FaElem<T>? p, Action<T> muvelet)
        {
            if (p != null)
            {
                ReszfaBejarasPostorder(p.bal, muvelet);
                ReszfaBejarasPostorder(p.jobb, muvelet);
                muvelet(p.tart);
            }
        }

        public void Bejar(Action<T> muvelet)
        {
            ReszfaBejarasPreorder(gyoker, muvelet);
        }

        FaElem<T> ReszfabaBeszur(FaElem<T>? p, T ertek)
        {
            if (p == null)
            {
                FaElem<T> uj = new FaElem<T>(ertek, null, null);
                return uj;
            }
            else
            {
                if (p.tart.CompareTo(ertek) > 0)
                {
                    p.bal = ReszfabaBeszur(p.bal, ertek);
                }
                else
                {
                    if (p.tart.CompareTo(ertek) < 0)
                    {
                        p.jobb = ReszfabaBeszur(p.jobb, ertek);
                    }
                }
                return p;
            }
        }

        public void Beszur(T ertek)
        {
            gyoker = ReszfabaBeszur(gyoker, ertek);
            /*if (Eleme(ertek))
            {
                Kereses(ertek).darab++;
            }*/
        }

        bool ReszfaEleme(FaElem<T>? p, T ertek)
        {
            if (p != null)
            {
                if (p.tart.CompareTo(ertek) == 1)
                {
                    return ReszfaEleme(p.bal, ertek);
                }
                else
                {
                    if (p.tart.CompareTo(ertek) == -1)
                    {
                        return ReszfaEleme(p.jobb, ertek);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public bool Eleme(T ertek)
        {
            return ReszfaEleme(gyoker, ertek);
        }

        FaElem<T> ReszfabolTorol(FaElem<T>? p, T ertek)
        {
            if (p != null)
            {
                if (p.tart.CompareTo(ertek) == 1)
                {
                    p.bal = ReszfabolTorol(p.bal, ertek);
                }
                else
                {
                    if (p.tart.CompareTo(ertek) == -1)
                    {
                        p.jobb = ReszfabolTorol(p.jobb, ertek);
                    }
                    else
                    {
                        if (p.bal == null)
                        {
                            FaElem<T>? q = p;
                            p = p.jobb;
                            q = null;
                        }
                        else
                        {
                            if (p.jobb == null)
                            {
                                FaElem<T>? q = p;
                                p = p.bal;
                                q = null;
                            }
                            else
                            {
                                p.bal = KetGyerekesTorles(p, p.bal);
                            }
                        }
                    }
                }
                return p;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        FaElem<T> KetGyerekesTorles(FaElem<T>? e, FaElem<T>? r)
        {
            if (r.jobb != null)
            {
                r.jobb = KetGyerekesTorles(e, r.jobb);
                return r;
            }
            else
            {
                e.tart = r.tart;
                FaElem<T>? q = r;
                r = r.bal;
                q = null;
                return r;
            }
        }

        public void Torol(T ertek)
        {
            //if (Eleme(ertek))
            //{
                //Kereses(ertek).darab--;
                //if(Kereses(ertek).darab == 0)
                //{
                    gyoker = ReszfabolTorol(gyoker, ertek);
                //}
            //}
        }

        /*public FaElem<T> Kereses(T ertek)
        {
            return PrivateKereses(gyoker, ertek);
        }*/

        /*public int Darab(T ertek)
        {
            if (Eleme(ertek))
            {
                return Kereses(ertek).darab;
            }
            return 0;
        }*/

        /*private FaElem<T> PrivateKereses(FaElem<T> gyokerszeruseg, T ertek)
        {
            int cmp = gyokerszeruseg.tart.CompareTo(ertek);

            if (cmp > 0)
            {
                return PrivateKereses(gyokerszeruseg.bal, ertek);
            }
            else if (cmp < 0)
            {
                return PrivateKereses(gyokerszeruseg.jobb, ertek);
            }
            else
            {
                return gyokerszeruseg;
            }
        }*/
    }
}
