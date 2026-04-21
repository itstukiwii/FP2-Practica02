//Diego Gonzalez Martin
//Marta Reyes Funk
namespace Hoja5;
using Coordinates;

class SetCoor
{
    Coor[] coors;
    int oc;
    public SetCoor (int tam = 10)
    {
        coors = new Coor[tam];
        oc = 0;
    }
    private int SearchElem (Coor c)
    {
        bool enc = false;
        int n = 0;
        while (n < oc && !enc)
        {
            if (coors[n] == c) enc = true;
            else n++;
        }
        if (enc) { return n; }
        else { return -1; }
    }
    public bool Add(Coor c)
    {
        bool existe = SearchElem(c)!=-1;
        if (!existe)
        {
            if (oc != coors.Length)
            {
                coors[oc] = c;
                oc++;
            }
        }
        return !existe;
    }
    public bool Belongs (Coor c)
    {
        return SearchElem(c)!=-1;
    }
    public Coor GetCoor(int i)
    {
        return coors[i];
    }
    public int NElem()
    {
        return oc;
    }
    private void Elim(int pos)
    {
        int i;
        for (i = pos; i < oc - 1; i++)
        {
            coors[i] = coors[i + 1];
        }
        coors[i] = new Coor();
        oc--;
    }

    public Coor PopElem()
    {
        Random rnd = new Random();
        int i = rnd.Next(0, oc);
        Coor aux = GetCoor(i);
        Elim(i);
        return aux;
    }
}

