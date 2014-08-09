using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Avro
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Input value is null");
            return input.First().ToString(CultureInfo.InvariantCulture).ToUpper() + String.Join("", input.Skip(1));
        }
    }
}
