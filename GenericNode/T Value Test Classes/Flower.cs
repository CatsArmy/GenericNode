using GenericNode.Utills;
using System.Drawing;

namespace GenericNode.T_Value_Test
{
    internal class Flower
    {
        private string name;
        private double height;
        private Color color;
        private Season season;

        public Flower(string name, double height, Color color, Season season)
        {
            this.name = name;
            this.height = height;
            this.color = color;
            this.season = season;
        }

        public string GetName() => name;
        public void SetName(string name) => this.name = name;

        public double GetHeight() => height;
        public void SetHeight(float height) => this.height = height;

        public Color GetColor() => color;
        public void SetColor(Color color) => this.color = color;

        public Season GetSeason() => season;
        public void SetSeason(Season season) => this.season = season;
        private readonly string flower = @"
   \* ^ */
  } /\|/\ {
  <(}[*]{)>
  } \/|\/ {
    / | \
      |
      |";
        private readonly string addStem = "\n      |";

        public void PrintFlowerInColor() => InputHandler.instance.TempConsoleColor(GetFlower(), GetColor());
        internal string GetFlower()
        {
            string flower = $"{this}\n{this.flower}";
            for (double i = height - 1; i >= 0; i--)
                flower += addStem;
            return flower;
        }
        public override string ToString() =>
             $"Flower: {name}, Height: {height}, Color: {color}, Season: {season}";
        public static Flower CreateFlowerInterface()
        {
            Console.WriteLine("Creating Flower...");
            Console.WriteLine("Input Flower name");
            string name = InputHandler.instance.ReadInput(true, true);
            Console.WriteLine("Input Flower <double> Height");
            InputHandler.instance.ReadInput();
            double height = InputHandler.instance.TryParseDouble();
            Console.WriteLine("Input Flower Color");
            InputHandler.instance.ReadInput();
            Color color = InputHandler.instance.TryParseColor();
            Console.WriteLine("Input Flower Season");
            InputHandler.instance.ReadInput();
            Season season = InputHandler.instance.TryParseSeason();
            Flower flower = new Flower(name, height, color, season);
            if (flower is null)
                return CreateFlowerInterface();
            return flower;
        }
        public static Flower GenerateRandomFlower(int index)
        {
            Random r = new Random();
            string name = $"Flower #{index}";
            double height = r.Next(0, 9) + r.NextDouble();
            Color color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            Season? season = null;
            while (season is null)
                season = r.Next(1, 4).ToString().ToEnum<Season>();
            Flower flower = new Flower(name, height, color, season.Value);
            if (flower is null)
                return GenerateRandomFlower(index);
            return flower;
        }
    }
   
}
