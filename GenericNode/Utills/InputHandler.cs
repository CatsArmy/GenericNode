using GenericNode.T_Value_Test;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;

namespace GenericNode.Utills
{
    public class InputHandler
    {
        private string? input = null;
        private string? lastInput = null;
        
        //private InputHandler() => Debug = IsBuildRelease();
        public static InputHandler instance = new InputHandler();

        public bool Debug
        {
            get
            {
#if RELEASE
                return false;
#endif
                return true;
            }
            private set { }
        }

        public string ReadInput(bool allowDuplicates = true, bool confirmInput = false)
        {
            if (this.input is not null)
                this.lastInput = this.input;
            string? input = Console.ReadLine();
            if (input is not null)
                if (allowDuplicates || lastInput != input)
                {
                    this.input = input;
                    if (confirmInput)
                        ConfirmationInput(allowDuplicates, confirmInput);
                }
                else
                {
                    Console.WriteLine("Error input == last input & allowDuplicates is true... Try Again");
                    return ReadInput(allowDuplicates, confirmInput);
                }
            else
            {
                Console.WriteLine("Error input is null... Try Again");
                return ReadInput(allowDuplicates, confirmInput);
            }

            return this.input;
        }
        public bool YesOrNoInput(string question, Action noAction = null, Action yesAction = null)
        {
            Console.WriteLine($"{question}\nY/N");
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Y)
            {
                if (yesAction is not null)
                    yesAction();
                return true;
            }
            if (key == ConsoleKey.N)
            {
                if (noAction is not null)
                    noAction();
                return false;
            }
            Console.WriteLine("Invalid Key");
            return YesOrNoInput(question, noAction, yesAction);
        }


        public void InputGate(Action action = null)
        {

            if (YesOrNoInput("Are you ready yet?", null, action)) 
                return;

            TimeSpan time = new(0, 0, 0, 1, 0);
            Console.Clear();
            Thread.Sleep(time);
            Console.Write('.');
            Thread.Sleep(time);
            Console.Write('.');
            Thread.Sleep(time);
            Console.WriteLine('.');
            Console.Clear();

            if (YesOrNoInput("Are you ready now... ?", null, action))
            {
                Console.WriteLine("Took you long enough");
                return;
            }
            
            Console.Clear();
            Thread.Sleep(time);
            Console.WriteLine("AAAAAAAAAAAAAAAAAAUUUUUGGGGGGGHHHHH");
            Thread.Sleep(time);
            Console.Write('.');
            Thread.Sleep(time);
            Console.Write('.');
            Thread.Sleep(time);
            Console.Write('.');
            Thread.Sleep(time);
            Console.Clear();
            Thread.Sleep(time);
            Console.WriteLine("Sigh just tell me when you're ready");

            if (YesOrNoInput("ARE YOU READY NOW!?!?!?!?!!!!!!!!!", delegate { InputGate(action); }, action))
                return;
        }

        public bool ConfirmationInput(bool allowDuplicates = true, bool allwaysConfirm = false,
            string confirmationString = "Please confirm this is the correct value")
        {
            if (input is null)
            {
                Console.WriteLine("Error input is null, please input again.");
                ReadInput(true, allwaysConfirm);
            }
            if (allwaysConfirm)
                return YesOrNoInput(confirmationString, delegate {
                    Console.WriteLine("Input your new value");
                    ReadInput(allowDuplicates, allwaysConfirm); 
                });

            if (!allowDuplicates)
                if (lastInput is null) {
                } else if (lastInput == input)
                    return YesOrNoInput(confirmationString, delegate { ReadInput(true, allwaysConfirm); });
            return true;
        }

        public Color TryParseColor()
        {
            ConfirmationInput();
            Color color = Color.FromName(input);
            if (color.IsKnownColor)
                color = Color.FromKnownColor(color.ToKnownColor());
            else if ((color.A & color.R & color.G & color.B) == byte.MinValue)
            {
                Console.WriteLine("Failed to parse color, invalid input color does not exist");
                Console.WriteLine("Input Color");
                ReadInput(false);
                return TryParseColor();
            }
            return color;
        }
        public double TryParseDouble()
        {
            ConfirmationInput();
            if (double.TryParse(input, out double value))
                return value;
            Console.WriteLine("Error invalid input, Input can only be type <int> or <double> not <string>");
            ReadInput(false);
            return TryParseDouble();
        }
        public Season TryParseSeason()
        {
            ConfirmationInput();
            /*if (Season.spring.ToString() == formatedInput)
                return Season.spring;
            if (Season.summer.ToString() == formatedInput)
                return Season.summer;
            if (Season.autumn.ToString() == formatedInput)
                return Season.autumn;
            if (Season.winter.ToString() == formatedInput)
                return Season.winter;
            */
            Season? season = input.ToEnum<Season>();
            if (season is null)
            {
                Console.WriteLine("Error, Failed to parse input: invalid input does not match any Season.");
                ReadInput(false);
                return TryParseSeason();
            }
            return (Season)season;
        }
        public void TempConsoleColor(string print, Color color)
        {
            ConsoleColor consoleColor = color.ClosestConsoleColor();
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(print);
            Console.ResetColor();
        }
        public void TempConsoleColor(string print, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(print);
            Console.ResetColor();
        }
    }
    public static class ExtendConsoleColor
    {
        public static ConsoleColor ClosestConsoleColor(this Color color)
        {
            ConsoleColor ret = 0;
            double r = color.R, g = color.G, b = color.B, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - r, 2.0) + Math.Pow(c.G - g, 2.0) + Math.Pow(c.B - b, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
        public static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
        {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }
    }
    public static class ExtendString
    {
        public static T? ToEnum<T>(this string str) where T : struct
        {
            if (Enum.TryParse(str, true, out T t) && Enum.IsDefined(typeof(T), t))
                return t;
            return null;
        }
        public static string CapitalizeFirst(this string s, bool includeWhiteSpace = true)
        {
            bool IsNewSentense = true;
            StringBuilder? result = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNewSentense && char.IsLetter(s[i]))
                {
                    result.Append(char.ToUpper(s[i]));
                    IsNewSentense = false;
                }
                else
                    result.Append(s[i]);

                if (s[i] == '!' || s[i] == '?' || s[i] == '.' || s[i] == ' ' && includeWhiteSpace)
                    IsNewSentense = true;
            }

            return result.ToString();
        }

    }
}
