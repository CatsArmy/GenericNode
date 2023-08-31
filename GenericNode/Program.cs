using GenericNode;
using GenericNode.Unit_Test;

/*
MainFlower main = new MainFlower();
main.PrintFlowers();
main.MostCommonSeason();
Console.WriteLine(main.RemoveAll(3.5, 7.5));
main.MostCommonSeason();*/
internal class Program
{
    private static void Main(string[] args)
    {
        
        GenericNode.Queue<int> q = new GenericNode.Queue<int>();
        for (int i = 0; i < 10; i++)
        {
            q.Insert(i);
            q.Insert(i);
        }
        Console.WriteLine(q);
        q.RemoveDuplicates();
        Console.WriteLine(q);
    }
}