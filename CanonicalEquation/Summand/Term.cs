using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.String;

namespace CanonicalEquation.Summand
{
    internal class Term : IComparable<Term>, IEquatable<Term>, IComparable
    {
        private const double Epsilon = 0.001;

        public Term()
        {
            Sign = Signs.Plus;
            Number = 1.0f;
            Vars = new List<Variable>();
        }

        public char Sign { get; set; }

        public float Number { private get; set; }

        public int Rank
        {
            get { return Vars.Sum(x => x.Power); }
        }

        public List<Variable> Vars { get; set; }

        public int CompareTo(object other)
        {
            if (!(other is Term))
            {
                throw new InvalidOperationException("CompareTo: not a Term");
            }

            return CompareTo((Term) other);
        }

        public int CompareTo(Term other)
        {
            if (Equals(other))
            {
                return 0;
            }

            var rankCompareResult = Rank.CompareTo(other.Rank);
            if (rankCompareResult == 0)
            {
                var varsCountComparison = Vars.Count.CompareTo(other.Vars.Count);
                if (varsCountComparison == 0)
                {
                    return -Compare(
                        new string(Vars.Select(x => x.Letter).ToArray()),
                        new string(other.Vars.Select(x => x.Letter).ToArray()),
                        StringComparison.Ordinal);
                }

                return -varsCountComparison;
            }

            return rankCompareResult;
        }

        public bool Equals(Term other)
        {
            return VarsToString().Equals(other.VarsToString());
        }

        public override bool Equals(object other)
        {
            if (!(other is Term))
            {
                return false;
            }

            return Equals((Term) other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ (VarsToString()?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public float GetNumber()
        {
            return float.Parse(Sign.ToString() + Number);
        }

        public float AddToNumber(float augend)
        {
            var result = GetNumber() + augend;
            Sign = result < 0 
                ? Signs.Minus 
                : Signs.Plus;
            Number = Math.Abs(result);

            return GetNumber();
        }

        public string VarsToString()
        {
            return Concat(Vars
                .OrderByDescending(x => x.Power)
                .ThenBy(x => x.Letter)
                .Select(x => x.ToString()));
        }

        public void InverseSign()
        {
            Sign =
                Sign == Signs.Minus
                    ? Signs.Plus
                    : Signs.Minus;
        }

        public bool IsZero()
        {
            if (Math.Abs(Number) < 0.0000000001)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            var output = Empty;
            if (IsZero())
            {
                return output;
            }

            if (Math.Abs(Number - 1.0) > Epsilon || Rank == 0)
            {
                output += GetNumber().ToString("0.0;- 0.0", CultureInfo.InvariantCulture);
            }
            else if (Sign == Signs.Minus)
            {
                output += Sign + " ";
            }

            output += Concat(Vars);

            return output;
        }
    }
}