using GenericNode.Utills;
using System.Globalization;

namespace GenericNode.T_Value_Test
{
    public class MainFlowerList
    {
        public static MainFlowerList instance = new MainFlowerList();
        internal Node<Flower> List;
        public MainFlowerList() { }
        public MainFlowerList(bool Debug)
        {
            if (Debug)
                List = GenerateRandomFlowerList(20);
            else
                List = CreateFlowerListInterface();
            instance = this;
        }
        public int RemoveAll(double minHeight, double maxHeight)
        {
            if (minHeight > maxHeight) 
                return RemoveAll(maxHeight, minHeight);
            return List.RemoveAll(h => h.GetHeight() < minHeight || h.GetHeight() > maxHeight);
        }
        public void MostCommonSeasons()
        {
            Season[] index = Enum.GetValues<Season>();
            int[] count = new int[index.Length];
            Array.Fill(count, 0);
            Node<Flower> node = List;

            for (Flower f = node.GetValue(); node.HasNext(); node = node.GetNext(), f = node.GetValue())
            {
                for (int i = 0; i < index.Length; i++)
                {
                    if (index[i] == f.GetSeason())
                        count[i]++;
                }
            }
            int max = 0;
            for (int i = 1; i < count.Length; i++)
                if (count[i] > count[max])
                    max = i;
            max = count[max];
            Console.WriteLine("Most flowers in seasons:");
            for (int i = 0; i < count.Length; i++)
                if (count[i] == max)
                    Console.WriteLine($"{index[i]} ");
        }
        internal Node<Flower> GenerateRandomFlowerList(int length)
        {
            Flower flowerOne = Flower.GenerateRandomFlower(1);
            Node<Flower> list = new Node<Flower>(flowerOne);
            Node<Flower> last = list, p;
            for (int i = 2; i <= length + 1; i++)
            {
                flowerOne = Flower.GenerateRandomFlower(i);
                p = new Node<Flower>(flowerOne);
                last.SetNext(p);
                last = p;
            }
            return list;
        }

        internal Node<Flower> CreateFlowerListInterface()
        {
            Flower flowerOne = Flower.CreateFlowerInterface();
            Node<Flower> list = new Node<Flower>(flowerOne);
            Node<Flower> last = list, p;
            for (int i = 2; InputHandler.instance.YesOrNoInput($"Create Flower #{i}"); i++)
            {
                Console.Clear();
                flowerOne = Flower.CreateFlowerInterface();
                p = new Node<Flower>(flowerOne);
                last.SetNext(p);
                last = p;
            }
            return list;
        }
     //   public bool Remove(Func<> condition) { string s = s.Select()}
        
        internal void PrintFlowersInColor()
        {
            Node<Flower> node = List;
            for (Flower i = node.GetValue(); node.HasNext(); node = node.GetNext(), i = node.GetValue())
                InputHandler.instance.InputGate(i.PrintFlowerInColor);
        }
        
        


    }
}
