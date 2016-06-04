using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CanonicalEquation.Summand;

namespace CanonicalEquation
{
    internal class EquationTransformer
    {
        private readonly Stack<bool> changeSignByBracketsStack = new Stack<bool>();
        private readonly SortedDictionary<Term, Term> sortedSummands;

        public EquationTransformer()
        {
            sortedSummands = new SortedDictionary<Term, Term>(new DescendingComparer<Term>(Comparer<Term>.Default));
            changeSignByBracketsStack.Push(false);
        }

        public string MakeCanonical(string equation)
        {
            var normalEquation = NormalizeString(equation);

            var normalEquationParts = normalEquation.Split('=');
            ParseEquation(normalEquationParts[0], false);
            ParseEquation(normalEquationParts[1], true);

            var canonicalEquation = OutputEquation(sortedSummands);
            sortedSummands.Clear();

            return canonicalEquation;
        }

        private string OutputEquation(SortedDictionary<Term, Term> summands)
        {
            var result = new StringBuilder();
            var firstElement = true;
            foreach (var summand in summands)
            {
                if (summand.Value.IsZero())
                {
                    continue;
                }

                if (summand.Value.Sign == Signs.Plus && !firstElement)
                {
                    result.Append(Signs.Plus + " ");
                }

                result.Append($"{summand.Value} ");
                firstElement = false;
            }

            if (firstElement)
            {
                result.Append("0 ");
            }

            return result.Append("= 0").ToString();
        }

        private string NormalizeString(string equation)
        {
            return equation.Replace(" ", string.Empty);
        }

        private void ParseEquation(string equation, bool inverseSign)
        {
            var i = 0;
            while (i < equation.Length)
            {
                if (equation[i] == ')')
                {
                    i++;
                    changeSignByBracketsStack.Pop();
                    continue;
                }

                ParseTerm(equation, inverseSign, ref i);
            }
        }

        private void ParseTerm(string equation, bool inverseSign, ref int i)
        {
            var term = new Term();

            ParseSign(term, equation, ref i);
            ParseNumber(term, equation, ref i);
            ParseVariables(term, equation, ref i);

            if (inverseSign ^ changeSignByBracketsStack.Peek())
            {
                term.InverseSign();
            }

            AddTermToCollection(term);
        }

        private void AddTermToCollection(Term term)
        {
            Term foundedTerm;
            if (sortedSummands.TryGetValue(term, out foundedTerm))
            {
                foundedTerm.AddToNumber(term.GetNumber());
            }
            else
            {
                sortedSummands.Add(term, term);
            }
        }

        private void ParseVariables(Term term, string equation, ref int i)
        {
            while (true)
            {
                if (i >= equation.Length || !char.IsLetter(equation[i]))
                {
                    break;
                }

                ParseVariable(term, equation, ref i);
            }
        }

        private void ParseVariable(Term term, string equation, ref int i)
        {
            var variable = new Variable();

            variable.Letter = equation[i++];
            if (i >= equation.Length)
            {
                term.Vars.Add(variable);
                return;
            }

            ParsePower(variable, equation, ref i);

            term.Vars.Add(variable);
        }

        private void ParsePower(Variable variable, string equation, ref int i)
        {
            var tempList = new List<char>();
            if (equation[i] == '^')
            {
                i++;
                do
                {
                    tempList.Add(equation[i++]);
                    if (i >= equation.Length)
                    {
                        break;
                    }
                } while (char.IsDigit(equation[i]));

                variable.Power = int.Parse(string.Concat(tempList));
            }
        }

        private void ParseNumber(Term term, string equation, ref int i)
        {
            if (!char.IsDigit(equation[i]))
            {
                return;
            }

            var tempList = new List<char>();
            while (char.IsDigit(equation[i]) ||
                   equation[i].ToString() == CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator)
            {
                tempList.Add(equation[i++]);
                if (i >= equation.Length)
                {
                    break;
                }
            }

            term.Number = float.Parse(string.Concat(tempList), NumberStyles.Any, new CultureInfo("en-US"));
        }

        private void ParseSign(Term term, string equation, ref int i)
        {
            CheckForBracket(equation, ref i);

            if (equation[i] == Signs.Minus || equation[i] == Signs.Plus)
            {
                term.Sign = equation[i++];
            }
        }

        private void CheckForBracket(string equation, ref int i)
        {
            if (equation[i] == '(')
            {
                i++;
                changeSignByBracketsStack.Push(false);
                return;
            }

            if (equation[i] == Signs.Minus || equation[i] == Signs.Plus)
            {
                if (equation[i + 1] == '(')
                {
                    var lastSignChangedValue = changeSignByBracketsStack.Peek();
                    if (equation[i] == Signs.Minus)
                    {
                        changeSignByBracketsStack.Push(lastSignChangedValue ^ true);
                    }
                    else
                    {
                        changeSignByBracketsStack.Push(lastSignChangedValue);
                    }

                    i += 2;
                }
            }
        }
    }
}