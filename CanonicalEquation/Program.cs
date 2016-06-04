using System;
using System.IO;
using CanonicalEquation.Infra;

namespace CanonicalEquation
{
    internal class CanonicalEquation
    {
        private static void Main(string[] args)
        {
            try
            {
                var workMode = new ArgumentsParser().Parse(args);

                var transformer = new EquationTransformer();
                if (workMode == ApplicationWorkMode.File)
                {
                    MakeEquationCanonicalFromFile(args, transformer);
                    return;
                }

                MakeEquationCanonicalFromConsole(transformer);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine("Error occurred in command line arguments: {0}", exception);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine("Input file was not found: {0}", exception);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Unexpected error occured: {0}", exception);
            }
        }

        private static void MakeEquationCanonicalFromConsole(EquationTransformer transformer)
        {
            while (true)
            {
                Console.WriteLine("Input equation");
                var equation = Console.ReadLine();
                if (equation == null)
                {
                    return;
                }

                var canonicalEquation = transformer.MakeCanonical(equation);
                Console.WriteLine(canonicalEquation);
            }
        }

        private static void MakeEquationCanonicalFromFile(string[] args, EquationTransformer transformer)
        {
            if (!File.Exists(args[1]))
            {
                throw new FileNotFoundException();
            }

            var outputFileName = $"{args[1]}.out";
            var equations = File.ReadAllLines(args[1]);
            using (var outputStream = new StreamWriter(outputFileName))
            {
                foreach (var equation in equations)
                {
                    var canonicalEquation = transformer.MakeCanonical(equation);
                    outputStream.WriteLine(canonicalEquation);
                }
            }

            Console.WriteLine("Equations were successfully processed and written into {0}", outputFileName);
        }
    }
}