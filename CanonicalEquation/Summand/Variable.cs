namespace CanonicalEquation.Summand
{
    internal class Variable
    {
        public Variable()
        {
            Power = 1;
        }

        public Variable(char letter, int power)
        {
            Letter = letter;
            Power = power;
        }

        public char Letter { get; set; }

        public int Power { get; set; }

        public override string ToString()
        {
            var output = string.Empty;
            if (Power == 0)
            {
                return output;
            }

            output += Letter.ToString();

            if (Power != 1)
            {
                output += "^" + Power;
            }

            return output.ToLower();
        }
    }
}