using System;

namespace CanonicalEquation.Infra
{
    internal class ArgumentsParser
    {
        public ApplicationWorkMode Parse(string[] args)
        {
            if (args.Length < 1)
            {
                return ApplicationWorkMode.Console;
            }

            if (args.Length > 2)
            {
                throw new ArgumentException("Too many arguments");
            }

            var firstArgParts = args[0].Split(':');
            if (!firstArgParts[0].Equals("/m", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid argumenet name", firstArgParts[0]);
            }

            if (firstArgParts[1].Equals(ApplicationWorkMode.Console.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return ApplicationWorkMode.Console;
            }

            if (firstArgParts[1].Equals(ApplicationWorkMode.File.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                if (args.Length != 2)
                {
                    throw new ArgumentException("No input file name set");
                }
                return ApplicationWorkMode.File;
            }

            throw new ArgumentException("Unknown argument value", firstArgParts[1]);
        }
    }
}