//Diego Gonzalez Martin
//Marta Reyes Funk
namespace Hoja5;
using Coordinates;


internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
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
        while (n<coors.Length&&!enc)
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
        else Console.WriteLine("error, array lleno");
        }
        return !existe;
    }
    public bool Belongs (Coor c)
    {
        return SearchElem(c)!=-1;
    }
}

