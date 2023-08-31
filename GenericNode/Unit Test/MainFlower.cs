using GenericNode.T_Value_Test;
using GenericNode.Utills;

namespace GenericNode.Unit_Test
{
    internal class MainFlower
    {
        public MainFlowerList FlowerList;
        readonly bool Debug = InputHandler.instance.Debug;
        public MainFlower() =>
            FlowerList = new MainFlowerList(Debug);
        public void PrintFlowers() => FlowerList.PrintFlowersInColor();
        public void MostCommonSeason() => FlowerList.MostCommonSeasons();
        public int RemoveAll(double min, double max) => FlowerList.RemoveAll(min, max);

    }
}
